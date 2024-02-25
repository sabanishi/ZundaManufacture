namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitModel:EntityModel
    {
        private UnitInfo _info;
        public UnitInfo Info => _info;
        
        private UnitModel(int id) : base(id)
        {
        }

        public static UnitModel Create(UnitInfo info)
        {
            var model = Create<UnitModel>();
            model.Setup(info);
            return model;
        }

        private void Setup(UnitInfo info)
        {
            _info = info;
        }
    }
}