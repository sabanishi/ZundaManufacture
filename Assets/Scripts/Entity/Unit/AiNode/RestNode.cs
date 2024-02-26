using GameFramework.Core;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class RestNode:BaseUnitNode
    {
    }
    
    public class RestHandler : BaseUnitHandler<RestNode>
    {
        protected override AsyncOperationHandle SetupHandle()
        {
            var service = Services.Get<UnitActionService>();
            return service.DoRestAction(Model);
        }
    }
}