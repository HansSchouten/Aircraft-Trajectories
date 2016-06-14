using AircraftTrajectories.Models.Contours;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftTrajectories.Models.Contours.Tests
{
    class ContourPointTest
    {
        [Test]
        public void ContourPoint()
        {
            ContourPoint point = new ContourPoint();
            point.Direction = ContourDirection.East;

            IEnumerable<ContourPoint>[][] hgrid = new IEnumerable<ContourPoint>[2][];
            IEnumerable<ContourPoint>[][] vgrid = new IEnumerable<ContourPoint>[2][];

            Assert.AreEqual(ContourDirection.East, point.Direction);
         }

        [Test]
        public void findNextContourPointTest()
        {
            ContourPoint point = new ContourPoint();
            point.Direction = ContourDirection.West;
        }

        [Test]
        public void findParentContourPointTest()
        {
            ContourPoint point = new ContourPoint();
            point.Direction = ContourDirection.West;
            
            Assert.IsNull(point.Parent);
        }

        [Test]
        public void findValueContourPointTest()
        {
            ContourPoint point = new ContourPoint();
            point.Direction = ContourDirection.West;

            Assert.AreEqual(0, point.Value);
        }

    }
}
