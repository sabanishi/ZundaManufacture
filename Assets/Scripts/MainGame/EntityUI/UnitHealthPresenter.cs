using GameFramework.Core;
using R3;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitHealthPresenter:EntityUiPresenter
    {
        private readonly UnitHealthModel _model;
        private readonly UnitHealthView _view;
        
        public UnitHealthPresenter(UnitHealthModel model, UnitHealthView view) : base(model, view)
        {
            _model = model;
            _view = view;
            _view.SetValue(_model.NowValue.CurrentValue);
        }

        protected override void ActivateInternal(IScope scope)
        {
            base.ActivateInternal(scope);
            _model.NowValue.Subscribe(_view.SetValue).RegisterTo(scope);
        }
    }
}