using GameFramework.Core;
using GameFramework.LogicSystems;
using R3;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGamePresenter:Logic
    {
        private readonly MainGameModel _model;
        private readonly MainGameView _view;
        
        private readonly UnitStoragePresenter _unitStoragePresenter;
        
        public MainGamePresenter(MainGameModel model, MainGameView view)
        {
            _model = model;
            _view = view;
            
            _unitStoragePresenter = new UnitStoragePresenter(model.UnitStorage, view.UnitStorage);
        }

        protected override void ActivateInternal(IScope scope)
        {
            _view.OnTmpButtonClickObservable.TakeUntil(scope).Subscribe(_=>_model.TmpCreate());
            _unitStoragePresenter.Activate();
        }
        
        protected override void DeactivateInternal()
        {
            _unitStoragePresenter.Deactivate();
        }
    }
}