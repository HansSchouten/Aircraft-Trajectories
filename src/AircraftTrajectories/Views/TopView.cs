using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Models.Visualisation.KML;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
