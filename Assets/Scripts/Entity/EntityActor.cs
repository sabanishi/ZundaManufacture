using System;
using System.Collections;
using System.Threading;
using GameFramework.ActorSystems;
using GameFramework.BodySystems;
using GameFramework.Core;
using GameFramework.CoroutineSystems;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class EntityActor:Actor
    {
        private DisposableScope _actionScope;
        private CoroutineRunner _coroutineRunner;
        
        protected EntityActor(Body body) : base(body)
        {
        }

        protected override void ActivateInternal(IScope scope)
        {
            _actionScope = new DisposableScope().ScopeTo(scope);
            _coroutineRunner = new CoroutineRunner().ScopeTo(scope);
        }

        protected override void UpdateInternal()
        {
            _coroutineRunner.Update();
        }

        public void SetPosition(Vector3 pos)
        {
            Body.Transform.position = pos;
        }

        public void SetEulerAngle(Vector3 eulerAngle)
        {
            Body.Transform.eulerAngles = eulerAngle;
        }
        
        /// <summary>
        /// 任意のコルーチンを実行し、その完了フラグを受け取るAsyncOperationHandleを返す
        /// </summary>
        protected AsyncOperationHandle DoActionAsync(IEnumerator enumerator)
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
                if (asyncOperator.IsDone) return;
                asyncOperator.Aborted(e);
            }

            var ct = _actionScope.Token;
            ct.Register(() => OnAborted());

            _coroutineRunner.StartCoroutine(enumerator,
                OnCompleted,
                () => OnAborted(),
                OnAborted,
                ct);
            return asyncOperator;
        }
        
        /// <summary>
        /// 現在実行中のコルーチンActionをキャンセルする
        /// </summary>
        private void CancelAction()
        {
            _actionScope.Clear();
        }
        
        protected CancellationToken GetActionToken()
        {
            return _actionScope.Token;
        }
    }
}