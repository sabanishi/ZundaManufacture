using GameAiBehaviour;
using GameFramework.Core;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class BaseUnitNode:HandleableActionNode
    {
    }
    
    public abstract class BaseUnitHandler<T> : ActionNodeHandler<T> where T : BaseUnitNode
    {
        protected UnitModel Model;
        protected AsyncOperationHandle Handle;

        public void Setup(UnitModel model)
        {
            Model = model;
        }
        
        protected override bool OnEnterInternal(T node)
        {
            if (Model == null) return false;
            
            Handle = SetupHandle();
            return true;
        }

        protected abstract AsyncOperationHandle SetupHandle();
        protected override IActionNodeHandler.State OnUpdateInternal(T node)
        {
            if (Handle.IsDone)
            {
                if (Handle.IsError) return IActionNodeHandler.State.Failure;
                return IActionNodeHandler.State.Success;
            }
            return IActionNodeHandler.State.Running;
        }
    }
}