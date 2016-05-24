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

            Assert.AreEqual("110640.78", Math.Round(obj.X(0.1), 3).ToString());
            Assert.AreEqual("478092.374", Math.Round(obj.Y(0.1), 3).ToString());
            Assert.AreEqual("14.309", Math.Round(obj.Z(0.1), 3).ToString());
            Assert.AreEqual("52.289", Math.Round(obj.Latitude(0.1), 3).ToString());
            Assert.AreEqual("4.737", Math.Round(obj.Longitude(0.1), 3).ToString());
        }

        [Test]
        public void TrajectoryLongLat()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Assert.AreEqual("52.289", Math.Round(obj.Latitude(0.1), 3).ToString());
            Assert.AreEqual("4.737", Math.Round(obj.Longitude(0.1), 3).ToString());
        }

        [Test]
        public void TrajectoryGeoPoint()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            GeoPoint3D geopoint = obj.GeoPoint(2.5);
            Assert.IsNotNull(geopoint);
            Assert.AreEqual("52.287", Math.Round(geopoint.Latitude, 3).ToString());
            Assert.AreEqual("4.731", Math.Round(geopoint.Longitude, 3).ToString());
            Assert.AreEqual("101.974", Math.Round(geopoint.Z, 3).ToString());
        }

        [Test]
        public void TrajectoryPoint()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var obj = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            Point3D point = obj.Point3D(0.05);
            Assert.IsNotNull(point);
            Assert.AreEqual("110649.39", Math.Round(point.X, 3).ToString());
            Assert.AreEqual("478097.687", Math.Round(point.Y, 3).ToString());
            Assert.AreEqual("12.488", Math.Round(point.Z, 3).ToString());
        }


    }
}