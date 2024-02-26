using GameFramework.Core;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitActionService
    {
        /// <summary>
        /// Modelをランダムな方向に移動させる
        /// </summary>
        public AsyncOperationHandle DoRandomWalkAction(UnitModel model)
        {
            if (model == null) return default;

            var targetPos = model.Position + new Vector3(Random.value-0.5f,0, Random.value-0.5f)*5;
            var speed = 2.0f;
            return model.DoMoveTargetPosAsync(targetPos, speed);
        }
        
        public AsyncOperationHandle DoRestAction(UnitModel model)
        {
            return model.DoRestAsync();
        }

        public AsyncOperationHandle DoIdleAction(UnitModel model)
        {
            return model.DoIdleAsync(1.0f);
        }
    }
}