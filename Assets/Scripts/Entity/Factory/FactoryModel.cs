namespace Sabanishi.ZundaManufacture.Entity
{
    public class FactoryModel:EntityModel
    {
        private FactoryInfo _info;
        public FactoryInfo Info => _info;
        
        private FactoryModel(int id) : base(id)
        {
        }
        
        public static FactoryModel Create(FactoryInfo info)
        {
            var model = Create<FactoryModel>();
            model.Setup(info);
            return model;
        }
        
        private void Setup(FactoryInfo info)
        {
            _info = info;
        }
    }
}