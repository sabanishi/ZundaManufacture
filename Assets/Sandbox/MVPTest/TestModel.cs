using System;
using System.Threading;
using R3;
using Sabanishi.ZundaManufacture.Core;

namespace Sandbox.MVPTest
{
    public class TestModel:Model
    {
        private ReactiveProperty<bool> _isOpen;
        
        public ReadOnlyReactiveProperty<bool> IsOpen => _isOpen;

        public TestModel()
        {
            _isOpen = new ReactiveProperty<bool>(false);
        }
        
        public void Dispose()
        {
            _isOpen.Dispose();
        }

        public void Open()
        {
            _isOpen.Value = true;
        }

        public void Close()
        {
            _isOpen.Value = false;
        }
    }
}