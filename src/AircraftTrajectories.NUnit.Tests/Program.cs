using System;
using NUnit.Framework;
using AircraftTrajectories.Models.Space3D;

namespace AircraftTrajectories.NUnit.Tests
{
    class Program
    {
        [TestFixture]
        public class WorldTests
        {
            [Test]
            public void convertToLatLong()
            {
                var converter = new MetricToGeographic();
                var metricPoint = converter.ConvertToLongLat(122202, 487250);

                Assert.AreEqual("52.372", Math.Round(metricPoint.Y, 3).ToString());
                Assert.AreEqual("4.906", Math.Round(metricPoint.X, 3).ToString());
            }
        }
    }
}
