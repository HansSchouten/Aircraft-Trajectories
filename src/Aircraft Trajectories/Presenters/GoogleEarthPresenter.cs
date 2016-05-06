using AircraftTrajectories.Views;

namespace AircraftTrajectories.Presenters
{
    public class GoogleEarthPresenter
    {
        private readonly IGoogleEarthForm view;

        public GoogleEarthPresenter(IGoogleEarthForm view)
        {
            this.view = view;
        }
    }
}
