using GameFramework.ModelSystems;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitModel:AutoIdModel<UnitModel>
    {
        private UnitInfo _info;
        public UnitInfo Info => _info;
        
        public UnitModel(int id) : base(id)
        {
        }

        public static UnitModel Create(UnitInfo info)
        {
            var model = Create();
            model.Setup(info);
            return model;
        }

        private void Setup(UnitInfo info)
        {
            _info = info;
        }
    }
}