using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Sabanishi.ZundaManufacture
{
    public static class ButtonRxExtensions
    {
        public static Observable<Unit> SafeOnClickAsObservable(this Button button)
        {
            if (button == null)
            {
                Debug.LogError("Buttonがnullです");
                return Observable.Empty<Unit>();
            }
            return button.OnClickAsObservable();
        }
        
        /// <summary>
        /// ボタンを押し続けている間、毎フレームOnNextを発行するObservable
        /// </summary>
        public static Observable<Unit> OnKeepTapAsObservable(this Button button)
        {
            if (button == null)
            {
                Debug.LogError("Buttonがnullです");
                return Observable.Empty<Unit>();
            }

            return button.OnPointerDownAsObservable()
                .SelectMany(_ => Observable.EveryUpdate())
                .TakeUntil(button.OnPointerUpAsObservable())
                .Select(_ => Unit.Default);
        }
    }
}