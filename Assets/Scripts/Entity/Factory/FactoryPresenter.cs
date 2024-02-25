using GameFramework.ActorSystems;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class FactoryPresenter:ActorEntityLogic
    {
        private readonly FactoryModel _model;
        private readonly FactoryActor _actor;
        
        public FactoryPresenter(FactoryModel model, FactoryActor actor)
        {
            _model = model;
            _actor = actor;
        }
    }
}