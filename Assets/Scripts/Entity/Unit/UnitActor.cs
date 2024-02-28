using System.Linq;
using GameFramework.BodySystems;
using GameFramework.Core;
using R3;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitActor:EntityActor
    {
        private const string AnimatorGimmickKey = "UnitAnimatorGimmick";
        private const string TapHitColliderGimmickKey = "UnitTapHitCollider";
        
        private readonly UnitAnimatorGimmick _animatorGimmick;
        private readonly UnitTapHitCollider _tapHitCollider;

        private readonly ReactiveProperty<Vector3> _moveVelocity;
        public ReadOnlyReactiveProperty<Vector3> MoveVelocity => _moveVelocity;
        
        #region Getter

        public Animator GetAnimator()
        {
            return _animatorGimmick.MyAnimator;
        }
        
        #endregion
        
        public UnitActor(Body body) : base(body)
        {
            _moveVelocity = new ReactiveProperty<Vector3>().ScopeTo(this);
            
            _animatorGimmick = GimmickController.GetGimmicks<UnitAnimatorGimmick>(AnimatorGimmickKey).FirstOrDefault();
            _tapHitCollider = GimmickController.GetGimmicks<UnitTapHitCollider>(TapHitColliderGimmickKey).FirstOrDefault();
            
            CheckIsGimmickNull(_animatorGimmick);
            CheckIsGimmickNull(_tapHitCollider);
        }

        protected override void ActivateInternal(IScope scope)
        {
            base.ActivateInternal(scope);
            _animatorGimmick.Activate();
            _tapHitCollider.Activate();
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();
            _animatorGimmick.Deactivate();
            _tapHitCollider.Deactivate();
        }

        protected override void UpdateInternal()
        {
            base.UpdateInternal();
            Move();
            var layer = Body.LayeredTime;
            _animatorGimmick.SetAnimatorSpeed(layer.TimeScale);
        }

        private void Move()
        {
            var deltaTime = Body.DeltaTime;
            var nextPos = Body.Transform.position + _moveVelocity.Value * deltaTime;
            Body.Transform.position = nextPos;
        }
        
        public void SetMoveVelocity(Vector3 moveVelocity)
        {
            _moveVelocity.Value = moveVelocity;
            if (moveVelocity != Vector3.zero)
            {
                LookTargetDir(moveVelocity);
            }
        }

        /// <summary>
        /// targetPosを見る
        /// </summary>
        private void LookTargetPos(Vector3 targetPos)
        {
            var nowPos = Body.Position;
            var moveDirection = targetPos - nowPos;
            LookTargetDir(moveDirection);
        }

        /// <summary>
        /// dirの方向を向く
        /// </summary>
        private void LookTargetDir(Vector3 dir)
        {
            var angle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
            var eulerAngle = new Vector3(0, angle, 0);
            Body.Transform.eulerAngles = eulerAngle;
        }
        
        /// <summary>
        /// 自身が選択されている間、MeshRendererを表示する
        /// </summary>
        public void SetTapRendererActive(bool active)
        {
            _tapHitCollider.SetRendererActive(active);
        }
    }
}