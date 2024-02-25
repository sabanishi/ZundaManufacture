using System.Collections.Generic;
using GameFramework.ActorSystems;
using GameFramework.Core;
using GameFramework.LogicSystems;
using ObservableCollections;
using R3;
using Sabanishi.ZundaManufacture.Entity;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitStoragePresenter:Logic
    {
        private readonly UnitStorageModel _model;
        private readonly UnitStorageView _view;
        private readonly Dictionary<UnitModel, ActorEntity> _unitDict;
        
        public UnitStoragePresenter(UnitStorageModel model, UnitStorageView view)
        {
            _model = model;
            _view = view;
            _unitDict = new Dictionary<UnitModel, ActorEntity>();
        }

        protected override void ActivateInternal(IScope scope)
        {
            _model.Units.ObserveAdd().TakeUntil(scope).Subscribe(x => OnCreateUnit(x.Value));
            _model.Units.ObserveRemove().TakeUntil(scope).Subscribe(x => OnDestroyUnit(x.Value));
            _model.Units.ObserveReset().TakeUntil(scope).Subscribe(_ => OnClearUnits());
        }

        private void OnCreateUnit(UnitModel unitModel)
        {
            var manager = Services.Get<UnitManager>();
            var entity = manager.GetOrCreateEntity(unitModel);
            manager.AttachActor(unitModel);
            var body = entity.GetBody();
            body.Transform.SetParent(_view.transform);
            _unitDict.Add(unitModel, entity);
        }
        
        private void OnDestroyUnit(UnitModel unitModel,bool isRemove=true)
        {
            var manager = Services.Get<UnitManager>();
            if (_unitDict.ContainsKey(unitModel))
            {
                manager.DetachActor(unitModel);
                manager.DisposeActorEntity(unitModel);
                if (isRemove)
                {
                    _unitDict.Remove(unitModel);
                }
            }
        }
        
        private void OnClearUnits()
        {
            foreach (var model in _unitDict.Keys)
            {
                OnDestroyUnit(model,false);
            }
            _unitDict.Clear();
        }
    }
}