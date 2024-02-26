namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitBrain : BehaviourTreeLogic
    {
        private readonly UnitModel _model;
        private readonly UnitActor _actor;
        
        public UnitBrain(UnitModel model, UnitActor actor):base(actor)
        {
            _model = model;
            _actor = actor;
        }

        protected override void BindActionHandlersInternal()
        {
            TreeController.BindActionNodeHandler<RandomWalkNode, RandomWalkHandler>(h => h.Setup(_model));
        }

        protected override void SetupTree()
        {
            if (_model.Info == null)
            {
                DebugLogger.LogError("UnitInfo is null");
                return;
            }
            TreeController.Setup(_model.Info.AiTree);
        }
    }
}