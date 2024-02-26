using GameFramework.Core;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class IdleNode:BaseUnitNode
    {
    }

    public class IdleHandler : BaseUnitHandler<IdleNode>
    {
        protected override AsyncOperationHandle SetupHandle()
        {
            var service = Services.Get<UnitActionService>();
            return service.DoIdleAction(Model);
        }
    }
}