using GameFramework.Core;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class RandomWalkNode:BaseUnitNode
    {
    }

    public class RandomWalkHandler :BaseUnitHandler<RandomWalkNode>
    {
        protected override AsyncOperationHandle SetupHandle()
        {
            var service = Services.Get<UnitActionService>();
            return service.DoRandomWalkAction(Model);
        }
    }
}