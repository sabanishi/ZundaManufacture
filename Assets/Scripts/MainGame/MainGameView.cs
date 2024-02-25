using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGameView:MonoBehaviour
    {
        [SerializeField] private UnitStorageView unitStorage;
        
        public UnitStorageView UnitStorage => unitStorage;
    }
}