using GameFramework.Core;
using GameFramework.LogicSystems;
using GameFramework.TaskSystems;
using R3;
using Sabanishi.ZundaManufacture.Entity;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    /// <summary>
    /// Entityのタップを検知する機構
    /// </summary>
    public class UniTapChecker:Logic
    {
        private static readonly LayerMask UnitLayer = LayerMask.NameToLayer(LayerName.Unit);
        
        private readonly Camera _myCamera;

        private readonly Subject<GameObject> _tapSubject;
        public Observable<GameObject> TapObservable => _tapSubject;
        
        public UniTapChecker(Camera myCamera)
        {
            _myCamera = myCamera;
            _tapSubject = new Subject<GameObject>();
            
            Services.Get<TaskRunner>().Register(this,TaskOrder.Logic);
        }

        protected override void DisposeInternal()
        {
            _tapSubject.Dispose();
        }

        protected override void UpdateInternal()
        {
            //タップされた時にカメラからレイを飛ばす
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _myCamera.ScreenPointToRay(Input.mousePosition);
                //レイを描画
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                if (Physics.Raycast(ray, out var hit,Mathf.Infinity,UnitLayer))
                {
                    var hitGimmick = hit.collider.gameObject.GetComponent<UnitTapHitCollider>();
                    if (hitGimmick == null)
                    {
                        DebugLogger.LogError("UnitTapHitColliderがアタッチされていません:"+hit.collider.gameObject.name);
                        return;
                    }
                    _tapSubject.OnNext(hitGimmick.GetParent());
                }
            }
        }
    }
}