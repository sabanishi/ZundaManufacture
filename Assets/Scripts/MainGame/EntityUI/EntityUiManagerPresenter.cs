using System.Collections.Generic;
using GameFramework.Core;
using GameFramework.LogicSystems;
using GameFramework.TaskSystems;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class EntityUiManagerPresenter : Logic
    {
        private readonly EntityUiManagerView _view;
        private readonly Dictionary<EntityUiModel, EntityUiPresenter> _elements;

        public EntityUiManagerPresenter(EntityUiManagerView view)
        {
            _view = view;
            _elements = new Dictionary<EntityUiModel, EntityUiPresenter>();
        }

        protected override void ActivateInternal(IScope scope)
        {
        }

        protected override void DeactivateInternal()
        {
            OnClearElement();
        }

        public void CreateElement(EntityUiModel elementModel, Transform target)
        {
            EntityUiPresenter presenter;
            switch (elementModel)
            {
                case UnitHealthModel unitHealthModel:
                    presenter = new UnitHealthPresenter(unitHealthModel, _view.CreateUnitHealthView(target));
                    break;
                default:
                    DebugLogger.LogError("EntityUiModelの型が不正です:" + elementModel.GetType().Name);
                    return;
            }

            presenter.Setup(_view.WorldCamera);
            presenter.Activate();
            _elements.Add(elementModel, presenter);
            
            //TaskRunnerに登録
            var taskRunner = Services.Get<TaskRunner>();
            taskRunner.Register(presenter,TaskOrder.Ui);
        }

        public void DestroyElement(EntityUiModel elementModel, bool isRemove = true)
        {
            if (_elements.TryGetValue(elementModel, out var presenter))
            {
                presenter.Dispose();
                if (presenter.View != null)
                {
                    GameObject.Destroy(presenter.View.gameObject);
                    if (isRemove)
                    {
                        _elements.Remove(elementModel);
                    }
                }
            }
        }

        private void OnClearElement()
        {
            foreach (var model in _elements.Keys)
            {
                DestroyElement(model, false);
            }

            _elements.Clear();
        }
    }
}