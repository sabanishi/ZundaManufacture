namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitAnimatorController:BehaviourTreeLogic
    {
        private readonly UnitModel _model;
        private readonly UnitActor _actor;

        public UnitAnimatorController(UnitModel model,UnitActor actor) : base(actor)
        {
            _model = model;
            _actor = actor;
        }

        protected override void SetupTree()
        {
            if (_model.Info == null)
            {
                DebugLogger.LogError("UnitInfo is null");
                return;
            }
            TreeController.Setup(_model.Info.AnimationTree);
        }
        
        protected override void BindActionHandlersInternal()
        {
        }
    }
}