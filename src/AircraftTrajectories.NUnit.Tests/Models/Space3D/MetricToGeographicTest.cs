using AircraftTrajectories.Models.Space3D;
using NUnit.Framework;
using System;

namespace AircraftTrajectories.NUnit.Tests.Models.Space3D
{
    [TestFixture]
    class MetricToGeographicTest
    {
        [Test]
        public void convertToLatLong()
        {
            var converter = new MetricToGeographic();
            var metricPoint = converter.ConvertToLongLat(122202, 487250);

            Assert.AreEqual(52.372, metricPoint.Y, 0.001);
            Assert.AreEqual(4.906, metricPoint.X, 0.001);
        }

        [Test]
        public void convertZeroToLatLong()
        {
            var converter = new MetricToGeographic();
            var metricPoint = converter.ConvertToLongLat(122202, 487250);

            Assert.AreEqual(52.37214, metricPoint.Y, 0.001);
            Assert.AreEqual(4.90559, metricPoint.X, 0.001);
        }

        [Test]
        public void convertLargeToLatLong()
        {
            var converter = new MetricToGeographic();
            var metricPoint = converter.ConvertToLongLat(60000000, 487250143);

            Assert.AreEqual(-4460808704, metricPoint.Y, 0.001);
            Assert.AreEqual(116939120640, metricPoint.X, 0.001);
        }

        [Test]
        public void convertNegativeToLatLong()
        {
            var converter = new MetricToGeographic();
            var metricPoint = converter.ConvertToLongLat(-122202, -487250);

            Assert.AreEqual(43.568, metricPoint.Y, 0.001);
            Assert.AreEqual(1.97292, metricPoint.X, 0.001);
        }
    }
}
