using Points.Model;
using Points.View;

namespace Points.Presenter
{
    public class PointsPresenter
    {
        private PointsModel _model;

        private PointsView _view;

        public PointsPresenter(PointsView view)
        {
            _model = new();

            _view = view;
        }

        public void Add(int amount = 1)
        {
            _model.Add(amount);
            _view.Display(_model.Count);
        }
    }
}