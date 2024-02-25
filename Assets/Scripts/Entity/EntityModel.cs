using GameFramework.Core;
using GameFramework.ModelSystems;
using R3;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class EntityModel:AutoIdModel<EntityModel>
    {
        private Subject<Vector3> _setPositionSubject;
        private Subject<Vector3> _setEulerAngleSubject;
        
        public Observable<Vector3> SetPositionObservable => _setPositionSubject;
        public Observable<Vector3> SetEulerAngleObservable => _setEulerAngleSubject;
        
        protected EntityModel(int id) : base(id)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            _setPositionSubject = new Subject<Vector3>();
            _setEulerAngleSubject = new Subject<Vector3>();
        }

        protected override void OnDeletedInternal()
        {
            _setPositionSubject.Dispose();
            _setEulerAngleSubject.Dispose();
        }
    }
}