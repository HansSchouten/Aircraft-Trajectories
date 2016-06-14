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
        public void ConvertGeographic()
        {
            x = 122202;
            y = 487250;
            RDToGeographic converter = new RDToGeographic();
            PointF point = converter.convertToLatLong(x, y);

            Assert.AreEqual("4.905598", point.X.ToString());
            Assert.AreEqual("52.37214", point.Y.ToString());
        }

        [Test]
        public void ConvertFormatGeographic()
        {
            x = 122202;
            y = 487250;
            RDToGeographic converter = new RDToGeographic();
            PointF point = converter.convertToLatLong(x, y);
            
            Assert.AreEqual("4.905598, 52.37214",
            string.Format("{0}, {1}",
                point.X.ToString(CultureInfo.InvariantCulture.NumberFormat),
                point.Y.ToString(CultureInfo.InvariantCulture.NumberFormat))
            );
        }

        [Test]
        public void convertZeroToGeographic()
        {
            var converter = new MetricToGeographic();
            var metricPoint = converter.ConvertToLongLat(122202, 487250);

            Assert.AreEqual(52.37214, metricPoint.Y, 0.001);
            Assert.AreEqual(4.9056, metricPoint.X, 0.001);
        }

        [Test]
        public void convertLargeToGeographic()
        {
            var converter = new MetricToGeographic();
            var metricPoint = converter.ConvertToLongLat(60000000, 487250143);

            Assert.AreEqual(-4460808704, metricPoint.Y, 0.001);
            Assert.AreEqual(116939120640, metricPoint.X, 0.001);
        }

        [Test]
        public void convertNegativeToGeographic()
        {
            var converter = new MetricToGeographic();
            var metricPoint = converter.ConvertToLongLat(-122202, -487250);

            Assert.AreEqual(43.568, metricPoint.Y, 0.001);
            Assert.AreEqual(1.97292, metricPoint.X, 0.001);
        }

    }

}
