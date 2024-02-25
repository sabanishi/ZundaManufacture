using UnityEngine;

namespace Sabanishi.ZundaManufacture.Common
{
    /// <summary>
    /// リソースを管理するためのクラス
    /// 今後、ResourcesとAddressablesを使い分ける事を考慮してラッパークラスとして実装している
    /// </summary>
    public class ResourceManager
    {
        private static ResourceManager _instance;
        public static ResourceManager Instance => _instance ??= new ResourceManager();

        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}