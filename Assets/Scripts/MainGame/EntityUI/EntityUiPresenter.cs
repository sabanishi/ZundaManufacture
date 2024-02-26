using GameFramework.Core;
using GameFramework.LogicSystems;
using R3;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public abstract class EntityUiPresenter:Logic
    {
        private readonly EntityUiModel _model;
        private readonly EntityUiView _view;
        public EntityUiView View => _view;
        
        protected EntityUiPresenter(EntityUiModel model, EntityUiView view)
        {
            _model = model;
            _view = view;
            _view.SetOffset(_model.Offset.CurrentValue);
        }

        protected override void ActivateInternal(IScope scope)
        {
            _model.Offset.Subscribe(_view.SetOffset).RegisterTo(scope);
        }

        public void Setup(Camera worldCamera)
        {
            _view.Setup(worldCamera);
        }

        protected override void UpdateInternal()
        {
            _view.UpdateEntityPosition();
        }
    }
}