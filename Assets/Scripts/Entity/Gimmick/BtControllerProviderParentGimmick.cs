using GameAiBehaviour;
using GameFramework.GimmickSystems;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    /// <summary>
    /// UnitのBehaviourTreeProviderを制御するためのGimmick
    /// </summary>
    public class BtControllerProviderParentGimmick:ActiveGimmick
    {
        [SerializeField] private Transform providerParent;
        
        protected override void ActivateInternal()
        {
            
        }

        protected override void DeactivateInternal()
        {
            if (providerParent == null) return;
            foreach (Transform child in providerParent)
            {
                Destroy(child.gameObject);
            }
        }

        public void AddProvider(BehaviourTreeController controller,string childName)
        {
            var provider = new GameObject(childName).AddComponent<BehaviourTreeControllerProvider>();
            provider.transform.SetParent(providerParent);
            provider.Set(controller);
        }

        public void RemoveProvider(BehaviourTreeController controller)
        {
            var providers = providerParent.GetComponentsInChildren<BehaviourTreeControllerProvider>();
            foreach (var provider in providers)
            {
                if (provider.BehaviourTreeController == controller)
                {
                    Destroy(provider.gameObject);
                    return;
                }
            }
        }
    }
}