using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Sandbox
{
    public class User:MonoBehaviour
    {
        private CoroutineRunner _coroutineRunner;
        private RepeatableCancellationTokenSource _actionCancelTokenSource;

        private bool _isStop;
        
        private void Awake()
        {
            _coroutineRunner = new CoroutineRunner();
            _actionCancelTokenSource = new RepeatableCancellationTokenSource();

            var asyncOperationHandle = DoGoTo(new Vector3(3, 0, 0));
            
            /*UniTask.Void(async () =>
            {
                //2秒待機
                await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: _actionCancelTokenSource.Token);
                //CancelAction();
                _isStop = true;
                //2秒待機
                await UniTask.Delay(TimeSpan.FromSeconds(4), cancellationToken: _actionCancelTokenSource.Token);
                _isStop = false;
                //4秒待機
                await UniTask.Delay(TimeSpan.FromSeconds(4), cancellationToken: _actionCancelTokenSource.Token);
                _isStop = true;
                //2秒待機
                await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: _actionCancelTokenSource.Token);
                _isStop = false;
                Debug.Log(asyncOperationHandle.IsDone);
                Debug.Log(asyncOperationHandle.IsError);
            });*/
        }

        private void OnDestroy()
        {
            _coroutineRunner.Dispose();
            _actionCancelTokenSource.Dispose();;
        }

        private void Update()
        {
            if(_isStop) return;
            _coroutineRunner.Update(Time.deltaTime);
        }
        
        private AsyncOperationHandle DoGoTo(Vector3 targetPosition)
        {
            IEnumerator Routine(AsyncOperator asyncOperator)
            {
                while (true)
                {
                    if(asyncOperator.IsDone) yield break;
                    var myPosition = transform.position;
                    //自身の位置を少し移動する
                    myPosition += (targetPosition - myPosition).normalized * Time.deltaTime;
                    transform.position = myPosition;
                    
                    var distance = Vector3.Distance(myPosition, targetPosition);
                    if (distance < 0.1f)
                    {
                        asyncOperator.Completed();
                        transform.position = targetPosition;
                        break;
                    }
                    yield return null;
                }
                
                var time = 2f;
                UniTask.Void(async () =>
                {
                    while (time > 0)
                    {
                        await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: _actionCancelTokenSource.Token);
                        Debug.Log($"Wait {time} seconds");
                        time--;
                    }
                });
                yield return new WaitUntil(() => time <= 0);
                
                transform.position = new Vector3(5, 0, 5);
            }
            
            return DoActionAsync(Routine);
        }

        private void CancelAction()
        {
            _actionCancelTokenSource.Cancel();
        }

        private AsyncOperationHandle DoActionAsync(Func<AsyncOperator, IEnumerator> routine)
        {
            CancelAction();
            
            var asyncOperator = new AsyncOperator();
            void OnCompleted()
            {
                if (asyncOperator.IsDone) return;
                asyncOperator.Completed();
            }

            void OnAborted(Exception e=null)
            {
                if(asyncOperator.IsDone) return;
                asyncOperator.Aborted(e);
            }

            var ct = _actionCancelTokenSource.Token;
            ct.Register(() => OnAborted());

            StartCoroutine(routine(asyncOperator), OnCompleted, () => OnAborted(), OnAborted, ct);
            return asyncOperator.GetHandle();
        }

        private Coroutine StartCoroutine(IEnumerator enumerator, Action onComplete, Action onAborted,
            Action<Exception> onError, CancellationToken token)
        {
            return _coroutineRunner.StartCoroutine(enumerator, onComplete, onAborted, onError, token);
        }
    }
}