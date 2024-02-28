using System.Collections;
using GameAiBehaviour;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class NopNode:ActionNode
    {
        public override ILogic CreateLogic(IBehaviourTreeRunner runner)
        {
            return new NopLogic(runner,this);
        }

        private class NopLogic : Logic<NopNode>
        {
            public NopLogic(IBehaviourTreeRunner runner, NopNode node):base(runner, node)
            {
            }
            protected override IEnumerator ExecuteActionRoutineInternal()
            {
                SetState(State.Success);
                yield break;
            }
        }
        
    }
}