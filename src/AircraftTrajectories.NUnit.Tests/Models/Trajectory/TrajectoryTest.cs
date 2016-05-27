using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using NUnit.Framework;
using System;

namespace AircraftTrajectories.NUnit.Tests.Trajectory
{

    [TestFixture]
    public class TrajectoryTest
    {

        [Test]
        public void Trajectory()
        {
            TrajectoryFileReader reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.IsNotNull(obj);
        }

        [Test]
        public void TrajectoryXYZ()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(110640.78, obj.X(0.1), 0.001);
            Assert.AreEqual(478092.374, obj.Y(0.1), 0.001);
            Assert.AreEqual(14.3085, obj.Z(0.1), 0.001);
            Assert.AreEqual(52.289, obj.Latitude(0.1), 0.001);
            Assert.AreEqual(4.737, obj.Longitude(0.1), 0.001);
        }

        [Test]
        public void TrajectoryLongLat()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(52.289, obj.Latitude(0.1), 0.001);
            Assert.AreEqual(4.737, obj.Longitude(0.1), 0.001);
        }

        [Test]
        public void TrajectoryGeoPoint()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            GeoPoint3D geopoint = obj.GeoPoint(2.5);
            Assert.IsNotNull(geopoint);
            Assert.AreEqual(52.287, geopoint.Latitude, 0.001);
            Assert.AreEqual(4.731, geopoint.Longitude, 0.001);
            Assert.AreEqual(101.974, geopoint.Z, 0.001);
        }

        [Test]
        public void TrajectoryPoint()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Point3D point = obj.Point3D(0.05);
            Assert.IsNotNull(point);
            Assert.AreEqual(110649.39, point.X, 0.001);
            Assert.AreEqual(478097.687, point.Y, 0.001);
            Assert.AreEqual(12.488, point.Z, 0.001);
        }

        [Test]
        public void AirspeedTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(102.8, obj.Airspeed(2), 0.001);
        }

        [Test]
        public void TiltTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(22.455, obj.Tilt(3.5), 0.001);
        }

        [Test]
        public void HeadingTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(270.0003, obj.Heading(4.2), 0.001);
        }

        [Test]
        public void BankAngleLessTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(0, obj.BankAngle(-20));
        }

        [Test]
        public void BankAngleMoreTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(0.019, obj.BankAngle(45), 0.001);
        }

        [Test]
        public void BankAngleInfinityTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual(39.286, obj.BankAngle(10), 0.001);
        }

    }
}