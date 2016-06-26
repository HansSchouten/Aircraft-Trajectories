using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.TemporalGrid
{
    using AircraftTrajectories.Models.IntegratedNoiseModel;
    using AircraftTrajectories.Models.TemporalGrid;
    using System;
    using System.Drawing;

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

            Grid temp = new Grid(new double[1][] { new double[] { 0 } }, null);
            temporalGrid.AddGrid(temp);
            Assert.IsNotNull(temporalGrid.GetGrid(0));
            Assert.AreEqual(numberOfGrids+1, temporalGrid.GetNumberOfGrids());
            Assert.AreEqual(temp, temporalGrid.GetGrid(numberOfGrids));

            completed = false;
        }

        [Test]
        public void t2_GridCoordinateXTest()
        {
            Grid grid = noiseModel.TemporalGrid.GetGrid(0);
            GeoPoint3D point = grid.GridGeoCoordinate(1.5, 4);
         
            Assert.AreEqual(52.2689, point.Latitude, 0.001);
        }

        [Test]
        public void t3_GridCoordinateYTest()
        {
            Grid grid = noiseModel.TemporalGrid.GetGrid(0);
            GeoPoint3D point = grid.GridGeoCoordinate(1.5, 4);

            Assert.AreEqual(4.6440, point.Longitude, 0.001);
        }

        [Test]
        public void t4_GridCoordinateZTest()
        {
            Grid grid = noiseModel.TemporalGrid.GetGrid(0);
            GeoPoint3D point = grid.GridGeoCoordinate(1.5, 4);

            Assert.AreEqual(0.0, Math.Round(point.Z, 1));
        }

        [Test]
        public void t5_GridCoordinateToGridXTest()
        {
            Grid grid = noiseModel.TemporalGrid.GetGrid(0);
            Point point = grid.CoordinateToGridIndex(110658, 478103);

            Assert.AreEqual(53, point.X);
        }

        [Test]
        public void t6_GridCoordinateToGridYTest()
        {
            Grid grid = noiseModel.TemporalGrid.GetGrid(0);
            Point point = grid.CoordinateToGridIndex(110658, 478103);

            Assert.AreEqual(21, point.Y);
        }

        private void INMCompleted()
        {
            completed = true;
        }

    }
}