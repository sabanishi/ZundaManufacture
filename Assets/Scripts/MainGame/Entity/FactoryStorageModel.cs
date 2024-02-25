using GameFramework.Core;
using GameFramework.ModelSystems;
using ObservableCollections;
using Sabanishi.ZundaManufacture.Entity;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class FactoryStorageModel:SingleModel<FactoryStorageModel>
    {
        private ObservableList<FactoryModel> _factories;
        public IObservableCollection<FactoryModel> Factories => _factories;
        
        private FactoryStorageModel(object empty) : base(empty)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            _factories = new ObservableList<FactoryModel>();
        }
        
        protected override void OnDeletedInternal()
        {
            _factories.Clear();
        }
        
        public void CreateFactory(FactoryType type)
        {
            var infoStorage = Services.Get<FactoryInfoStorage>();
            var info = infoStorage.GetInfo(type);
            var model = FactoryModel.Create(info);
            _factories.Add(model);
        }
    }
}