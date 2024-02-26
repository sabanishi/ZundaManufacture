using System.Collections;
using GameFramework.Core;
using R3;
using Sabanishi.ZundaManufacture.MainGame;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitModel:EntityModel
    {
        private UnitInfo _info;
        private Subject<Vector3> _setMoveVelocitySubject;
        private UnitHealthModel _health;
        
        public UnitInfo Info => _info;
        public Observable<Vector3> SetMoveVelocityObservable => _setMoveVelocitySubject;
        public UnitHealthModel Health => _health;
        
        private UnitModel(int id) : base(id)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            base.OnCreatedInternal(scope);
            _setMoveVelocitySubject = new Subject<Vector3>().ScopeTo(scope);
            _health = UnitHealthModel.Create<UnitHealthModel>().ScopeTo(scope);
            _health.SetOffset(new Vector3(0,2.37f,0));
        }

        public static UnitModel Create(UnitInfo info)
        {
            var model = Create<UnitModel>();
            model.Setup(info);
            return model;
        }
        
        private void Setup(UnitInfo info)
        {
            _info = info;
        }

        /// <summary>
        /// 目標地点まで移動する
        /// </summary>
        public AsyncOperationHandle DoMoveTargetPosAsync(Vector3 targetPos,float speed)
        {
            IEnumerator Routine(AsyncOperator asyncOperator)
            {
                //Actorに移動速度の情報を送信する
                var velocitySpeed = (targetPos - Position).normalized * speed;
                _setMoveVelocitySubject.OnNext(velocitySpeed);
                
                //目標地点への移動が完了するまで待機
                var epsilon = 0.01f;
                var cacheDist = Mathf.Infinity;
                while (true)
                {
                    var dist = Vector3.Distance(Position, targetPos);
                    //目標地点に充分近づいていたら、移動を終了する
                    if (dist < epsilon) break;
                    //Unitが反対方向に向かっていたら、移動を終了する
                    if (cacheDist < dist) break;
                    cacheDist = dist;
                    yield return null;
                }
            }

            return DoActionAsync(Routine);
        }
    }
}