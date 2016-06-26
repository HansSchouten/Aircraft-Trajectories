using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Population;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Models.Visualisation;
using AircraftTrajectories.Models.Visualisation.KML;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AircraftTrajectories.Views
{

    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            var aircraft = new Aircraft("GP7270", "wing");
            var generator = new TrajectoryGenerator(aircraft, new ReferencePointRD());
            var reader = new TrajectoryFileReader(CoordinateUnit.metric, generator);
            var trajectory = reader.CreateTrajectoryFromFile(Globals.currentDirectory + "track_schiphol.dat");
            

            for (int t=0; t < trajectory.Duration; t++)
            {
                chartGroundPath.Series["Groundpath"].Points.AddXY(trajectory.Longitude(t), trajectory.Latitude(t));
            }
            AutoScaleChart(chartGroundPath);
            chartGroundPath.ChartAreas[0].AxisY.LabelStyle.Format = "{0.00}";
            chartGroundPath.ChartAreas[0].AxisX.LabelStyle.Format = "{0.00}";
            chartGroundPath.Series["Groundpath"].ChartType = SeriesChartType.FastLine;




            for (int t = 0; t < trajectory.Duration; t++)
            {
                chartAltitude.Series["Altitude [m]"].Points.AddXY(t, trajectory.Z(t));
            }
            chartAltitude.Series["Altitude [m]"].ChartType = SeriesChartType.FastLine;


            /*
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            trajectory = reader.createTrajectoryFromFile(Globals.currentDirectory + "track_schiphol.dat");

            aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(progressChanged);

            var legend = new LegendCreator();
            legend.OutputLegendImage();
            legend.OutputLegendTitle();

            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            //temporalGrid.LowerLeftCorner = new Point3D(104062, 475470, 0, CoordinateUnit.metric);
            //temporalGrid.GridSize = 125;

            var population = new PopulationData(Globals.currentDirectory + "population.dat");

            var camera = new FollowKMLAnimatorCamera(aircraft, trajectory);
            var sections = new List<KMLAnimatorSectionInterface>() {
                new LegendKMLAnimator(),
                new AircraftKMLAnimator(aircraft, trajectory),
                new AirplotKMLAnimator(trajectory),
                new GroundplotKMLAnimator(trajectory),
                new ContourKMLAnimator(temporalGrid, trajectory, new List<int>() { 65, 70, 75 }),
                new AnnoyanceKMLAnimator(temporalGrid, population.getPopulationData())
            };
            var animator = new KMLAnimator(sections, camera);
            animator.AnimationToFile(trajectory.Duration, Globals.currentDirectory + "test.kml");

            GoogleEarthServerForm googleEarthForm = new GoogleEarthServerForm();
            
            this.Hide();
            googleEarthForm.Closed += (s, args) => this.Close();
            googleEarthForm.Show();
            */
        }

        protected void progressChanged(double newProgress)
        {

        }

        protected void AutoScaleChart(Chart chart)
        {
            var points = chart.Series[0].Points;
            var minY = points.Min(y => y.YValues[0]);
            var maxY = points.Max(y => y.YValues[0]);
            var minX = points.Min(x => x.XValue);
            var maxX = points.Max(x => x.XValue);
            var yBound = (maxY - minY) * 0.05;
            var xBound = (maxX - minX) * 0.05;
            chart.ChartAreas[0].AxisY.Minimum = minY - yBound;
            chart.ChartAreas[0].AxisY.Maximum = maxY + yBound;
            chart.ChartAreas[0].AxisX.Minimum = minX - xBound;
            chart.ChartAreas[0].AxisX.Maximum = maxX + xBound;
        }
    }
}