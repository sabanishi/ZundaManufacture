using System.Threading;
using Cysharp.Threading.Tasks;
using GameFramework.BodySystems;
using GameFramework.Core;
using GameFramework.TaskSystems;
using Sabanishi.ScreenSystem;
using Sabanishi.ZundaManufacture.Entity;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGameScreen:BaseScreen
    {
        [SerializeField] private MainGameView view;

        private MainGameModel _model;
        private MainGamePresenter _presenter;

        private ServiceContainer _serviceContainer;
        private TaskRunner _taskRunner;
        private BodyManager _unitBodyManager;
        private UnitInfoStorage _unitInfoStorage;

        protected override UniTask InitializeInternal(CancellationToken token)
        {
            _model = MainGameModel.Create();
            _presenter = new MainGamePresenter(_model, view);

            _serviceContainer = new ServiceContainer();
            
            _taskRunner = new TaskRunner();
            _serviceContainer.Set(_taskRunner);
            
            _unitBodyManager = new BodyManager();
            _serviceContainer.Set(_unitBodyManager);
            var unitManager = new UnitManager(_taskRunner, _unitBodyManager);
            _serviceContainer.Set(unitManager);
            
            _unitInfoStorage = ResourceManager.Instance.Load<UnitInfoStorage>("UnitInfoStorage");
            _serviceContainer.Set(_unitInfoStorage);
            
            _presenter.Activate();
            return UniTask.CompletedTask;
        }

        protected override UniTask DisposeInternal(CancellationToken token)
        {
            _model.Dispose();
            _presenter.Dispose();
            
            Destroy(_unitInfoStorage);
            _serviceContainer.Dispose();
            _taskRunner.Dispose();
            _unitBodyManager.Dispose();
            return UniTask.CompletedTask;
        }
    }
}