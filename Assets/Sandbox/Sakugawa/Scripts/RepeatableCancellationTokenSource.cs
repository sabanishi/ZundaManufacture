using System.Threading;

namespace Sabanishi.ZundaManufacture.Sandbox
{
    public class RepeatableCancellationTokenSource
    {
        private CancellationTokenSource _cts;
        public CancellationToken Token => _cts.Token;
        
        public RepeatableCancellationTokenSource()
        {
            _cts = new CancellationTokenSource();
        }
        
        public void Cancel()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = new CancellationTokenSource();
        }
        
        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}