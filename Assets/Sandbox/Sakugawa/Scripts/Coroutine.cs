using System;
using System.Collections;
using System.Collections.Generic;

namespace Sabanishi.ZundaManufacture.Sandbox
{
    public class Coroutine
    {
        private readonly IEnumerator _enumerator;
        private readonly CoroutineTimer _timer;
        private readonly Stack<object> _stack;
        private object _current;
        private bool _isDone;

        /// <summary>完了しているか</summary>
        public bool IsDone => _isDone || Exception != null;

        /// <summary>エラー内容</summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="enumerator">処理</param>
        public Coroutine(IEnumerator enumerator,CoroutineTimer timer)
        {
            _enumerator = enumerator;
            _timer = timer;
            _stack = new Stack<object>();
            Reset();
        }

        /// <summary>
        /// リセット処理
        /// </summary>
        void Reset()
        {
            _stack.Clear();
            _stack.Push(_enumerator);
            _current = null;
            _isDone = false;
        }

        /// <summary>
        /// CoroutineRunner用のコルーチン実行処理(更新あり)
        /// </summary>
        /// <returns>次の処理がある場合tureを返す</returns>
        public bool MoveNext(float deltaTime)
        {
            Update(deltaTime);
            return !IsDone;
        }

        /// <summary>
        /// 処理更新
        /// </summary>
        private void Update(float deltaTime)
        {
            _timer.DeltaTime = deltaTime;
            // 完了処理
            void Done()
            {
                _stack.Clear();
                _current = null;
                _isDone = true;
            }

            // スタックがなければ、完了
            if (_stack.Count == 0)
            {
                Done();
                return;
            }

            // スタックを取り出して、処理を進める
            var peek = _stack.Peek();
            _current = peek;

            try
            {
                if (peek == null)
                {
                    _stack.Pop();
                }
                else if (peek is Coroutine coroutine)
                {
                    if (coroutine.MoveNext(deltaTime))
                    {
                        _stack.Push(coroutine._current);
                    }
                    else
                    {
                        _stack.Pop();
                    }

                    Update(deltaTime);
                }
                else if (peek is WaitForSeconds waitForSeconds)
                {
                    waitForSeconds.SetNextStepTime(deltaTime);
                    if (waitForSeconds.MoveNext())
                    {
                        _stack.Push(waitForSeconds.Current);
                    }
                    else
                    {
                        _stack.Pop();
                    }
                }
                else if (peek is IEnumerator enumerator)
                {
                    if (enumerator.MoveNext())
                    {
                        _stack.Push(enumerator.Current);
                    }
                    else
                    {
                        _stack.Pop();
                    }

                    Update(deltaTime);
                }
                else if (peek is IEnumerable enumerable)
                {
                    _stack.Pop();
                    _stack.Push(enumerable.GetEnumerator());
                    Update(deltaTime);
                }
                else
                {
                    throw new NotSupportedException($"{peek.GetType()} is not supported.");
                }
            }
            catch (Exception exception)
            {
                Exception = exception;
                throw;
            }
        }
    }
}