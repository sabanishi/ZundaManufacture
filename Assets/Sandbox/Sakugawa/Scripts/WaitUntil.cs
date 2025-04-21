using System;
using System.Collections;

namespace Sabanishi.ZundaManufacture.Sandbox
{
    public class WaitUntil:IEnumerator
    {
        private readonly Func<bool> _function;
        
        public WaitUntil(Func<bool> function)
        {
            _function = function;
        }
        
        public bool MoveNext()
        {
            if (_function == null) return false;
            return !_function.Invoke();
        }

        public void Reset()
        {
        }

        public object Current => null;
    }
}