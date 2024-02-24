using System.Threading;

namespace Sabanishi.ZundaManufacture.Core
{
    public class Presenter
    {
        private CancellationTokenSource _lifeScope;

        public void Activate()
        {
            if (_lifeScope != null)
            {
                _lifeScope.Cancel();
                Deactivate();
            }
            _lifeScope = new CancellationTokenSource();
            ActivateInternal(_lifeScope.Token);
        }

        public void Deactivate()
        {
            if (_lifeScope == null) return;
            _lifeScope.Cancel();
            _lifeScope = null;
            DeactivateInternal();
        }

        public void Update(float deltaTime)
        {
            UpdateInternal(deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
            LateUpdateInternal(deltaTime);
        }

        protected virtual void ActivateInternal(CancellationToken token){}
        protected virtual void DeactivateInternal(){}
        protected virtual void UpdateInternal(float deltaTime){}
        protected virtual void LateUpdateInternal(float deltaTime){}
    }
}