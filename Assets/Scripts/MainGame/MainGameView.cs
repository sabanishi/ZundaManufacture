using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class MainGameView : MonoBehaviour
    {
        [SerializeField] private Button tmpUnitButton;
        [SerializeField] private Button tmpFactoryButton;
        [SerializeField] private UnitStorageView unitStorage;
        [SerializeField] private FactoryStorageView factoryStorage;
        [SerializeField] private EntityUiManagerView entityUiManagerView;
        [SerializeField] private TMP_Text numZundaText;

        public Observable<Unit> OnTmpUnitButtonClickObservable => tmpUnitButton.SafeOnClickAsObservable();
        public Observable<Unit> OnTmpFactoryButtonClickObservable => tmpFactoryButton.SafeOnClickAsObservable();
        public UnitStorageView UnitStorage => unitStorage;
        public FactoryStorageView FactoryStorage => factoryStorage;
        public EntityUiManagerView EntityUiManager => entityUiManagerView;

        public void SetNumZunda(int numZunda)
        {
            numZundaText.text = numZunda.ToString();
        }
    }
}