using System;
using GameAiBehaviour;
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
        [SerializeField] private BehaviourTree aiTree;
        [SerializeField] private BehaviourTree animationTree;
        public UnitType Type => type;
        public string ModelPath => modelPath;
        public BehaviourTree AiTree => aiTree;
        public BehaviourTree AnimationTree => animationTree;
    }
}