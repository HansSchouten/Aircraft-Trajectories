using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Models.Visualisation.KML;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using MathNet.Numerics.Interpolation;

namespace AircraftTrajectories.Views
{
    public partial class TopView : Form
    {
        public TopView()
        {
            InitializeComponent();
        }

        Aircraft aircraft;
        Trajectory trajectory;
        IntegratedNoiseModel noiseModel;

        private void TopView_Load(object sender, EventArgs e)
        {
            string rawTrackData = File.ReadAllText(Globals.currentDirectory + "inbound.txt");
            var _trackData = rawTrackData
                .Split('\n')
                .Select(q =>
                    q.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(Convert.ToString)
                     .ToArray()
                )
                .ToArray();

            string flight_id = "";
            var _tData = new List<double>();
            var _xData = new List<double>();
            var _yData = new List<double>();
            var _zData = new List<double>();
            var trajectories = new List<Trajectory>();
            double xMin = int.MaxValue, yMin = int.MaxValue, xMax = int.MinValue, yMax = int.MinValue;
            double xMinT = int.MaxValue, yMinT = int.MaxValue, xMaxT = int.MinValue, yMaxT = int.MinValue;
            for (int i = 0; i < _trackData.Length; i++)
            {
                if (_trackData[i][0] != flight_id && i > 0)
                {
                    var xSpline = CubicSpline.InterpolateNatural(_tData, _xData);
                    var ySpline = CubicSpline.InterpolateNatural(_tData, _yData);
                    var zSpline = CubicSpline.InterpolateNatural(_tData, _zData);
                    trajectories.Add(new Trajectory(xSpline, ySpline, zSpline, null, null));
                    
                    _tData = new List<double>();
                    _xData = new List<double>();
                    _yData = new List<double>();
                    _zData = new List<double>();
                    MessageBox.Show(xMinT + "," + yMinT + " - " + xMaxT + "," + yMaxT);
                    xMinT = int.MaxValue;
                    yMinT = int.MaxValue;
                    xMaxT = int.MinValue;
                    yMaxT = int.MinValue;
                }
                flight_id = _trackData[i][0];

                _tData.Add(i);
                double x = Convert.ToDouble(_trackData[i][4]);
                double y = Convert.ToDouble(_trackData[i][5]);
                double z = Convert.ToDouble(_trackData[i][6]) * 0.3040 * 100;
                _xData.Add(x);
                _yData.Add(y);
                _zData.Add(z);
                xMin = Math.Min(xMin, x);
                xMax = Math.Max(xMax, x);
                yMin = Math.Min(yMin, y);
                yMax = Math.Max(yMax, y);
                xMinT = Math.Min(xMinT, x);
                xMaxT = Math.Max(xMaxT, x);
                yMinT = Math.Min(yMinT, y);
                yMaxT = Math.Max(yMaxT, y);
            }
            MessageBox.Show(xMin + "," + yMin + " - " + xMax + "," + yMax);
            return;

            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            trajectory = reader.createTrajectoryFromFile(Globals.currentDirectory + "track_schiphol.dat");

            aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.GridName = "schiphol_grid2D";
            noiseModel.StartCalculation(calculationCompleted, null);
        }

        private void calculationCompleted()
        {
            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            var convertor = new GridConverter(temporalGrid, GridTransformation.MAX);
            var LAMaxTemporalGrid = convertor.transform();
            temporalGrid = LAMaxTemporalGrid;
            //temporalGrid = new TemporalGrid();
            //temporalGrid.AddGrid(LAMaxTemporalGrid.GetGrid(LAMaxTemporalGrid.GetNumberOfGrids() - 1));
            temporalGrid.LowerLeftCorner = new Point3D(104062, 475470, 0, CoordinateUnit.metric);
            temporalGrid.GridSize = 125;

            var camera = new TopViewKMLAnimatorCamera(aircraft, trajectory);
            var sections = new List<KMLAnimatorSectionInterface>() {
                new ContourKMLAnimator(temporalGrid, trajectory, new List<int>() { })
            };
            var animator = new KMLAnimator(sections, camera);
            animator.AnimationToFile(temporalGrid.GetNumberOfGrids(), Globals.currentDirectory + "topview_fullpath.kml");
        }
    }
}
