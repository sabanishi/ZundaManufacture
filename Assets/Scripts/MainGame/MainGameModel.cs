using GameFramework.Core;
using GameFramework.ModelSystems;
using R3;
using Sabanishi.ZundaManufacture.Entity;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGameModel:SingleModel<MainGameModel>
    {
        private UnitStorageModel _unitStorage;
        private FactoryStorageModel _factoryStorage;
        private UnitSelectorModel _unitSelector;
        
        private ReactiveProperty<int> _numZunda;
        
        public UnitStorageModel UnitStorage => _unitStorage;
        public FactoryStorageModel FactoryStorage => _factoryStorage;
        public UnitSelectorModel UnitSelector => _unitSelector;
        public ReadOnlyReactiveProperty<int> NumZunda => _numZunda;
        
        private MainGameModel(object empty) : base(empty)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            _unitStorage = UnitStorageModel.Create();
            _factoryStorage = FactoryStorageModel.Create();
            _unitSelector = UnitSelectorModel.Create();
            _numZunda = new ReactiveProperty<int>();
        }

        protected override void OnDeletedInternal()
        {
            _unitStorage.Dispose();
            _factoryStorage.Dispose();
            _unitSelector.Dispose();
        }

        public void TmpUnitCreate()
        {
            _unitStorage.CreateUnit(UnitType.Kiritan);
        }
        
        public void TmpFactoryCreate()
        {
            _factoryStorage.CreateFactory(FactoryType.Rice);
        }

        public void AddNumZunda(int addition)
        {
            _numZunda.Value += addition;
        }
        
        public void SubNumZunda(int subtraction)
        {
            _numZunda.Value -= subtraction;
        }
    }
}