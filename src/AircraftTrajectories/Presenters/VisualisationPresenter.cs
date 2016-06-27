using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Population;
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
            _view.VisualiseTrajectoryEvent += delegate (object sender, EventArgs e) { VisualiseTrajectory(); };
        }

        protected void Cancel()
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }

        Trajectory trajectory;
        List<Trajectory> trajectories;
        IntegratedNoiseModel noiseModel;
        TemporalGrid temporalGrid;
        ReferencePoint referencePoint;

        #region "Calculate Noise"

        public void CalculateNoise()
        {
            startTime = DateTime.Now;
            referencePoint = new ReferencePointRD();
            if (_view.CustomReference)
            {
                referencePoint = new ReferencePoint(_view.GeoReference, _view.MetricReference);
            }

            if (!_view.ExternalNoise)
            {
                if (_view.OneTrajectory)
                {
                    thread = new Thread(OneTrajectoryINM);
                } else
                {
                    thread = new Thread(MultipleTrajectoriesINM);
                }
                thread.Start();
            }
        }
        
        protected void OneTrajectoryINM()
        {
            var aircraft = new Aircraft("GP7270", "wing");
            var trajectoryGenerator = new TrajectoryGenerator(aircraft, referencePoint);

            if (_view.TrajectoryFile.Contains("."))
            {
                var reader = new TrajectoryFileReader(CoordinateUnit.metric, trajectoryGenerator);
                trajectory = reader.CreateTrajectoryFromFile(_view.TrajectoryFile);
            }

            trajectory.Aircraft = aircraft;
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.NoiseMetric = _view.NoiseMetric;

            noiseModel.StartCalculation(ProgressChanged);
            temporalGrid = noiseModel.TemporalGrid;
            _view.Invoke(delegate { _view.NoiseCalculationCompleted(); });
        }

        protected void MultipleTrajectoriesINM()
        {
            var reader = new TrajectoriesFileReader();
            trajectories = reader.CreateFromFile(_view.TrajectoryFile, referencePoint);

            // Calculate the noise for each trajectory
            temporalGrid = new TemporalGrid();
            int counter = 0;
            foreach (Trajectory trajectory in trajectories)
            {
                double percentage = (double) counter / trajectories.Count * 100.0;
                ProgressChanged(percentage);
                Console.WriteLine("INM "+counter+" started");
                if (counter > 4) { break; }

                var INM = new IntegratedNoiseModel(trajectory, trajectory.Aircraft);
                INM.GridName = "schiphol_grid2D";
                INM.NoiseMetric = _view.NoiseMetric;
                INM.RunINMFullTrajectory();

                Grid grid = INM.TemporalGrid.GetGrid(0);
                grid.ReferencePoint = referencePoint;
                temporalGrid.AddGrid(grid);
                Console.WriteLine("INM " + counter + " completed");
                counter++;
            }

            _view.Invoke(delegate { _view.NoiseCalculationCompleted(); });
        }

        public void VisualiseTrajectory()
        {
            trajectory = _view.Trajectory;
        }

        #endregion




        #region "Prepare Visualisation"

        public void PrepareVisualisation()
        {
            startTime = DateTime.Now;

            if (_view.OneTrajectory)
            {
                thread = new Thread(OneTrajectoryVisualisation);
            }
            else
            {
                thread = new Thread(MultipleTrajectoryVisualisation);
            }
            thread.Start();
        }


        protected void OneTrajectoryVisualisation()
        {
            // Value conversion
            var localTemporalGrid = temporalGrid;
            if (_view.ValueConversion == "Max")
            {
                var g = new GridConverter(temporalGrid, GridTransformation.MAX);
                localTemporalGrid = g.transform();
            }

            // Create legend
            var legend = new LegendCreator();
            legend.OutputLegendImage();
            legend.OutputLegendTitle();

            // Contour animator
            List<double> contoursOfInterest = (_view.VisualiseContoursOfInterest) ? _view.ContoursOfInterest : null;
            var contourAnimator = new ContourKMLAnimator(localTemporalGrid, trajectory, contoursOfInterest);
            if (_view.VisualiseGradient) {
                contourAnimator.SetGradientSettings(_view.LowestContourValue, _view.HighestContourValue, _view.ContourValueStep);
            }

            // Create sections
            var sections = new List<KMLAnimatorSectionInterface>() {
                new LegendKMLAnimator(),
                new AircraftKMLAnimator(trajectory.Aircraft, trajectory),
                new AirplotKMLAnimator(trajectory),
                new GroundplotKMLAnimator(trajectory),
                contourAnimator
                //new AnnoyanceKMLAnimator(temporalGrid, population.getPopulationData())
            };
            if (_view.Heatmap)
            {
                var population = new PopulationData(Globals.currentDirectory + "population.dat");
                var section = new HeatmapKMLAnimator(population);
                sections.Add(section);
            }

            // Create animator
            var camera = new FollowKMLAnimatorCamera(trajectory.Aircraft, trajectory);
            var animator = new KMLAnimator(sections, camera);
            animator.AnimationToFile(trajectory.Duration, Globals.webrootDirectory + "visualisation.kml");

            _view.Invoke(delegate { _view.PreparationCalculationCompleted(); });
        }


        protected void MultipleTrajectoryVisualisation()
        {
            // Value conversion
            var localTemporalGrid = temporalGrid;
            if (_view.ValueConversion == "Max")
            {
                var g = new GridConverter(temporalGrid, GridTransformation.MAX);
                localTemporalGrid = g.transform();
            }

            // Legend
            var legend = new LegendCreator();

            // Contour animator
            List<double> contoursOfInterest = (_view.VisualiseContoursOfInterest) ? _view.ContoursOfInterest : null;
            var contourAnimator = new ContourKMLAnimator(localTemporalGrid, null, contoursOfInterest);
            if (_view.VisualiseGradient)
            {
                legend.SetSettings(_view.LowestContourValue, _view.HighestContourValue);
                contourAnimator.SetGradientSettings(_view.LowestContourValue, _view.HighestContourValue, _view.ContourValueStep);
            }
            contourAnimator.AltitudeOffset = (_view.MapFile != "");

            // plot legend
            legend.OutputLegendImage();
            legend.OutputLegendTitle();

            // Create sections
            var sections = new List<KMLAnimatorSectionInterface>() {
                new LegendKMLAnimator(),
                contourAnimator,
                new MultipleGroundplotKMLAnimator(trajectories)
            };
            if (_view.MapFile != "")
            {
                sections.Add(new CustomMapKMLAnimator(_view.MapFile, _view.MapBottomLeft, _view.MapUpperRight));
            }
            if (_view.Heatmap)
            {
                var population = new PopulationData(Globals.currentDirectory + "population.dat");
                var section = new HeatmapKMLAnimator(population);
                sections.Add(section);
            }

            // Create animator
            var camera = new TopViewKMLAnimatorCamera(new GeoPoint3D(referencePoint.GeoPoint.Longitude, referencePoint.GeoPoint.Latitude, 15000));
            var animator = new KMLAnimator(sections, camera);
            animator.Duration = 0;
            animator.AnimationToFile(localTemporalGrid.GetNumberOfGrids(), Globals.webrootDirectory + "visualisation.kml");

            _view.Invoke(delegate { _view.PreparationCalculationCompleted(); });
        }

        #endregion





        protected void ProgressChanged(double progress)
        {
            _view.Invoke(delegate
            {
                if (progress == 0) { return; }

                double factor = progress / 100;
                double secElapsed = DateTime.Now.Subtract(startTime).TotalSeconds;

                _view.Percentage = (int)(factor * 100);
                _view.TimeLeft = (int)Math.Ceiling(((secElapsed / factor) - secElapsed) / 60.0);
            });
        }

    }
}
