
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

        [Test]
        public void TrajectoryReaderDurationTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);

            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(1, obj.Duration, 0.1);
        }

        [Test]
        public void TrajectoryReaderAirspeedTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);

            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(102.8, obj.Airspeed(1550), 0.1);
        }

        [Test]
        public void TrajectoryReaderBankAngleTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);

            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(0.01904, obj.BankAngle(45), 0.1);
        }

        [Test]
        public void TrajectoryReaderLongitudeTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);

            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(5.912, obj.Longitude(55), 0.1);
        }

        [Test]
        public void TrajectoryReaderLatitudeTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);

            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(50.922, obj.Latitude(55), 0.1);
        }

        [Test]
        public void TrajectoryReaderCoordinateUnitsTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);

            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(CoordinateUnit.metric, obj.Point3D(55).CoordinateUnits);
        }

        [Test]
        public void TrajectoryReaderCoordinatesTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);

            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(197362.42, obj.Point3D(55).X, 0.1);
            Assert.AreEqual(328168.06, obj.Point3D(55).Y, 0.1);
            Assert.AreEqual(39508.31, obj.Point3D(55).Z, 0.1);
        }

        [Test]
        public void ReaderCoordinatesTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);

            Assert.AreEqual(CoordinateUnit.metric, reader.CoordinateUnits);
        }

    }
}