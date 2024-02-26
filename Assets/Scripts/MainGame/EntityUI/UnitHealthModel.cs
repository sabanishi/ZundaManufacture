using GameFramework.Core;
using R3;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitHealthModel:EntityUiModel
    {
        private ReactiveProperty<float> _nowValue;

        public ReadOnlyReactiveProperty<float> NowValue => _nowValue;
        
        private UnitHealthModel(int id) : base(id)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            base.OnCreatedInternal(scope);
            _nowValue = new ReactiveProperty<float>();
            _nowValue.Value = 1;
        }

        protected override void OnDeletedInternal()
        {
            base.OnDeletedInternal();
            _nowValue.Dispose();
        }
        
        public void SetValue(float value)
        {
            var clampedValue = Mathf.Clamp(value, 0, 1);
            _nowValue.Value =clampedValue;
        }
    }
}