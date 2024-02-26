using UnityEngine;
using UnityEngine.UI;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitHealthView:EntityUiView
    {
        [SerializeField] private Slider slider;
        
        public void SetValue(float val)
        {
            slider.value = val;
        }
    }
}