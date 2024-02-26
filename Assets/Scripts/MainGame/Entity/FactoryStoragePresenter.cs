using System.Collections.Generic;
using GameFramework.ActorSystems;
using GameFramework.Core;
using GameFramework.LogicSystems;
using ObservableCollections;
using R3;
using Sabanishi.ZundaManufacture.Entity;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class FactoryStoragePresenter:Logic
    {
        private readonly FactoryStorageModel _model;
        private readonly FactoryStorageView _view;
        private readonly Dictionary<FactoryModel,ActorEntity> _factoryDict;
        
        public FactoryStoragePresenter(FactoryStorageModel model, FactoryStorageView view)
        {
            _model = model;
            _view = view;
            _factoryDict = new Dictionary<FactoryModel, ActorEntity>();
        }

        protected override void ActivateInternal(IScope scope)
        {
            _model.Factories.ObserveAdd().Subscribe(x=>OnCreateFactory(x.Value)).RegisterTo(scope);
            _model.Factories.ObserveRemove().Subscribe(x=>OnDestroyFactory(x.Value)).RegisterTo(scope);
            _model.Factories.ObserveReset().Subscribe(_=>OnClearFactories()).RegisterTo(scope);
        }
        
        private void OnCreateFactory(FactoryModel factoryModel)
        {
            var manager = Services.Get<FactoryManager>();
            var entity = manager.GetOrCreateEntity(factoryModel);
            manager.AttachActor(factoryModel);
            var body = entity.GetBody();
            body.Transform.SetParent(_view.transform);
            _factoryDict.Add(factoryModel, entity);
        }
        
        private void OnDestroyFactory(FactoryModel factoryModel,bool isRemove=true)
        {
            var manager = Services.Get<FactoryManager>();
            if (_factoryDict.ContainsKey(factoryModel))
            {
                manager.DetachActor(factoryModel);
                manager.DisposeActorEntity(factoryModel);
                if (isRemove)
                {
                    _factoryDict.Remove(factoryModel);
                }
            }
        }
        
        private void OnClearFactories()
        {
            foreach (var model in _factoryDict.Keys)
            {
                OnDestroyFactory(model,false);
            }
            _factoryDict.Clear();
        }
    }
}