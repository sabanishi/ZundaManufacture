using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitSelectorView:MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField]private Camera unitCamera;
        [SerializeField] private Button cancelButton;
        public Camera UnitCamera => unitCamera;
        public Observable<Unit> OnClickCancelButtonAsObservable => cancelButton.SafeOnClickAsObservable();

        public void Open()
        {
            root.SetActive(true);
        }

        public void Close()
        {
            root.SetActive(false);
        }
    }
}