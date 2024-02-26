using GameAiBehaviour;
using GameFramework.Core;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class RandomWalkNode:HandleableActionNode
    {
    }

    public class RandomWalkHandler : ActionNodeHandler<RandomWalkNode>
    {
        private UnitModel _model;
        private AsyncOperationHandle _handle;

        public void Setup(UnitModel model)
        {
            _model = model;
        }

        protected override bool OnEnterInternal(RandomWalkNode node)
        {
            if (_model == null) return false;

            var service = Services.Get<UnitActionService>();
            _handle = service.DoRandomWalkAction(_model);
            return true;
        }
        
        protected override IActionNodeHandler.State OnUpdateInternal(RandomWalkNode node)
        {
            if (_handle.IsDone)
            {
                if (_handle.IsError) return IActionNodeHandler.State.Failure;
                return IActionNodeHandler.State.Success;
            }
            return IActionNodeHandler.State.Running;
        }
    }
}