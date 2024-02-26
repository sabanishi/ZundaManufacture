using GameFramework.Core;
using R3;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitAnimatorController:BehaviourTreeLogic
    {
        private const string SpeedKey = "Speed";
        
        private readonly UnitModel _model;
        private readonly UnitActor _actor;

        public UnitAnimatorController(UnitModel model,UnitActor actor) : base(actor)
        {
            _model = model;
            _actor = actor;
        }

        protected override void ActivateInternal(IScope scope)
        {
            base.ActivateInternal(scope);
            _actor.MoveVelocity.TakeUntil(scope).Subscribe(OnUpdateSpeed);
        }

        protected override void SetupTree()
        {
            if (_model.Info == null)
            {
                DebugLogger.LogError("UnitInfo is null");
                return;
            }
            TreeController.Setup(_model.Info.AnimationTree);
        }
        
        protected override void BindActionHandlersInternal()
        {
            TreeController.BindActionNodeHandler<AnimatorNode, AnimatorNodeHandler>(h => h.Setup(_actor.GetAnimator()));
        }

        private void OnUpdateSpeed(Vector3 velocity)
        {
            var speed = velocity.magnitude;
            TreeController.Blackboard.SetFloat(SpeedKey,speed);
        }
    }
}