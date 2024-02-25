using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    [CreateAssetMenu(menuName = "ZundaManufacture/FactoryInfoStorage",fileName = "FactoryInfoStorage.asset")]
    public class FactoryInfoStorage:ScriptableObject
    {
        [SerializeField]private FactoryInfo[] factoryInfos;
        
        public FactoryInfo GetInfo(FactoryType type)
        {
            foreach(var info in factoryInfos)
            {
                if (info.Type == type)
                {
                    return info;
                }
            }
            DebugLogger.LogWarning("指定されたFactoryTypeのFactoryInfoが存在しません:"+type);
            return null;
        }
    }
}