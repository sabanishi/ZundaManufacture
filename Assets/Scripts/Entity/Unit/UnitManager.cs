using GameFramework.ActorSystems;
using GameFramework.BodySystems;
using GameFramework.TaskSystems;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitManager:EntityManager<UnitModel>
    {
        private readonly BodyManager _bodyManager;
        
        public UnitManager(TaskRunner taskRunner,BodyManager bodyManager) : base(taskRunner)
        {
            _bodyManager = bodyManager;
        }

        public void AttachActor(UnitModel model)
        {
            if (!TryGetActorEntity(model, out var entity)) return;

            var body = CreateBody(model.Info);
            if (body == null || !body.IsValid)
            {
                DebugLogger.LogWarning("Bodyがnullまたは無効です");
                return;
            }

            var actor = new UnitActor(body);
            var presenter = new UnitPresenter(model, actor);
            var brain = new UnitBrain(model, actor);
            var animatorController = new UnitAnimatorController(model, actor);
            
            entity.SetBody(body, false);
            entity.AddActor(actor);
            entity.AddLogic(presenter);
            entity.AddLogic(brain);
            entity.AddLogic(animatorController);
            
            RegisterTask(actor,TaskOrder.Actor);
            RegisterTask(presenter,TaskOrder.Logic);
            RegisterTask(brain,TaskOrder.AiLogic);
            RegisterTask(animatorController,TaskOrder.Body);
        }

        public void DetachActor(UnitModel model)
        {
            if (!TryGetActorEntity(model, out var entity)) return;
            entity.RemoveLogic<UnitPresenter>();
            entity.RemoveLogic<UnitBrain>();
            entity.RemoveLogic<UnitAnimatorController>();
            entity.RemoveActors();
            entity.RemoveBody();
        }
        
        private Body CreateBody(UnitInfo info)
        {
            var prefab = ResourceManager.Instance.Load<GameObject>(info.ModelPath);
            return _bodyManager.CreateFromPrefab(prefab);
        }
    }
}