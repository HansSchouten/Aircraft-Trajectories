using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Views.Visualisation;
using System;

namespace AircraftTrajectories.Presenters
{
    public class VisualisationPresenter
    {
        protected IVisualisationForm _view;

        public VisualisationPresenter(IVisualisationForm view)
        {
            _view = view;
            _view.CalculateNoise += delegate (object sender, EventArgs e) { CalculateNoise(); };
        }

        Trajectory trajectory;
        public void CalculateNoise()
        {
            if (_view.OneTrajectory && !_view.ExternalNoise)
            {
                var reader = new TrajectoryFileReader(CoordinateUnit.metric);
                trajectory = reader.createTrajectoryFromFile(Globals.currentDirectory + "track_schiphol.dat");

                var aircraft = new Aircraft("GP7270", "wing");
                var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
                //noiseModel.StartCalculation(calculationCompleted, pbAnimation);
            }
        }

    }
}
