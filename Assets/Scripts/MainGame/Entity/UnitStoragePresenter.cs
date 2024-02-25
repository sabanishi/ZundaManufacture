using GameFramework.LogicSystems;

namespace Sabanishi.ZundaManufacture.MainGame
{
    public class UnitStoragePresenter:Logic
    {
        private readonly UnitStorageModel _model;
        private readonly UnitStorageView _view;
        
        public UnitStoragePresenter(UnitStorageModel model, UnitStorageView view)
        {
            _model = model;
            _view = view;
        }
    }
}