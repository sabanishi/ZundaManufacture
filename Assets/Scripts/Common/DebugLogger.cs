using UnityEngine;

namespace Sabanishi.ZundaManufacture
{
    /// <summary>
    /// ログを出力するためのクラス
    /// 今後の拡張性を考慮してDebugクラスのラッパーとして実装している
    /// </summary>
    public class DebugLogger
    {
        public static void Log(string message)
        {
            Debug.Log(message);
        }
        
        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }
        
        public static void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}