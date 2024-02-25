using UnityEngine;

namespace Sabanishi.ZundaManufacture
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T:MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
                return _instance;
            }
            private set => _instance = value;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = GetComponent<T>();
            }
            else
            {
                Destroy(gameObject);
            }
            OnAwakeInternal();
        }

        private void OnDestroy()
        {
            OnDestroyInternal();
            if (Instance == this)
            {
                Instance = null;
            }
        }

        /// <summary>
        /// Awake時に実行される処理
        /// Override用
        /// </summary>
        protected virtual void OnAwakeInternal()
        {
        }
        
        /// <summary>
        /// Destroy時に実行される処理
        /// Override用
        /// </summary>
        protected virtual void OnDestroyInternal()
        {
        }
    }
}