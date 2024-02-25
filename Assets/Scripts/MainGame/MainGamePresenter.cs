using GameFramework.Core;
using GameFramework.LogicSystems;
using R3;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGamePresenter : Logic
    {
        private readonly MainGameModel _model;
        private readonly MainGameView _view;

        private readonly UnitStoragePresenter _unitStoragePresenter;
        private readonly FactoryStoragePresenter _factoryStoragePresenter;

        public MainGamePresenter(MainGameModel model, MainGameView view)
        {
            _model = model;
            _view = view;

            _unitStoragePresenter = new UnitStoragePresenter(model.UnitStorage, view.UnitStorage);
            _factoryStoragePresenter = new FactoryStoragePresenter(model.FactoryStorage, view.FactoryStorage);
        }

        protected override void ActivateInternal(IScope scope)
        {
            _unitStoragePresenter.Activate();
            _factoryStoragePresenter.Activate();

            _view.OnTmpUnitButtonClickObservable.TakeUntil(scope).Subscribe(_ => _model.TmpUnitCreate());
            _view.OnTmpFactoryButtonClickObservable.TakeUntil(scope).Subscribe(_ => _model.TmpFactoryCreate());
        }

        protected override void DeactivateInternal()
        {
            _unitStoragePresenter.Deactivate();
            _factoryStoragePresenter.Deactivate();
        }
    }
}