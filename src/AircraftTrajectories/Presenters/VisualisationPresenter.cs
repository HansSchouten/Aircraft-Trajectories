using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Views.Visualisation;
using System;
using System.Threading;
using System.Windows.Forms;

namespace AircraftTrajectories.Presenters
{
    public class VisualisationPresenter
    {
        protected IVisualisationForm _view;
        
        Thread thread;
        DateTime startTime;

        public VisualisationPresenter(IVisualisationForm view)
        {
            _view = view;
            _view.CalculateNoise += delegate (object sender, EventArgs e) { CalculateNoise(); };
            _view.CancelNoiseCalculation += delegate (object sender, EventArgs e) { Cancel(); };
        }

        protected void Cancel()
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }


        #region "Calculate Noise"

        public void CalculateNoise()
        {
            startTime = DateTime.Now;

            if (_view.OneTrajectory && !_view.ExternalNoise)
            {
                thread = new Thread(OneTrajectoryINM);
                thread.Start();
            }
        }

        IntegratedNoiseModel noiseModel;
        protected void OneTrajectoryINM()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(_view.TrajectoryFile);

            var aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);

            noiseModel.StartCalculation(ProgressChanged);

            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
        }

        #endregion





        protected void ProgressChanged(int progress)
        {
            _view.Invoke(delegate
            {
                double factor = (double) progress / 100;
                double secElapsed = DateTime.Now.Subtract(startTime).TotalSeconds;

                _view.Percentage = (int)(factor * 100);
                _view.TimeLeft = (int)Math.Ceiling(((secElapsed / factor) - secElapsed) / 60.0);
            });
        }

    }
}
