using GameFramework.Core;
using ObservableCollections;
using R3;

namespace Sabanishi.ZundaManufacture
{
    /// <summary>
    /// IScope用のRx拡張メソッド
    /// </summary>
    public static class ScopeObservableExtensions
    {
        /// <summary>
        /// IDisposableのScope登録
        /// </summary>
        public static Observable<T> TakeUntil<T>(this Observable<T> self, IScope scope) {
            return self.TakeUntil(scope.Token);
        }
        
        public static Observable<CollectionAddEvent<T>> TakeUntil<T>(this Observable<CollectionAddEvent<T>> self, IScope scope) {
            return self.TakeUntil(scope.Token);
        }
        
        public static Observable<CollectionRemoveEvent<T>> TakeUntil<T>(this Observable<CollectionRemoveEvent<T>> self, IScope scope) {
            return self.TakeUntil(scope.Token);
        }
        
        public static Observable<CollectionReplaceEvent<T>> TakeUntil<T>(this Observable<CollectionReplaceEvent<T>> self, IScope scope) {
            return self.TakeUntil(scope.Token);
        }
        
        public static Observable<CollectionMoveEvent<T>> TakeUntil<T>(this Observable<CollectionMoveEvent<T>> self, IScope scope) {
            return self.TakeUntil(scope.Token);
        }
    }
}