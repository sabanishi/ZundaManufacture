using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sabanishi.ScreenSystem;
using Sabanishi.ZundaManufacture.Common;
using Sabanishi.ZundaManufacture.MainGame;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.ScreenSystem
{
    /// <summary>
    /// ScreenTransitionをシングルトンとして公開するためのクラス
    /// </summary>
    public class ScreenTransitionLocator : SingletonMonoBehaviour<ScreenTransitionLocator>
    {
        private readonly Dictionary<Type, string> _screenPathDict = new()
        {
            {typeof(MainGameScreen),"Screen/MainGameScreen"},
        };

        private ScreenTransition _screenTransition;

        protected override void OnAwakeInternal()
        {
            _screenTransition = new ScreenTransition();
            _screenTransition.Initialize(LoadScreenPrefab());
        }

        private Dictionary<Type, GameObject> LoadScreenPrefab()
        {
            var dict = new Dictionary<Type, GameObject>();
            foreach (var pair in _screenPathDict)
            {
                var prefab = ResourceManager.Instance.Load<GameObject>(pair.Value);
                if (prefab == null)
                {
                    DebugLogger.LogError($"{pair.Value}が見つかりませんでした");
                    continue;
                }

                dict.Add(pair.Key, prefab);
            }

            return dict;
        }

        public async UniTask Move<T>(ITransitionAnimation closeAnimation, ITransitionAnimation openAnimation,
            Action<T> bridgeAction = null) where T : IScreen
        {
            await _screenTransition.Move<T>(closeAnimation, openAnimation, bridgeAction);
        }
    }
}