using GameFramework.Core;
using GameFramework.ModelSystems;
using Sabanishi.ZundaManufacture.Entity;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGameModel:SingleModel<MainGameModel>
    {
        private UnitStorageModel _unitStorage;
        private FactoryStorageModel _factoryStorage;
        
        public UnitStorageModel UnitStorage => _unitStorage;
        public FactoryStorageModel FactoryStorage => _factoryStorage;
        
        private MainGameModel(object empty) : base(empty)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            _unitStorage = UnitStorageModel.Create();
            _factoryStorage = FactoryStorageModel.Create();
        }

        protected override void OnDeletedInternal()
        {
            _unitStorage.Dispose();
            _factoryStorage.Dispose();
        }

        public void TmpUnitCreate()
        {
            _unitStorage.CreateUnit(UnitType.Kiritan);
        }
        
        public void TmpFactoryCreate()
        {
            _factoryStorage.CreateFactory(FactoryType.Rice);
        }
    }
}