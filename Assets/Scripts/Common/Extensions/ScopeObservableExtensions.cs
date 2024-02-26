using System;
using System.Threading;
using GameFramework.Core;
using R3;

namespace Sabanishi.ZundaManufacture
{
    /// <summary>
    /// IScope用のRx拡張メソッド
    /// </summary>
    public static class ScopeObservableExtensions
    {
        public static CancellationTokenRegistration RegisterTo(this IDisposable source, IScope scope)
        {
            return source.RegisterTo(scope.Token);
        }
    }
}