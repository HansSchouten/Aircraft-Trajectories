
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Trajectory
{

    [TestFixture]
    public class TrajectoryFileReaderTest
    {
        [Test]
        public void TrajectoryFileReader()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            Assert.IsNotNull(reader);

            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
        }

    }
}
