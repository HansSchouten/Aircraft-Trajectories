using AircraftTrajectories.Models.Space3D;
using NUnit.Framework;
using System;

namespace AircraftTrajectories.NUnit.Tests.Models.Space3D
{
    [TestFixture]
    class GeographicToMetricTest
    {
        [Test]
        public void convertToXY()
        {
            var converter = new GeographicToMetric();
            var metricPoint = converter.ConvertToXY(122202, 487250);

            Assert.AreEqual("1118889.975", Math.Round(metricPoint.Y, 3).ToString());
            Assert.AreEqual("13603464413.919", Math.Round(metricPoint.X, 3).ToString());
        }
    }
}
