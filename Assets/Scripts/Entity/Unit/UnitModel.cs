using System.Collections;
using GameFramework.Core;
using R3;
using Sabanishi.ZundaManufacture.MainGame;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    public class UnitModel : EntityModel
    {
        private UnitInfo _info;
        private ReactiveProperty<bool> _isResting;
        private ReactiveProperty<bool> _isWaitCommand;
        
        private Subject<Vector3> _setMoveVelocitySubject;
        private Subject<Vector3> _setEulerAngleSubject;
        
        private UnitHealthModel _health;

        public UnitInfo Info => _info;
        public ReadOnlyReactiveProperty<bool> IsResting => _isResting;
        public ReadOnlyReactiveProperty<bool> IsWaitCommand => _isWaitCommand;
        public Observable<Vector3> SetMoveVelocityObservable => _setMoveVelocitySubject;
        public Observable<Vector3> SetEulerAngleObservable => _setEulerAngleSubject;
        public UnitHealthModel Health => _health;

        private UnitModel(int id) : base(id)
        {
        }

        protected override void OnCreatedInternal(IScope scope)
        {
            base.OnCreatedInternal(scope);
            _isResting = new ReactiveProperty<bool>().ScopeTo(scope);
            _isWaitCommand = new ReactiveProperty<bool>().ScopeTo(scope);
            _setMoveVelocitySubject = new Subject<Vector3>().ScopeTo(scope);
            _setEulerAngleSubject = new Subject<Vector3>().ScopeTo(scope);
            _health = UnitHealthModel.Create<UnitHealthModel>().ScopeTo(scope);
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

            _health.SetOffset(new Vector3(0, 2.37f, 0));
        }

        /// <summary>
        /// 目標地点まで移動する
        /// </summary>
        public AsyncOperationHandle DoMoveTargetPosAsync(Vector3 targetPos, float speed)
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
                    //体力を減少させる
                    _health.SetValue(_health.NowValue.CurrentValue - _info.DecreaseHealthSpeedForWalk * Time.deltaTime);
                    if (_health.NowValue.CurrentValue <= 0)
                    {
                        //体力が0になった時、移動処理を終了する
                        if (!asyncOperator.IsDone)
                        {
                            break;
                        }
                    }

                    var dist = Vector3.Distance(Position, targetPos);
                    //目標地点に充分近づいていたら、移動を終了する
                    if (dist < epsilon) break;
                    //Unitが反対方向に向かっていたら、移動を終了する
                    if (cacheDist < dist) break;
                    cacheDist = dist;
                    yield return null;
                }

                //速度を0にする
                _setMoveVelocitySubject.OnNext(Vector3.zero);
            }

            return DoActionAsync(Routine);
        }

        /// <summary>
        /// 体力が最大になるまで休憩する
        /// </summary>
        public AsyncOperationHandle DoRestAsync()
        {
            IEnumerator Routine(AsyncOperator asyncOperator)
            {
                _isResting.Value = true;
                while (true)
                {
                    //体力を回復させる
                    _health.SetValue(_health.NowValue.CurrentValue + _info.IncreaseHealthSpeedForRest * Time.deltaTime);
                    if (_health.NowValue.CurrentValue >= 1)
                    {
                        //体力が最大になったら、休憩処理を終了する
                        if (!asyncOperator.IsDone)
                        {
                            _isResting.Value = false;
                            asyncOperator.Completed();
                        }
                    }

                    yield return null;
                }
            }

            return DoActionAsync(Routine);
        }

        /// <summary>
        /// time秒待機する
        /// </summary>
        public AsyncOperationHandle DoIdleAsync(float time)
        {
            IEnumerator Routine(AsyncOperator asyncOperator)
            {
                //time秒待機する
                yield return new WaitForSeconds(time);
            }

            return DoActionAsync(Routine);
        }

        /// <summary>
        /// ユーザーからの命令を待ち始める
        /// </summary>
        public void StartWaitCommand(Vector3 cameraPos)
        {
            DebugLogger.Log("UnitModel StartWaitCommand");
            CancelAction();
            _isWaitCommand.Value = true;
            //カメラの方向を向いて停止する
            var dir = cameraPos - Position;
            _setMoveVelocitySubject.OnNext(Vector3.zero);
            _setEulerAngleSubject.OnNext(dir);
        }
        
        public void CancelWaitCommand()
        {
            _isWaitCommand.Value = false;
        }
    }
}