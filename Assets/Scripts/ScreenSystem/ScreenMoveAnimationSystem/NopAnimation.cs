using System.Threading;
using Cysharp.Threading.Tasks;
using Sabanishi.ScreenSystem;

namespace Sabanishi.ZundaManufacture.ScreenSystem
{
    public class NopAnimation:ITransitionAnimation
    {
        public UniTask Play(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }
    }
}