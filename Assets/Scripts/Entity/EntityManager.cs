using System;
using System.Collections.Generic;
using GameFramework.ActorSystems;
using GameFramework.ModelSystems;
using GameFramework.TaskSystems;
using Sabanishi.ZundaManufacture.Common;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class EntityManager<T> :IDisposable where T : class,IModel
    {
        private readonly TaskRunner _taskRunner;
        private readonly Dictionary<T,ActorEntity> _entities;
        
        protected EntityManager(TaskRunner taskRunner)
        {
            _taskRunner = taskRunner;
            _entities = new Dictionary<T, ActorEntity>();
        }

        public void Dispose()
        {
            foreach (var pair in _entities)
            {
                var model = pair.Key;
                var entity = pair.Value;
                DisposeActorEntityInternal(model,entity);
                entity?.Dispose();
            }
            _entities.Clear();
            DisposeInternal();
        }

        /// <summary>
        /// Modelに紐づいたActorEntityを取得する<br />
        /// ActorEntityが存在しない場合は新規生成し、存在する場合はそのまま返す
        /// </summary>
        public ActorEntity GetOrCreateEntity(T model)
        {
            if (model == null)
            {
                DebugLogger.LogWarning("Modelがnullです");
                return null;
            }

            //Entityが既に生成済みの場合はそのまま返す
            if (_entities.TryGetValue(model, out var entity))
            {
                return entity;
            }

            entity = new ActorEntity();
            _entities.Add(model, entity);
            entity.SetModel(model);
            GetOrCreateEntityInternal(model, entity);
            return entity;
        }

        /// <summary>
        /// Modelに紐づいたActorEntityを破棄する
        /// </summary>
        public void DisposeActorEntity(T model)
        {
            if (!TryGetActorEntity(model, out var entity)) return;
            entity.Dispose();
            _entities.Remove(model);
        }

        protected bool TryGetActorEntity(T model, out ActorEntity entity)
        {
            entity = null;
            if (model == null) return false;
            if (_entities.TryGetValue(model, out entity)) return true;
            return false;
        }

        protected void RegisterTask(ITask task,TaskOrder order)
        {
            _taskRunner.Register(task,order);
        }

        protected virtual void DisposeInternal()
        {
        }
        
        protected virtual void GetOrCreateEntityInternal(T model,ActorEntity entity)
        {
        }
        
        protected virtual void DisposeActorEntityInternal(T model,ActorEntity entity)
        {
        }
    }
}