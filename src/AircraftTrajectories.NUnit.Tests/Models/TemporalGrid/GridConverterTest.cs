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
        IntegratedNoiseModel noiseModel;
        GridConverter converter;
        bool testRunned = false;

        public GridTransformation MAX { get; private set; }

        [Test]
        public void GridConverter()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Aircraft aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(test);

            while (!testRunned) { }

            converter = new GridConverter(noiseModel.TemporalGrid, MAX);
            TemporalGrid res = converter.transform();
            Assert.IsNotNull(converter);

            Assert.AreEqual(2, res.GetNumberOfGrids());
            Assert.AreEqual(1, res.Interval);
            Assert.AreEqual("25.9", res.GetGrid(1).Data[65][60].ToString());
        }

        private void test()
        {
            testRunned = true;
        }
    }
}
