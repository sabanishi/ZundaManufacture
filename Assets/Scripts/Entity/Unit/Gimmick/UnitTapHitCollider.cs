using GameFramework.GimmickSystems;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitTapHitCollider:ActiveGimmick
    {
        [SerializeField] private GameObject root;
        [SerializeField] private GameObject rendererObject;
        protected override void ActivateInternal()
        {
            root.SetActive(true);
        }

        protected override void DeactivateInternal()
        {
            root.SetActive(false);
        }
        
        public void SetRendererActive(bool active)
        {
            rendererObject.SetActive(active);
        }
    }
}