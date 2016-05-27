using NUnit.Framework;
using System;
using AircraftTrajectories.Models.Space3D;
using System.Windows.Forms;

namespace AircraftTrajectories.NUnit.Tests.Models.Space3D
{
    [TestFixture]
    class GeoPoint3DTest
    {
        [Test]
        public void GeoPointConstructorTest()
        {
            GeoPoint3D point = new GeoPoint3D(1.5, 2.3, 4.5);
            Assert.IsNotNull(point);
        }

        [Test]
        public void GeoPointLatTest()
        {
            GeoPoint3D point = new GeoPoint3D(1.5, 2.3, 4.5);
            Assert.AreEqual(2.3, Math.Round(point.Latitude, 2));
        }

        [Test]
        public void GeoPointLongTest()
        {
            GeoPoint3D point = new GeoPoint3D(1.5, 2.3, 4.5);
            Assert.AreEqual(1.5, point.Longitude);
        }

        [Test]
        public void GeoPointAltitudeTest()
        {
            GeoPoint3D point = new GeoPoint3D(1.5, 2.3, 4.5);
            Assert.AreEqual(4.5, point.Z);
        }

        [Test]
        public void GeoPointMoveDirectionTest()
        {
            GeoPoint3D point = new GeoPoint3D(1.5, 2.3, 4.5);
            GeoPoint3D movedPoint = point.MoveInDirection(12.5, 50);

            GeoPoint3D destination = new GeoPoint3D(0, 100, 100);

            Assert.AreEqual(1.5, movedPoint.Longitude, 0.001);
        }

        [Test]
        public void GeoPointHeadingToTest()
        {
            GeoPoint3D point = new GeoPoint3D(1.5, 2.3, 4.5);
            GeoPoint3D destination = new GeoPoint3D(8.5, 4.5, 7.8);
            double res = point.HeadingTo(destination);

            Assert.AreEqual(252.523, res, 0.001);
        }
        
    }
}
