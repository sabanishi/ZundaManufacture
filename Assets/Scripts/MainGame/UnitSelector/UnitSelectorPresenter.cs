using GameFramework.Core;
using GameFramework.LogicSystems;
using R3;
using Sabanishi.ZundaManufacture.Entity;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitSelectorPresenter : Logic
    {
        private readonly UnitSelectorModel _model;
        private readonly UnitSelectorView _view;
        
        private readonly UniTapChecker _tapChecker;
        
        public UnitSelectorPresenter(UnitSelectorModel model, UnitSelectorView view)
        {
            _model = model;
            _view = view;
            _tapChecker = new UniTapChecker(_view.UnitCamera);
        }

        protected override void ActivateInternal(IScope scope)
        {
            _tapChecker.Activate();

            _model.IsOpen.Where(x=>x).Subscribe(_=>_view.Open()).ScopeTo(scope);
            _model.IsOpen.Where(x=>!x).Subscribe(_=>_view.Close()).ScopeTo(scope);
            _view.OnClickCancelButtonAsObservable.Subscribe(_ => _model.SetSelectedUnit(null)).ScopeTo(scope);
            _tapChecker.TapObservable.Subscribe(OnTapUnit).ScopeTo(scope);
            
            _model.SetSelectedUnit(null);
        }
        
        protected override void DeactivateInternal()
        {
            _tapChecker.Deactivate();
        }
        
        /// <summary>
        /// ユーザーがUnitをタップした事をModelに伝える
        /// </summary>
        private void OnTapUnit(GameObject unitObject)
        {
            DebugLogger.Log("OnTapUnit");
            var manager = Services.Get<UnitManager>();
            if (manager.TryGetModelFromGameObject(unitObject, out var unitModel))
            {
                var cameraPos = _view.UnitCamera.gameObject.transform.position;
                _model.SetSelectedUnit(unitModel,cameraPos);
            }
        }
    }
}