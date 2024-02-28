using GameFramework.Core;
using GameFramework.LogicSystems;

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
        }
        
        protected override void DeactivateInternal()
        {
            _tapChecker.Deactivate();
        }
    }
}