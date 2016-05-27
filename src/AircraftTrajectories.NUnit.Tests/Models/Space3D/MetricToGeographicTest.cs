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
    }
}
