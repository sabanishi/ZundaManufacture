using GameFramework.Core;
using GameFramework.ModelSystems;
using Sabanishi.ZundaManufacture.Entity;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGameModel:SingleModel<MainGameModel>
    {
        private UnitStorageModel _unitStorage;
        public UnitStorageModel UnitStorage => _unitStorage;
        
        private MainGameModel(object empty) : base(empty)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            _unitStorage = UnitStorageModel.Create();
        }

        protected override void OnDeletedInternal()
        {
            _unitStorage.Dispose();
        }

        public void TmpCreate()
        {
            _unitStorage.CreateUnit(UnitType.Kiritan);
        }
    }
}