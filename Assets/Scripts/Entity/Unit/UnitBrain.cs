using GameAiBehaviour;
using GameFramework.ActorSystems;
using GameFramework.Core;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitBrain : ActorEntityLogic
    {
        private readonly UnitModel _model;
        private readonly UnitActor _actor;

        private readonly BehaviourTreeController _btController;

        public UnitBrain(UnitModel model, UnitActor actor)
        {
            _model = model;
            _actor = actor;
            _btController = new BehaviourTreeController();
        }

        protected override void DisposeInternal()
        {
            _btController.Dispose();
        }

        protected override void ActivateInternal(IScope scope)
        {
            if (_model.Info == null)
            {
                DebugLogger.LogError("UnitInfo is null");
                return;
            }

            _btController.Setup(_model.Info.AiTree);
            BindActionHandlers();
            BindBehaviourTreeController();
        }

        protected override void DeactivateInternal()
        {
            UnbindBehaviourTreeController();
            _btController.ResetLinkNodeHandlers();
            _btController.ResetConditionHandlers();
            _btController.ResetActionNodeHandlers();
        }

        private void BindActionHandlers()
        {
            _btController.BindActionNodeHandler<RandomWalkNode, RandomWalkHandler>(h => h.Setup(_model));
        }

        private void BindBehaviourTreeController()
        {
            if (!_actor.Body.IsValid) return;
            if (!_actor.Body.GameObject.TryGetComponent<BehaviourTreeControllerProvider>(out var provider))
            {
                provider = _actor.Body.GameObject.AddComponent<BehaviourTreeControllerProvider>();
            }

            provider.Set(_btController);
        }

        private void UnbindBehaviourTreeController()
        {
            if (!_actor.Body.IsValid) return;
            if (!_actor.Body.GameObject.TryGetComponent<BehaviourTreeControllerProvider>(out var provider)) return;
            provider.Set(null);
        }
        
        protected override void UpdateInternal()
        {
            var deltaTime = 0f;
            if (_actor.Body != null)
            {
                deltaTime = _actor.Body.DeltaTime;
            }

            _btController.Update(deltaTime);
        }
    }
}