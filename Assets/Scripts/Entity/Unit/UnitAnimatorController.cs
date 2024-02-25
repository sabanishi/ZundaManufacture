using GameAiBehaviour;
using GameFramework.ActorSystems;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitAnimatorController:ActorEntityLogic
    {
        private readonly UnitModel _model;
        private readonly UnitActor _actor;

        private readonly BehaviourTreeController _btController;
        
        public UnitAnimatorController(UnitModel model, UnitActor actor)
        {
            _model = model;
            _actor = actor;
            _btController = new BehaviourTreeController();
        }

        protected override void DisposeInternal()
        {
            _btController.Dispose();
        }
    }
}