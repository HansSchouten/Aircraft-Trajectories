using AircraftTrajectories.Models.Space3D;
using NUnit.Framework;
using System;


namespace AircraftTrajectories.NUnit.Tests.Models.Space3D
{
    [TestFixture]
    class CoordinateConversionTest
    {
        [Test]
        public void ConvertCoordinatesTest()
        {
            
            Point3D input = new Point3D(122202, 487250, 122202, CoordinateUnit.metric);
            CoordinateConversion converter = new CoordinateConversion();
            Point3D result = converter.ConvertCoordinates(input, CoordinateUnit.geographic);

            Assert.AreEqual("52.372", Math.Round(result.Y, 3).ToString());
            Assert.AreEqual("4.906", Math.Round(result.X, 3).ToString());
            Assert.AreEqual("122202", Math.Round(result.Z, 3).ToString());

        }
    }
}
