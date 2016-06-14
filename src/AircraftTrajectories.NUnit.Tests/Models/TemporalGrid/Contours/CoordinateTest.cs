using AircraftTrajectories.Models.Contours;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftTrajectories.Tests.TemporalGrid.Contours
{
    [TestFixture]
    class CoordinateTest
    {
        [Test]
        public void Coordinate()
        {
            Coordinate temp = new Coordinate(2, 6);
            Assert.IsNotNull(temp);

            Assert.AreEqual(2, temp.X);
            Assert.AreEqual(6, temp.Y);
        }
    }
}
