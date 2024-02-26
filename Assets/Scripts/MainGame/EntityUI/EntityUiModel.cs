using GameFramework.Core;
using GameFramework.ModelSystems;
using R3;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class EntityUiModel:AutoIdModel<EntityUiModel>
    {
        private ReactiveProperty<Vector3> _offset;
        public ReadOnlyReactiveProperty<Vector3> Offset => _offset;
        
        protected EntityUiModel(int id) : base(id)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            _offset = new ReactiveProperty<Vector3>();
        }

        protected override void OnDeletedInternal()
        {
            _offset.Dispose();
        }
        
        public void SetOffset(Vector3 offset)
        {
            _offset.Value = offset;
        }
    }
}