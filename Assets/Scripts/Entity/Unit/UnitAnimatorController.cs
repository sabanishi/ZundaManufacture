using GameFramework.Core;
using R3;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitAnimatorController:BehaviourTreeLogic
    {
        private const string SpeedKey = "Speed";
        private const string IsRestingKey = "IsResting";
        
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
            _actor.MoveVelocity.Subscribe(OnUpdateSpeed).RegisterTo(scope);
           _model.IsResting.Subscribe(OnUpdateHealth).RegisterTo(scope);
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

        protected void OnUpdateHealth(bool isResting)
        {
            TreeController.Blackboard.SetBoolean(IsRestingKey,isResting);
        }
    }
}