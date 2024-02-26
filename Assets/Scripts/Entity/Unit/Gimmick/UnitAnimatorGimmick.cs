using GameFramework.GimmickSystems;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitAnimatorGimmick:ActiveGimmick
    {
        [SerializeField] private Animator myAnimator;
        
        public Animator MyAnimator => myAnimator;
        
        protected override void ActivateInternal()
        {
            SetAnimatorSpeed(1.0f);
        }

        protected override void DeactivateInternal()
        {
            SetAnimatorSpeed(0f);
        }

        /// <summary>
        /// アニメーションの再生速度を設定する
        /// </summary>
        public void SetAnimatorSpeed(float speed)
        {
            if (myAnimator != null)
            {
                myAnimator.speed = speed;
            }
        }
    }
}