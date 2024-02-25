using System;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    /// <summary>
    /// Unit毎の情報を保持するクラス
    /// </summary>
    [Serializable]
    public class UnitInfo
    {
        [SerializeField] private UnitType type;
        [SerializeField] private string modelPath;
        public UnitType Type => type;
        public string ModelPath => modelPath;
    }
}