using System;
using System.Collections;
using System.Threading;
using GameFramework.Core;
using GameFramework.CoroutineSystems;
using GameFramework.ModelSystems;
using R3;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class EntityModel:AutoIdModel<EntityModel>
    {
        private CoroutineRunner _coroutineRunner;
        private DisposableScope _actionScope;
        
        private Subject<Vector3> _setPositionSubject;
        private Subject<Vector3> _setEulerAngleSubject;
        
        public Observable<Vector3> SetPositionObservable => _setPositionSubject;
        public Observable<Vector3> SetEulerAngleObservable => _setEulerAngleSubject;
        
        public Vector3 Position { get; private set; }
        public Vector3 EulerAngle { get; private set; }
        
        
        protected EntityModel(int id) : base(id)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            _coroutineRunner = new CoroutineRunner().ScopeTo(scope);
            _setPositionSubject = new Subject<Vector3>().ScopeTo(scope);
            _setEulerAngleSubject = new Subject<Vector3>().ScopeTo(scope);
            _actionScope = new DisposableScope().ScopeTo(scope);
        }
        
        public virtual void Update(float deltaTime)
        {
            _coroutineRunner.Update();
        }
        
        public void ApplyPosition(Vector3 position)
        {
            Position = position;
        }
        
        public void ApplyEulerAngle(Vector3 eulerAngle)
        {
            EulerAngle = eulerAngle;
        }
        
        /// <summary>
        /// 任意のコルーチンを実行し、その完了フラグを受け取るAsyncOperationHandleを返す
        /// </summary>
        /// <param name="routine">返り値となるAsyncOperatorを引数にとり、行いたいコルーチンを返す関数</param>
        protected AsyncOperationHandle DoActionAsync(Func<AsyncOperator,IEnumerator> routine)
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

            var ct = GetActionToken();
            ct.Register(() => OnAborted());

            _coroutineRunner.StartCoroutine(routine(asyncOperator),
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
            _actionScope?.Clear();
        }
        
        protected CancellationToken GetActionToken()
        {
            return _actionScope.Token;
        }
    }
}