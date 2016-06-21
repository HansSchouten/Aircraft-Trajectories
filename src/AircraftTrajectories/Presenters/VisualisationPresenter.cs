using AircraftTrajectories.Views.Visualisation;

namespace AircraftTrajectories.Presenters
{
    public class VisualisationPresenter
    {
        protected IVisualisationForm _view;

        public VisualisationPresenter(IVisualisationForm view)
        {
            _view = view;
        }

    }
}
