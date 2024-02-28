using GameFramework.Core;
using GameFramework.ModelSystems;
using R3;
using Sabanishi.ZundaManufacture.Entity;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitSelectorModel:SingleModel<UnitSelectorModel>
    {
        private ReactiveProperty<bool> _isOpen;
        public ReadOnlyReactiveProperty<bool> IsOpen => _isOpen;

        private UnitModel _nowSelectedModel;
        
        private UnitSelectorModel(object empty) : base(empty)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            _isOpen = new ReactiveProperty<bool>().ScopeTo(scope);
        }
        
        public void SetSelectedUnit(UnitModel unit,Vector3 cameraPos=default)
        {
            //null以外のUnitが選択された時、UIを表示する
            var isNull = unit == null;
            _isOpen.Value = !isNull;

            if (_nowSelectedModel != null)
            {
                //選択中のModelが存在する時、待機状態を解除する
                _nowSelectedModel.CancelWaitCommand();
            }
            _nowSelectedModel = unit;

            if (isNull) return;
            //Modelが現在行っている作業を中断し、次の命令が来るまで待機させる
            unit.StartWaitCommand(cameraPos);
        }
    }
}