using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using AircraftTrajectories.Models.Space3D;
using System.Globalization;

namespace AircraftTrajectories.NUnit.Tests.Models.Space3D
{
    [TestFixture]
    public class RDToGeographicTest
    {
        double x;
        double y;

        [Test]
        public void ConvertLatLong()
        {
            x = 122202;
            y = 487250;
            RDToGeographic converter = new RDToGeographic();
            PointF point = converter.convertToLatLong(x, y);
            Assert.AreEqual("4.905598", point.X.ToString());
            Assert.AreEqual("52.37214", point.Y.ToString());
            Assert.AreEqual("4.905598, 52.37214", 
            string.Format("{0}, {1}",
                point.X.ToString(CultureInfo.InvariantCulture.NumberFormat),
                point.Y.ToString(CultureInfo.InvariantCulture.NumberFormat))
            );
        }

        [Test]
        public void ConvertLatLongCustom()
        {
            int referenceRdX = 155000;
            int referenceRdY = 463000;
            double referenceWgs84X = 52.15517;
            double referenceWgs84Y = 5.387206;

            x = 122202;
            y = 487250;
            var refPoint = new Point3D(referenceRdX, referenceRdY, 0, CoordinateUnit.metric);
            var conPoint = new Point3D(x, y, 0, CoordinateUnit.metric);
            double heading = refPoint.HeadingTo(conPoint);
            var refGeoPoint = new GeoPoint3D(referenceWgs84Y, referenceWgs84X, 0);
            var conGeoPoint = refGeoPoint.MoveInDirection(conPoint.DistanceTo(refPoint), heading);

            Assert.AreEqual(52.37214, conGeoPoint.Latitude, 0.001);
            Assert.AreEqual(4.905598, conGeoPoint.Longitude, 0.001);
        }

    }

}
