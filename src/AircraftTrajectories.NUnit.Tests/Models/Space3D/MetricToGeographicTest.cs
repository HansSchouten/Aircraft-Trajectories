using AircraftTrajectories.Models.Space3D;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Space3D
{
    [TestFixture]
    class MetricToGeographicTest
    {
        [Test]
        public void convertToLatLong()
        {
            var converter = new MetricToGeographic(new ReferencePointRD());
            var metricPoint = converter.ConvertToLongLat(122202, 487250);

            Assert.AreEqual(52.372, metricPoint.Longitude, 0.001);
            Assert.AreEqual(4.906, metricPoint.Latitude, 0.001);
        }
    }
}
