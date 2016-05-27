using NUnit.Framework;


namespace AircraftTrajectories.NUnit.Tests.Models.TemporalGrid
{
    using AircraftTrajectories.Models.Space3D;
    using AircraftTrajectories.Models.TemporalGrid;
    using AircraftTrajectories.Models.IntegratedNoiseModel;
    using AircraftTrajectories.Models.Trajectory;
    using System.Windows.Forms;
    using System;
    [TestFixture]
    class GridConverterTest
    {
        bool testRunned_MAX = false;
        bool testRunned_SEL = false;

        public GridTransformation MAX { get; private set; }
        public GridTransformation SEL { get; private set; }

        [Test]
        public void GridConverterMAX()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            var noiseModel_MAX = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel_MAX.StartCalculation(testMAX);

            while (!testRunned_MAX) { }

            var converter_MAX = new GridConverter(noiseModel_MAX.TemporalGrid, GridTransformation.MAX);
            TemporalGrid res = converter_MAX.transform();
            Assert.IsNotNull(converter_MAX);

            testRunned_MAX = false;
        }

        [Test]
        public void GridConverterMAXNumberofGrids()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            var noiseModel_MAX = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel_MAX.StartCalculation(testMAX);

            while (!testRunned_MAX) { }

            var converter_MAX = new GridConverter(noiseModel_MAX.TemporalGrid, GridTransformation.MAX);
            TemporalGrid res = converter_MAX.transform();

            Assert.AreEqual(2, res.GetNumberOfGrids());

            testRunned_MAX = false;
        }

        [Test]
        public void GridConverteMAXInterval()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            var noiseModel_MAX = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel_MAX.StartCalculation(testMAX);

            while (!testRunned_MAX) { }

            var converter_MAX = new GridConverter(noiseModel_MAX.TemporalGrid, GridTransformation.MAX);
            TemporalGrid res = converter_MAX.transform();

            Assert.AreEqual(1, res.Interval);

            testRunned_MAX = false;
        }

        [Test]
        public void GridConverteMAXData()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            var noiseModel_MAX = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel_MAX.StartCalculation(testMAX);

            while (!testRunned_MAX) { }

            var converter_MAX = new GridConverter(noiseModel_MAX.TemporalGrid, GridTransformation.MAX);
            TemporalGrid res = converter_MAX.transform();
            Assert.IsNotNull(converter_MAX);

            Assert.AreEqual(25.9, res.GetGrid(1).Data[65][60], 0.01);

            testRunned_MAX = false;

        }

        private void testMAX()
        {
            testRunned_MAX = true;
        }

        [Test]
        public void GridConverterSELNumberOfGrids()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            var noiseModel_SEL = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel_SEL.StartCalculation(testSEL);

            while (!testRunned_SEL) { }

            var converter_SEL = new GridConverter(noiseModel_SEL.TemporalGrid, GridTransformation.SEL);
            TemporalGrid res = converter_SEL.transform();
            Assert.IsNotNull(converter_SEL);

            Assert.AreEqual(2, res.GetNumberOfGrids());

            testRunned_SEL = false;

        }

        [Test]
        public void GridConverterSELInterval()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            var noiseModel_SEL = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel_SEL.StartCalculation(testSEL);

            while (!testRunned_SEL) { }

            var converter_SEL = new GridConverter(noiseModel_SEL.TemporalGrid, GridTransformation.SEL);
            TemporalGrid res = converter_SEL.transform();

            Assert.AreEqual(1, res.Interval);

            testRunned_SEL = false;
        }

        [Test]
        public void GridConverterSELData()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            var noiseModel_SEL = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel_SEL.StartCalculation(testSEL);

            while (!testRunned_SEL) { }

            var converter_SEL = new GridConverter(noiseModel_SEL.TemporalGrid, GridTransformation.SEL);
            TemporalGrid res = converter_SEL.transform();

            Assert.AreEqual(30.67, Math.Round(res.GetGrid(1).Data[65][60], 2), 0.001);

            testRunned_SEL = false;
        }

        private void testSEL()
        {
            testRunned_SEL = true;
        }
    }
}

