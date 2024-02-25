using System;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Entity
{
    /// <summary>
    /// Factory毎の情報を保持するクラス
    /// </summary>
    [Serializable]
    public class FactoryInfo
    {
        [SerializeField]private FactoryType type;
        [SerializeField] private string modelPath;
        
        public FactoryType Type => type;
        public string ModelPath => modelPath;
    }
}