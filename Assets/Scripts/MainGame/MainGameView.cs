using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGameView:MonoBehaviour
    {
        [SerializeField] private Button tmpUnitButton;
        [SerializeField] private Button tmpFactoryButton;
        [SerializeField] private UnitStorageView unitStorage;
        [SerializeField]private FactoryStorageView factoryStorage;

        public Observable<Unit> OnTmpUnitButtonClickObservable => tmpUnitButton.SafeOnClickAsObservable();
        public Observable<Unit> OnTmpFactoryButtonClickObservable => tmpFactoryButton.SafeOnClickAsObservable();
        public UnitStorageView UnitStorage => unitStorage;
        public FactoryStorageView FactoryStorage => factoryStorage;
    }
}