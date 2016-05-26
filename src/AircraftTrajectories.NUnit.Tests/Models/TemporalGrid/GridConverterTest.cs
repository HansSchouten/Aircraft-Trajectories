using NUnit.Framework;


namespace AircraftTrajectories.NUnit.Tests.Models.TemporalGrid
{
    using AircraftTrajectories.Models.Space3D;
    using AircraftTrajectories.Models.TemporalGrid;
    using AircraftTrajectories.Models.IntegratedNoiseModel;
    using AircraftTrajectories.Models.Trajectory;
    using System.Windows.Forms;
    [TestFixture]
    class GridConverterTest
    {
        IntegratedNoiseModel noiseModel_MAX;
        IntegratedNoiseModel noiseModel_SEL;

        GridConverter converter_MAX;
        GridConverter converter_SEL;

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
            noiseModel_MAX = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel_MAX.StartCalculation(testMAX);

            while (!testRunned_MAX) { }

            converter_MAX = new GridConverter(noiseModel_MAX.TemporalGrid, MAX);
            TemporalGrid res = converter_MAX.transform();
            Assert.IsNotNull(converter_MAX);

            Assert.AreEqual(2, res.GetNumberOfGrids());
            Assert.AreEqual(1, res.Interval);
            Assert.AreEqual("25.9", res.GetGrid(1).Data[65][60].ToString());
        }

        private void testMAX()
        {
            testRunned_MAX = true;
        }

        [Test]
        public void GridConverterSEL()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            noiseModel_SEL = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel_SEL.StartCalculation(testSEL);

            while (!testRunned_SEL) { }

            converter_SEL = new GridConverter(noiseModel_SEL.TemporalGrid, SEL);
            TemporalGrid res = converter_SEL.transform();
            Assert.IsNotNull(converter_SEL);

            Assert.AreEqual(2, res.GetNumberOfGrids());
            Assert.AreEqual(1, res.Interval);
            Assert.AreEqual("25.9", res.GetGrid(1).Data[65][60].ToString());
        }

        private void testSEL()
        {
            testRunned_SEL = true;
        }
    }
}

