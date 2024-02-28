using GameFramework.Core;
using R3;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitBrain : BehaviourTreeLogic
    {
        private const string HealthKey = "Health";
        private const string IsWaitCommandKey = "IsWaitCommand";
        
        private readonly UnitModel _model;
        private readonly UnitActor _actor;
        
        public UnitBrain(UnitModel model, UnitActor actor):base(actor)
        {
            _model = model;
            _actor = actor;
        }

        protected override void ActivateInternal(IScope scope)
        {
            base.ActivateInternal(scope);
            _model.Health.NowValue.Subscribe(OnUpdateHealth).ScopeTo(scope);
            _model.IsWaitCommand.Subscribe(OnUpdateWaitCommand).ScopeTo(scope);
        }

        protected override void BindActionHandlersInternal()
        {
            TreeController.BindActionNodeHandler<RandomWalkNode, RandomWalkHandler>(h => h.Setup(_model));
            TreeController.BindActionNodeHandler<IdleNode, IdleHandler>(h => h.Setup(_model));
            TreeController.BindActionNodeHandler<RestNode,RestHandler>(h => h.Setup(_model));
        }

        protected override void SetupTree()
        {
            if (_model.Info == null)
            {
                DebugLogger.LogError("UnitInfo is null");
                return;
            }
            TreeController.Setup(_model.Info.AiTree);
        }
        
        private void OnUpdateHealth(float health)
        {
            TreeController.Blackboard.SetFloat(HealthKey,health);
        }

        private void OnUpdateWaitCommand(bool isWaitCommand)
        {
            DebugLogger.Log("OnUpdateWaitCommand:"+isWaitCommand);
            TreeController.Blackboard.SetBoolean(IsWaitCommandKey,isWaitCommand);
        }
    }
}