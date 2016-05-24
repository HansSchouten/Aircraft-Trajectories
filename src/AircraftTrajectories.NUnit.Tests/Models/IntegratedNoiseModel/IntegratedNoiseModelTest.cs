using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.IntegratedNoiseModel
{
    [TestFixture]
    public class IntegratedNoiseModelTest
    {

        [Test]
        public void IntegratedNoiseModel()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(@"AircraftTrajectories\bin\Debug\track_schiphol.dat");

            //aircraft = new Aircraft("GP7270", "wing");
            //noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            //noiseModel.StartCalculation(calculationCompleted, pbAnimation);
        }
        

    }

}
