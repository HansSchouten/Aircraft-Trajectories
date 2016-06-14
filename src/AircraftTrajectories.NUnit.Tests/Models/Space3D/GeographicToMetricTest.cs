using AircraftTrajectories.Models.Space3D;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Space3D
{
    [TestFixture]
    class GeographicToMetricTest
    {
        [Test]
        public void convertXoXY()
        {
            var converter = new GeographicToMetric();
            var metricPoint = converter.ConvertToXY(4.7, 53);

            Assert.AreEqual(6982997.9204, metricPoint.Y, 0.001);
        }

        [Test]
        public void convertYToXY()
        {
            var converter = new GeographicToMetric();
            var metricPoint = converter.ConvertToXY(4.7, 53);

            Assert.AreEqual(523201.6067, metricPoint.X, 0.001);
        }

        [Test]
        public void convertZeroToXY()
        {
            var converter = new GeographicToMetric();
            var metricPoint = converter.ConvertToXY(0, 0);

            Assert.AreEqual(0, metricPoint.X, 0.001);
            Assert.AreEqual(0, metricPoint.Y, 0.001);
        }
    }
}
