using GameFramework.ActorSystems;
using GameFramework.BodySystems;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class EntityActor:Actor
    {
        public EntityActor(Body body) : base(body)
        {
        }

        public void SetPosition(Vector3 pos)
        {
            Body.Transform.position = pos;
        }
        
        public void SetEulerAngle(Vector3 eulerAngle)
        {
            Body.Transform.eulerAngles = eulerAngle;
        }
    }
}