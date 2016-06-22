using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Models.Visualisation;
using AircraftTrajectories.Models.Visualisation.KML;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras;
using AircraftTrajectories.Views.Visualisation;
using System;
using System.Collections.Generic;
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
            _view.PrepareVisualisation += delegate (object sender, EventArgs e) { PrepareVisualisation(); };
            _view.CancelVisualisationPreparation += delegate (object sender, EventArgs e) { Cancel(); };
        }

        protected void Cancel()
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }

        Trajectory trajectory;
        IntegratedNoiseModel noiseModel;
        TemporalGrid temporalGrid;

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
        
        protected void OneTrajectoryINM()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            trajectory = reader.createTrajectoryFromFile(_view.TrajectoryFile);

            var aircraft = new Aircraft("GP7270", "wing");
            trajectory.Aircraft = aircraft;
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);

            noiseModel.StartCalculation(ProgressChanged);
            temporalGrid = noiseModel.TemporalGrid;

            _view.Invoke(delegate { _view.NoiseCalculationCompleted(); });
        }

        #endregion




        #region "Prepare Visualisation"

        public void PrepareVisualisation()
        {
            startTime = DateTime.Now;

            if (_view.OneTrajectory)
            {
                thread = new Thread(OneTrajectoryVisualisation);
                thread.Start();
            }
        }

        protected void OneTrajectoryVisualisation()
        {
            var legend = new LegendCreator();
            legend.OutputLegendImage();
            legend.OutputLegendTitle();

            //var population = new PopulationData(Globals.currentDirectory + "population.dat");

            var sections = new List<KMLAnimatorSectionInterface>() {
                new LegendKMLAnimator(),
                new AircraftKMLAnimator(trajectory.Aircraft, trajectory),
                new AirplotKMLAnimator(trajectory),
                new GroundplotKMLAnimator(trajectory),
                new ContourKMLAnimator(temporalGrid, trajectory, new List<int>() { 65, 70, 75 }),
                //new AnnoyanceKMLAnimator(temporalGrid, population.getPopulationData())
            };
            var camera = new FollowKMLAnimatorCamera(trajectory.Aircraft, trajectory);
            var animator = new KMLAnimator(sections, camera);
            animator.AnimationToFile(trajectory.Duration, Globals.currentDirectory + "visualisation.kml");

            _view.Invoke(delegate { _view.PreparationCalculationCompleted(); });
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
