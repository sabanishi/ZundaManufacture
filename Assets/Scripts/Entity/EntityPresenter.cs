using GameFramework.ActorSystems;
using GameFramework.Core;
using R3;
using Sabanishi.ZundaManufacture.MainGame;

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
            _model.SetPositionObservable.Subscribe(_actor.SetPosition).RegisterTo(scope);
            _model.SetEulerAngleObservable.Subscribe(_actor.SetEulerAngle).RegisterTo(scope);
        }

        protected override void UpdateInternal()
        {
            _model.ApplyPosition(_actor.Body.Position);
            _model.ApplyEulerAngle(_actor.Body.Rotation.eulerAngles);

            var deltaTime = _actor.Body.DeltaTime;
            _model.Update(deltaTime);
        }
    }
}