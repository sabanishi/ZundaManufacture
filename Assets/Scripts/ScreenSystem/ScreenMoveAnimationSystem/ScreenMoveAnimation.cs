using UnityEngine;
using UnityEngine.UI;

namespace Sabanishi.ZundaManufacture.ScreenSystem
{
    public class ScreenMoveAnimation:SingletonMonoBehaviour<ScreenMoveAnimation>
    {
        [SerializeField] private Image image;
        
        public TmpOpenAnimation OpenAnimation { get; private set; }
        public TmpCloseAnimation CloseAnimation { get; private set; }

        protected override void OnAwakeInternal()
        {
            if (image == null)
            {
                DebugLogger.LogWarning("ScreenMoveAnimationにImageがアタッチされていません");
                return;
            }
            OpenAnimation = new TmpOpenAnimation(image);
            CloseAnimation = new TmpCloseAnimation(image);
        }
    }
}