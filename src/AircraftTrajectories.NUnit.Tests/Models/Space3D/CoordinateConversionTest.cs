using AircraftTrajectories.Models.Space3D;
using NUnit.Framework;
using System;


namespace AircraftTrajectories.NUnit.Tests.Models.Space3D
{
    [TestFixture]
    class CoordinateConversionTest
    {
        [Test]
        public void ConvertMetricToGeographic()
        {
            
            Point3D input = new Point3D(122202, 487250, 122202, CoordinateUnit.metric);
            CoordinateConversion converter = new CoordinateConversion();
            Point3D result = converter.ConvertCoordinates(input, CoordinateUnit.geographic);
            Assert.AreEqual("52.372", Math.Round(result.Y, 3).ToString());
            Assert.AreEqual("4.906", Math.Round(result.X, 3).ToString());
            Assert.AreEqual("122202", Math.Round(result.Z, 3).ToString());

        }

        [Test]
        public void ConvertImperial()
        {

            Point3D input = new Point3D(122202, 487250, 122202, CoordinateUnit.imperial);
            CoordinateConversion converter = new CoordinateConversion();
            Point3D result = converter.ConvertCoordinates(input, CoordinateUnit.metric);
            Assert.AreEqual("148513.8", Math.Round(result.Y, 3).ToString());
            Assert.AreEqual("37247.17", Math.Round(result.X, 3).ToString());
            Assert.AreEqual("37247.17", Math.Round(result.Z, 3).ToString());

        }

        [Test]
        public void ConvertGeographic()
        {

            Point3D input = new Point3D(122202, 487250, 122202, CoordinateUnit.geographic);
            CoordinateConversion converter = new CoordinateConversion();
            Point3D result = converter.ConvertCoordinates(input, CoordinateUnit.metric);
            Assert.AreEqual("1118889.975", Math.Round(result.Y, 3).ToString());
            Assert.AreEqual("13603464413.919", Math.Round(result.X, 3).ToString());
            Assert.AreEqual("122202", Math.Round(result.Z, 3).ToString());

        }

        [Test]
        public void ConvertMetricToImperial()
        {

            Point3D input = new Point3D(122202, 487250, 122202, CoordinateUnit.metric);
            CoordinateConversion converter = new CoordinateConversion();
            Point3D result = converter.ConvertCoordinates(input, CoordinateUnit.imperial);
            Assert.AreEqual("1598589.239", Math.Round(result.Y, 3).ToString());
            Assert.AreEqual("400925.197", Math.Round(result.X, 3).ToString());
            Assert.AreEqual("400925.197", Math.Round(result.Z, 3).ToString());

        }
    }

}
