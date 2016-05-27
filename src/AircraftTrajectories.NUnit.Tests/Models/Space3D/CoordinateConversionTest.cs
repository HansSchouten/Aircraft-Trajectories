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
            var input = new Point3D(122202, 487250, 122202, CoordinateUnit.metric);
            CoordinateConversion converter = new CoordinateConversion();
            var result = converter.ConvertCoordinates(input, CoordinateUnit.geographic);

            Assert.AreEqual(52.372, result.Y, 0.001);
            Assert.AreEqual(4.906, result.X, 0.001);
            Assert.AreEqual(122202, result.Z, 0.001);
        }

        [Test]
        public void ConvertImperial()
        {
            var input = new Point3D(122202, 487250, 122202, CoordinateUnit.imperial);
            CoordinateConversion converter = new CoordinateConversion();
            var result = converter.ConvertCoordinates(input, CoordinateUnit.metric);

            Assert.AreEqual(148513.8, result.Y, 0.001);
            Assert.AreEqual(37247.17, result.X, 0.001);
            Assert.AreEqual(37247.17, result.Z, 0.001);
        }

        [Test]
        public void ConvertGeographic()
        {
            var input = new Point3D(4.7, 53.2, 1000, CoordinateUnit.geographic);
            CoordinateConversion converter = new CoordinateConversion();
            var result = converter.ConvertCoordinates(input, CoordinateUnit.metric);

            Assert.AreEqual(523201.6067, result.X, 0.001);
            Assert.AreEqual(7020078.532, result.Y, 0.001);
            Assert.AreEqual(1000, result.Z, 0.001);
        }

        [Test]
        public void ConvertMetricToImperial()
        {
            var input = new Point3D(122202, 487250, 122202, CoordinateUnit.metric);
            CoordinateConversion converter = new CoordinateConversion();
            var result = converter.ConvertCoordinates(input, CoordinateUnit.imperial);

            Assert.AreEqual(1598589.239, result.Y, 0.001);
            Assert.AreEqual(400925.197, result.X, 0.001);
            Assert.AreEqual(400925.197, result.Z, 0.001);
        }
    }

}
