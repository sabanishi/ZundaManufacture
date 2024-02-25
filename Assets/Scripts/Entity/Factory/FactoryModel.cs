using GameFramework.ModelSystems;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class FactoryModel:SingleModel<FactoryModel>
    {
        private FactoryInfo _info;
        public FactoryInfo Info => _info;
        
        private FactoryModel(object empty) : base(empty)
        {
        }
        
        public static FactoryModel Create(FactoryInfo info)
        {
            var model = Create();
            model.Setup(info);
            return model;
        }
        
        private void Setup(FactoryInfo info)
        {
            _info = info;
        }
    }
}