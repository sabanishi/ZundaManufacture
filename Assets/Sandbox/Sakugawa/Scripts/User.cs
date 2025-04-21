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

            var asyncOperationHandle = DoGoTo(new Vector3(5, 0, 0));
            
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
            _coroutineRunner.Update(Time.deltaTime*0.5f);
        }
        
        private AsyncOperationHandle DoGoTo(Vector3 targetPosition)
        {
            IEnumerator Routine(AsyncOperator asyncOperator,CoroutineTimer timer)
            {
                while (true)
                {
                    if(asyncOperator.IsDone) yield break;
                    var myPosition = transform.position;
                    //自身の位置を少し移動する
                    myPosition += (targetPosition - myPosition).normalized * timer.DeltaTime;
                    transform.position = myPosition;
                    
                    var distance = Vector3.Distance(myPosition, targetPosition);
                    if (distance < 0.01f)
                    {
                        asyncOperator.Completed();
                        transform.position = targetPosition;
                        break;
                    }
                    yield return null;
                }
                
                var time = 5f;
                yield return new WaitForSeconds(time);
                
                transform.position = new Vector3(20, 0, 20);
            }
            
            return DoActionAsync(Routine);
        }

        private void CancelAction()
        {
            _actionCancelTokenSource.Cancel();
        }

        private AsyncOperationHandle DoActionAsync(Func<AsyncOperator, CoroutineTimer,IEnumerator> routine)
        {
            CancelAction();
            
            var asyncOperator = new AsyncOperator();
            var timer = new CoroutineTimer();
            
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

            StartCoroutine(routine(asyncOperator,timer), timer, OnCompleted, () => OnAborted(), OnAborted, ct);
            return asyncOperator.GetHandle();
        }

        private Coroutine StartCoroutine(IEnumerator enumerator, CoroutineTimer timer, Action onComplete, Action onAborted,
            Action<Exception> onError, CancellationToken token)
        {
            return _coroutineRunner.StartCoroutine(enumerator, timer, onComplete, onAborted, onError, token);
        }
    }
}