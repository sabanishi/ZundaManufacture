using GameFramework.ActorSystems;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitPresenter:ActorEntityLogic
    {
        private readonly UnitModel _model;
        private readonly UnitActor _actor;
        
        public UnitPresenter(UnitModel model, UnitActor actor)
        {
            _model = model;
            _actor = actor;
        }
    }
}