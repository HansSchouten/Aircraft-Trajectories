using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.TemporalGrid
{
    using AircraftTrajectories.Models.IntegratedNoiseModel;
    using AircraftTrajectories.Models.TemporalGrid;
	
    [TestFixture]
    public class TemporalGridTest
    {
        IntegratedNoiseModel noiseModel;

        [Test]
        public void TemporalGrid()
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
    }
}