using System.Threading;

namespace Sabanishi.ZundaManufacture.Core
{
    public class Model
    {
        public void OnActivate(CancellationToken token)
        {
            OnActivateInternal(token);
        }
        
        public void OnDeactivate()
        {
            OnDeactivateInternal();
        }
        
        protected virtual void OnActivateInternal(CancellationToken token){}
        protected virtual void OnDeactivateInternal(){}
    }
}