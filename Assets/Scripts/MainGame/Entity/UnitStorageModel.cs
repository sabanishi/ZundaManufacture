using GameFramework.Core;
using GameFramework.ModelSystems;
using ObservableCollections;
using Sabanishi.ZundaManufacture.Entity;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitStorageModel:SingleModel<UnitStorageModel>
    {
        private ObservableList<UnitModel> _units;
        public IObservableCollection<UnitModel> Units => _units;
        
        private UnitStorageModel(object empty) : base(empty)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            _units = new ObservableList<UnitModel>();
        }

        protected override void OnDeletedInternal()
        {
            _units.Clear();
        }

        public void CreateUnit(UnitType type)
        {
            var infoStorage = Services.Get<UnitInfoStorage>();
            var info = infoStorage.GetInfo(type);
            var model = UnitModel.Create(info);
            _units.Add(model);
        }
    }
}