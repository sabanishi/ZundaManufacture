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
        
        private UnitInfoStorage _unitInfoStorage;
        
        private FactoryInfoStorage _factoryInfoStorage;

        protected override UniTask InitializeInternal(CancellationToken token)
        {
            _serviceContainer = new ServiceContainer();
            _taskRunner = new TaskRunner();
            _serviceContainer.Set(_taskRunner);
            
            var layeredTime = new LayeredTime();
            _serviceContainer.Set(layeredTime);
            
            _model = MainGameModel.Create();
            _presenter = new MainGamePresenter(_model, view);
            
            //Presenterの要素のうち、シングルトンにするものをServiceContainerに登録
            _serviceContainer.Set(_presenter.EntityUiManager);
            
            var unitBodyManager = new BodyManager();
            var unitManager = new UnitManager(_taskRunner, unitBodyManager);
            _serviceContainer.Set(unitManager);
            _unitInfoStorage = ResourceManager.Instance.Load<UnitInfoStorage>("UnitInfoStorage");
            _serviceContainer.Set(_unitInfoStorage);
            var unitActionService = new UnitActionService();
            _serviceContainer.Set(unitActionService);
            
            var factoryBodyManager = new BodyManager();
            var factoryManager = new FactoryManager(_taskRunner, factoryBodyManager);
            _serviceContainer.Set(factoryManager);
            _factoryInfoStorage = ResourceManager.Instance.Load<FactoryInfoStorage>("FactoryInfoStorage");
            _serviceContainer.Set(_factoryInfoStorage);
            
            _presenter.Activate();
            return UniTask.CompletedTask;
        }

        protected override UniTask DisposeInternal(CancellationToken token)
        {
            _model.Dispose();
            _presenter.Dispose();
            _serviceContainer.Dispose();
            
            Destroy(_unitInfoStorage);
            Destroy(_factoryInfoStorage);
            return UniTask.CompletedTask;
        }

        private void Update()
        {
            _taskRunner?.Update();
        }

        private void LateUpdate()
        {
            _taskRunner?.LateUpdate();
        }
    }
}