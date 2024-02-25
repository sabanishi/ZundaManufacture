using Sabanishi.ZundaManufacture.Common;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    [CreateAssetMenu(menuName = "ZundaManufacture/UnitInfoStorage",fileName = "UnitInfoStorage.asset")]
    public class UnitInfoStorage:ScriptableObject
    {
        [SerializeField]private UnitInfo[] unitInfos;

        public UnitInfo GetInfo(UnitType type)
        {
            foreach(var info in unitInfos)
            {
                if (info.Type == type)
                {
                    return info;
                }
            }
            DebugLogger.LogWarning("指定されたUnitTypeのUnitInfoが存在しません:"+type);
            return null;
        }
    }
}