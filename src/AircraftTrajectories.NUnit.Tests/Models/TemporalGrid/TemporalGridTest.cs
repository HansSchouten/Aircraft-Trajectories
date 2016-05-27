using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.TemporalGrid
{
    using AircraftTrajectories.Models.IntegratedNoiseModel;
    using AircraftTrajectories.Models.TemporalGrid;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    [TestFixture]
    public class TemporalGridTest
    {
        protected bool completed = false;
        IntegratedNoiseModel noiseModel;

        [Test]
        public void t1_TemporalGrid()
        { 
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            int numberOfGrids = temporalGrid.GetNumberOfGrids();
            Assert.AreNotEqual(0, numberOfGrids);

            Grid temp = new Grid(new double[1][] { new double[] { 0 } });
            temporalGrid.AddGrid(temp);
            Assert.IsNotNull(temporalGrid.GetGrid(0));
            Assert.AreEqual(numberOfGrids+1, temporalGrid.GetNumberOfGrids());
            Assert.AreEqual(temp, temporalGrid.GetGrid(numberOfGrids));

            completed = false;
        }

        [Test]
        public void t2_GridCoordinateXTest()
        {
            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            GeoPoint3D point = temporalGrid.GridCoordinate(1.5, 4);
         
            Assert.AreEqual(47.979, Math.Round(point.Latitude, 3));
        }

        [Test]
        public void t3_GridCoordinateYTest()
        {
            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            GeoPoint3D point = temporalGrid.GridCoordinate(1.5, 4);

            Assert.AreEqual(3.316, Math.Round(point.Longitude, 3));
        }

        [Test]
        public void t4_GridCoordinateZTest()
        {
            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            GeoPoint3D point = temporalGrid.GridCoordinate(1.5, 4);

            Assert.AreEqual(0.0, Math.Round(point.Z, 1));
        }

        [Test]
        public void t5_GridCoordinateToGridXTest()
        {
            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            Point point = temporalGrid.CoordinateToGridIndex(1.5, 4);

            Assert.AreEqual(0, point.X);
        }

        [Test]
        public void t6_GridCoordinateToGridYTest()
        {
            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            Point point = temporalGrid.CoordinateToGridIndex(1.5, 4);

            Assert.AreEqual(0, point.Y);
        }

        private void INMCompleted()
        {
            completed = true;
        }

    }
}