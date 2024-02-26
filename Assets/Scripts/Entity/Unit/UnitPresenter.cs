using GameFramework.Core;
using R3;

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
            _model.SetMoveVelocityObservable.TakeUntil(scope).Subscribe(_actor.SetMoveVelocity);
        }
    }
}