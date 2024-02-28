using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitSelectorView:MonoBehaviour
    {
        [SerializeField]private Camera unitCamera;
        public Camera UnitCamera => unitCamera;
    }
}