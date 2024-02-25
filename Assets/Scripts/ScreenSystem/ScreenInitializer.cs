using Cysharp.Threading.Tasks;
using Sabanishi.ZundaManufacture.MainGame;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.ScreenSystem
{
    public class ScreenInitializer:MonoBehaviour
    {
        private void Start()
        {
            var nopAnimation = new NopAnimation();
            ScreenTransitionLocator.Instance
                .Move<MainGameScreen>(nopAnimation,nopAnimation).Forget();
        }
    }
}