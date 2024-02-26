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
        private readonly EntityUiManagerPresenter _entityUiManagerPresenter;
        public EntityUiManagerPresenter EntityUiManager => _entityUiManagerPresenter;

        public MainGamePresenter(MainGameModel model, MainGameView view)
        {
            _model = model;
            _view = view;

            _unitStoragePresenter = new UnitStoragePresenter(model.UnitStorage, view.UnitStorage);
            _factoryStoragePresenter = new FactoryStoragePresenter(model.FactoryStorage, view.FactoryStorage);
            _entityUiManagerPresenter = new EntityUiManagerPresenter(view.EntityUiManager);
        }

        protected override void ActivateInternal(IScope scope)
        {
            _unitStoragePresenter.Activate();
            _factoryStoragePresenter.Activate();
            _entityUiManagerPresenter.Activate();

            _view.OnTmpUnitButtonClickObservable.Subscribe(_ => _model.TmpUnitCreate()).RegisterTo(scope);
            _view.OnTmpFactoryButtonClickObservable.Subscribe(_ => _model.TmpFactoryCreate()).RegisterTo(scope);
        }

        protected override void DeactivateInternal()
        {
            _unitStoragePresenter.Deactivate();
            _factoryStoragePresenter.Deactivate();
            _entityUiManagerPresenter.Deactivate();
        }
    }
}