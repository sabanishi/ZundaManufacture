using System;
using System.Collections;

namespace Sabanishi.ZundaManufacture.Sandbox
{
    public class WaitWhile:IEnumerator
    {
        private readonly Func<bool> _function;
        
        public WaitWhile(Func<bool> function)
        {
            _function = function;
        }
        
        public bool MoveNext()
        {
            if (_function == null) return false;
            return _function.Invoke();
        }

        public void Reset()
        {
        }

        public object Current => null;
    }
}