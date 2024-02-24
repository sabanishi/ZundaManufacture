using System.Threading;
using Sabanishi.ZundaManufacture.Core;

namespace Sandbox.MVPTest
{
    public class TestPresenter:Presenter
    {
        private readonly TestModel _model;
        private readonly TestView _view;
        
        public TestPresenter(TestModel model, TestView view)
        {
            _model = model;
            _view = view;
        }

        protected override void ActivateInternal(CancellationToken token)
        {
            base.ActivateInternal(token);
        }
    }
}