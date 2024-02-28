using GameFramework.Core;
using GameFramework.ModelSystems;
using R3;
using Sabanishi.ZundaManufacture.Entity;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitSelectorModel:SingleModel<UnitSelectorModel>
    {
        private ReactiveProperty<UnitModel> _selectedUnit;

        public ReadOnlyReactiveProperty<UnitModel> SelectedUnit => _selectedUnit;
        
        private UnitSelectorModel(object empty) : base(empty)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            _selectedUnit = new ReactiveProperty<UnitModel>();
        }

        protected override void OnDeletedInternal()
        {
            _selectedUnit.Dispose();
        }
        
        public void SetSelectedUnit(UnitModel unit)
        {
            _selectedUnit.Value = unit;
        }
    }
}