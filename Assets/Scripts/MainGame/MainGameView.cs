using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGameView:MonoBehaviour
    {
        [SerializeField] private Button tmpButton;
        [SerializeField] private UnitStorageView unitStorage;

        public Observable<Unit> OnTmpButtonClickObservable => tmpButton.SafeOnClickAsObservable();
        public UnitStorageView UnitStorage => unitStorage;
    }
}