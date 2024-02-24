using System.Threading;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Core
{
    public class View:MonoBehaviour
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