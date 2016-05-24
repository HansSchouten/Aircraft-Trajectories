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
        public void ConvertLatLong()
        {
            x = 122202;
            y = 487250;
            RDToGeographic converter = new RDToGeographic();
            PointF point = converter.convertToLatLong(x, y);
            Assert.AreEqual("4.905598", point.X.ToString());
            Assert.AreEqual("52.37214", point.Y.ToString());
            Assert.AreEqual("4.905598, 52.37214", string.Format("{0}, {1}",
    point.X.ToString(CultureInfo.InvariantCulture.NumberFormat),
    point.Y.ToString(CultureInfo.InvariantCulture.NumberFormat)));
        }

    }

}
