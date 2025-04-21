using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Sabanishi.ZundaManufacture.Sandbox
{
    public class CoroutineRunner : IDisposable
    {
        private class CoroutineInfo
        {
            public Coroutine coroutine;
            public IDisposable disposable;

            public event Action<Exception> OnError;
            public event Action OnCanceled;
            public event Action OnCompleted;

            private bool _isCanceled;
            private Exception _exception;
            private bool _isCompleted;

            public bool IsDone => _isCanceled || _exception != null || _isCompleted;

            /// <summary>
            /// 完了処理
            /// </summary>
            public void Complete()
            {
                if (IsDone)
                {
                    return;
                }

                var onCompletedInternal = OnCompleted;

                _isCompleted = true;
                OnCanceled = null;
                OnError = null;
                OnCompleted = null;
                disposable?.Dispose();
                disposable = null;

                onCompletedInternal?.Invoke();
            }

            /// <summary>
            /// キャンセル処理
            /// </summary>
            public void Cancel()
            {
                if (IsDone)
                {
                    return;
                }

                var onCanceledInternal = OnCanceled;

                _isCanceled = true;
                OnCanceled = null;
                OnError = null;
                OnCompleted = null;
                disposable?.Dispose();
                disposable = null;

                onCanceledInternal?.Invoke();
            }

            /// <summary>
            /// 例外処理
            /// </summary>
            /// <param name="error">例外内容</param>
            public void Abort(Exception error)
            {
                if (IsDone)
                {
                    return;
                }

                var onErrorInternal = OnError;

                _exception = error;
                OnCanceled = null;
                OnError = null;
                OnCompleted = null;
                disposable?.Dispose();
                disposable = null;

                onErrorInternal?.Invoke(error);
            }
        }

        // 制御中のコルーチン
        private readonly List<CoroutineInfo> _coroutineInfos = new List<CoroutineInfo>();

        // 更新が完了したコルーチンのIDを保持するリスト
        private readonly List<int> _cachedRemoveIndices = new List<int>();

        /// <summary>
        /// コルーチンの開始
        /// </summary>
        /// <param name="enumerator">実行する非同期処理</param>
        /// <param name="onCompleted">完了時の通知</param>
        /// <param name="onCanceled">キャンセル時の通知</param>
        /// <param name="onError">エラー時の通知</param>
        /// <param name="cancellationToken">キャンセルハンドリング用</param>
        public Coroutine StartCoroutine(IEnumerator enumerator, CoroutineTimer timer, Action onCompleted = null,
            Action onCanceled = null, Action<Exception> onError = null, CancellationToken cancellationToken = default)
        {
            if (enumerator == null)
            {
                return null;
            }

            // コルーチンの追加
            var coroutineInfo = new CoroutineInfo
            {
                coroutine = new Coroutine(enumerator,timer)
            };

            // イベント登録
            coroutineInfo.OnCompleted += onCompleted;
            coroutineInfo.OnCanceled += onCanceled;

            // すでにキャンセル済
            if (cancellationToken.IsCancellationRequested)
            {
                coroutineInfo.Cancel();
                return coroutineInfo.coroutine;
            }

            // キャンセルの監視
            coroutineInfo.disposable = cancellationToken.Register(() => coroutineInfo.Cancel());

            coroutineInfo.OnError += onError;

            _coroutineInfos.Add(coroutineInfo);

            return coroutineInfo.coroutine;
        }

        /// <summary>
        /// コルーチンの強制停止
        /// </summary>
        /// <param name="coroutine">停止させる対象のCoroutine</param>
        public void StopCoroutine(Coroutine coroutine)
        {
            if (coroutine == null)
            {
                return;
            }

            if (coroutine.IsDone)
            {
                return;
            }

            var foundIndex = _coroutineInfos.FindIndex(x => x.coroutine == coroutine);
            if (foundIndex < 0)
            {
                return;
            }

            // キャンセル処理
            var info = _coroutineInfos[foundIndex];
            info.Cancel();
        }

        /// <summary>
        /// コルーチンの全停止
        /// </summary>
        public void StopAllCoroutines()
        {
            // 降順にキャンセルしていく
            for (var i = _coroutineInfos.Count - 1; i >= 0; i--)
            {
                var info = _coroutineInfos[i]; // キャンセル処理
                info.Cancel();
            }

            _coroutineInfos.Clear();
        }

        /// <summary>
        /// 廃棄時処理
        /// </summary>
        public void Dispose()
        {
            // 全コルーチン停止
            StopAllCoroutines();
        }

        /// <summary>
        /// コルーチン更新処理
        /// </summary>
        public void Update(float deltaTime)
        {
            // コルーチンの更新
            _cachedRemoveIndices.Clear();

            // 昇順で更新
            for (var i = 0; i < _coroutineInfos.Count; i++)
            {
                var coroutineInfo = _coroutineInfos[i];
                var coroutine = coroutineInfo.coroutine;

                // すでに完了しているCoroutineの場合はそのまま除外
                if (coroutineInfo.IsDone)
                {
                    _cachedRemoveIndices.Add(i);
                    continue;
                }

                if (!coroutine.MoveNext(deltaTime))
                {
                    if (coroutine.Exception != null)
                    {
                        // エラー終了通知
                        coroutineInfo.Abort(coroutine.Exception);
                        _cachedRemoveIndices.Add(i);
                    }
                    else
                    {
                        // 完了通知
                        coroutineInfo.Complete();
                        _cachedRemoveIndices.Add(i);
                    }
                }
            }

            // 降順でインスタンスクリア
            for (var i = _cachedRemoveIndices.Count - 1; i >= 0; i--)
            {
                var removeIndex = _cachedRemoveIndices[i];
                if (removeIndex < 0 || removeIndex >= _coroutineInfos.Count)
                {
                    continue;
                }

                _coroutineInfos.RemoveAt(removeIndex);
            }
        }
    }
}