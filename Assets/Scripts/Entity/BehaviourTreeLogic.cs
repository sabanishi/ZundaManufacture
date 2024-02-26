using GameAiBehaviour;
using GameFramework.ActorSystems;
using GameFramework.Core;

namespace Sabanishi.ZundaManufacture.Entity
{
    /// <summary>
    /// BehaviourTreeを扱うActorEntityLogicの基底クラス
    /// </summary>
    public abstract class BehaviourTreeLogic:ActorEntityLogic
    {
        private readonly EntityActor _actor;
        protected readonly BehaviourTreeController TreeController;

        protected BehaviourTreeLogic(EntityActor actor)
        {
            _actor = actor;
            TreeController = new BehaviourTreeController();
        }

        protected override void DisposeInternal()
        {
            TreeController.Dispose();
        }

        protected override void ActivateInternal(IScope scope)
        {
            SetupTree();
            BindActionHandlersInternal();
            BindBehaviourTreeController();
        }
        
        protected override void DeactivateInternal()
        {
            UnbindBehaviourTreeController();
            TreeController.ResetLinkNodeHandlers();
            TreeController.ResetConditionHandlers();
            TreeController.ResetActionNodeHandlers();
        }
        
        protected override void UpdateInternal()
        {
            var deltaTime = 0f;
            if (_actor.Body != null)
            {
                deltaTime = _actor.Body.DeltaTime;
            }

            TreeController.Update(deltaTime);
        }

        protected virtual void BindActionHandlersInternal()
        {
        }

        protected abstract void SetupTree();
        
        private void BindBehaviourTreeController()
        {
            if (!_actor.Body.IsValid) return;
            _actor.BtControllerProviderParentGimmick.AddProvider(TreeController,TreeController.Tree.name);
        }
        
        private void UnbindBehaviourTreeController()
        {
            if (!_actor.Body.IsValid) return;
            _actor.BtControllerProviderParentGimmick.RemoveProvider(TreeController);
        }
    }
}