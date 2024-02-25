using GameFramework.ActorSystems;
using GameFramework.BodySystems;
using GameFramework.TaskSystems;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class FactoryManager:EntityManager<FactoryModel>
    {

        private readonly BodyManager _bodyManager;
        public FactoryManager(TaskRunner taskRunner,BodyManager bodyManager) : base(taskRunner)
        {
            _bodyManager = bodyManager;
        }

        public void AttachActor(FactoryModel model)
        {
            if (!TryGetActorEntity(model, out var entity)) return;

            var body = CreateBody(model.Info);
            if (body == null || !body.IsValid)
            {
                DebugLogger.LogWarning("Bodyがnullまたは無効です");
                return;
            }

            var actor = new FactoryActor(body);
            var presenter = new FactoryPresenter(model, actor);

            entity.SetBody(body, false);
            
            entity.AddActor(actor);
            entity.AddLogic(presenter);
            
            RegisterTask(actor,TaskOrder.Actor);
            RegisterTask(presenter,TaskOrder.Logic);
        }

        public void DetachActor(FactoryModel model)
        {
            if (!TryGetActorEntity(model, out var entity)) return;
            entity.RemoveLogic<FactoryPresenter>();
            entity.RemoveActors();
            entity.RemoveBody();
        }
        
        private Body CreateBody(FactoryInfo info)
        {
            var prefab = ResourceManager.Instance.Load<GameObject>(info.ModelPath);
            return _bodyManager.CreateFromPrefab(prefab);
        }
    }
}