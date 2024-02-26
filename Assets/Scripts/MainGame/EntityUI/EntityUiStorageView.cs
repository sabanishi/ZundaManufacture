using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class EntityUiStorageView:MonoBehaviour
    {
        [SerializeField] private Camera worldCamera;
        [SerializeField] private UnitHealthView unitHealthViewPrefab;
        
        public Camera WorldCamera => worldCamera;

        public UnitHealthView CreateUnitHealthView(Transform target)
        {
            return Instantiate(unitHealthViewPrefab,target);
        }
    }
}