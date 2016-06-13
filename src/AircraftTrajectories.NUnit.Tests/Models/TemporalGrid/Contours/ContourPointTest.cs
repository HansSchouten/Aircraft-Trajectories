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
        [Test()]
        public void ContourPoint()
        {
            ContourPoint point = new ContourPoint();
            point.Direction = ContourDirection.East;

            IEnumerable<ContourPoint>[][] hgrid = new IEnumerable<ContourPoint>[2][];
            IEnumerable<ContourPoint>[][] vgrid = new IEnumerable<ContourPoint>[2][];

           
        }

        [Test()]
        public void findNextTest()
        {
            ContourPoint point = new ContourPoint();
            point.Direction = ContourDirection.West;
        }

    }
}
