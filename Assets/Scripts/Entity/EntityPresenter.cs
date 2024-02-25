using GameFramework.ActorSystems;
using GameFramework.Core;
using R3;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class EntityPresenter:ActorEntityLogic
    {
        private readonly EntityModel _model;
        private readonly EntityActor _actor;
        
        public EntityPresenter(EntityModel model, EntityActor actor)
        {
            _model = model;
            _actor = actor;
        }

        protected override void ActivateInternal(IScope scope)
        {
            _model.SetPositionObservable.TakeUntil(scope).Subscribe(_actor.SetPosition);
            _model.SetEulerAngleObservable.TakeUntil(scope).Subscribe(_actor.SetEulerAngle);
        }
    }
}