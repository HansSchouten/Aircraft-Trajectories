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
        IntegratedNoiseModel noiseModel;

        [Test]
        public void t1_TemporalGrid()
        { 
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(fullTestCompleted);
        }

        private void fullTestCompleted()
        {
            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            Assert.AreNotEqual(0, temporalGrid.GetNumberOfGrids());
            
            Grid temp = new Grid(null);
            temporalGrid.AddGrid(temp);
            Assert.IsNotNull(temporalGrid.GetGrid(0));
            Assert.IsNull(temporalGrid.GetGrid(1));

            TemporalGrid temp1 = new TemporalGrid();
            temp1.AddGrid(temporalGrid.GetGrid(0));
            Assert.AreEqual(temp1.GetGrid(0), temporalGrid.GetGrid(0));
            Assert.AreEqual(temp, temporalGrid.GetGrid(0));
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
    }
}