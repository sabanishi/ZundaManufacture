using UnityEngine;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class EntityUiView:MonoBehaviour
    {
        private RectTransform _myTransform;

        private RectTransform MyTransform
        {
            get
            {
                if (_myTransform == null)
                {
                    _myTransform = GetComponent<RectTransform>();
                }

                return _myTransform;
            }
        }
        private Camera _worldCamera;
        
        public void Setup(Camera worldCamera)
        {
            _worldCamera = worldCamera;
        }
        
        public void SetOffset(Vector3 offset)
        {
            MyTransform.localPosition = offset;
        }
        
        public void UpdateEntityPosition()
        {
            MyTransform.LookAt(_worldCamera.transform);
        }
    }
}