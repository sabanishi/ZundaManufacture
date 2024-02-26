using GameAiBehaviour;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class AnimatorNode:HandleableActionNode
    {
        [Tooltip("再生するアニメーション")]public string animationName;
    }
    
    public class AnimatorNodeHandler:ActionNodeHandler<AnimatorNode>
    {
        private Animator _animator;
        public void Setup(Animator animator)
        {
            _animator = animator;
        }
        
        protected override bool OnEnterInternal(AnimatorNode node) 
        {
            _animator.Play(node.animationName);
            return true;
        }
    }
}