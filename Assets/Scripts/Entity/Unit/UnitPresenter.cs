using GameFramework.Core;
using R3;
using Sabanishi.ZundaManufacture.MainGame;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitPresenter:EntityPresenter
    {
        private readonly UnitModel _model;
        private readonly UnitActor _actor;
        
        public UnitPresenter(UnitModel model, UnitActor actor):base(model, actor)
        {
            _model = model;
            _actor = actor;
        }

        protected override void ActivateInternal(IScope scope)
        {
            base.ActivateInternal(scope);
            _model.SetMoveVelocityObservable.Subscribe(_actor.SetMoveVelocity).ScopeTo(scope);
            _model.SetEulerAngleObservable.Subscribe(_actor.LookTargetPos).ScopeTo(scope);
            _model.IsWaitCommand.Subscribe(_actor.SetTapRendererActive).ScopeTo(scope);
            RegisterUi(_model.Health);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();
            UnregisterUi(_model.Health);
        }

        private void RegisterUi(EntityUiModel uiModel)
        {
            var uiStorage = Services.Get<EntityUiManagerPresenter>();
            var targetTransform = _actor.Body.Transform;
            uiStorage.CreateElement(uiModel,targetTransform);
        }
        
        private void UnregisterUi(EntityUiModel uiModel)
        {
            var uiStorage = Services.Get<EntityUiManagerPresenter>();
            uiStorage.DestroyElement(uiModel);
        }
    }
}