namespace Sabanishi.ZundaManufacture.Entity
{
    public class FactoryPresenter:EntityPresenter
    {
        private readonly FactoryModel _model;
        private readonly FactoryActor _actor;
        
        public FactoryPresenter(FactoryModel model, FactoryActor actor):base(model,actor)
        {
            _model = model;
            _actor = actor;
        }
    }
}