using System.Threading;
using Cysharp.Threading.Tasks;
using Sabanishi.ScreenSystem;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGameScreen:BaseScreen
    {
        [SerializeField] private MainGameView view;

        private MainGameModel _model;
        private MainGamePresenter _presenter;

        protected override UniTask InitializeInternal(CancellationToken token)
        {
            _model = MainGameModel.Create();
            _presenter = new MainGamePresenter(_model, view);
            
            _presenter.Activate();
            return UniTask.CompletedTask;
        }

        protected override UniTask DisposeInternal(CancellationToken token)
        {
            _model.Dispose();
            _presenter.Dispose();
            return UniTask.CompletedTask;
        }
    }
}