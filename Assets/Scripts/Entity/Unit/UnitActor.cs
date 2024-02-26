using GameFramework.BodySystems;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitActor:EntityActor
    {
        private Vector3 _moveVelocity;
        public Vector3 MoveVelocity => _moveVelocity;
        
        public UnitActor(Body body) : base(body)
        {
        }

        protected override void UpdateInternal()
        {
            base.UpdateInternal();
            Move();
        }

        private void Move()
        {
            var deltaTime = Body.DeltaTime;
            var nextPos = Body.Transform.position + _moveVelocity * deltaTime;
            Body.Transform.position = nextPos;
        }
        
        public void SetMoveVelocity(Vector3 moveVelocity)
        {
            _moveVelocity = moveVelocity;
            LookTargetDir(moveVelocity);
        }

        /// <summary>
        /// targetPosの方向を向く
        /// </summary>
        private void LookTargetPos(Vector3 targetPos)
        {
            var nowPos = Body.Position;
            var moveDirection = targetPos - nowPos;
            LookTargetDir(moveDirection);
        }

        private void LookTargetDir(Vector3 dir)
        {
            var angle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
            var eulerAngle = new Vector3(0, angle, 0);
            Body.Transform.eulerAngles = eulerAngle;
        }
    }
}