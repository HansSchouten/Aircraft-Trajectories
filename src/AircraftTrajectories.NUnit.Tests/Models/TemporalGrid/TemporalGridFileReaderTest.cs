using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AircraftTrajectories.Models.TemporalGrid;

namespace AircraftTrajectories.NUnit.Tests.Models.TemporalGrid
{
    using AircraftTrajectories.Models.TemporalGrid;
    using global::NUnit.Framework;
    using System.Drawing;
    class TemporalGridFileReaderTest
    {
        [Test]
        public void TemporalGridReaderTest()
        {
            TemporalGrid temp = new TemporalGridFileReader().createTemporalGridFromFile("noise.out");
            Assert.IsNotNull(temp);
        }

        [Test]
        public void TemporalGridReaderCellSizeTest()
        {
            TemporalGrid temp = new TemporalGridFileReader().createTemporalGridFromFile("noise.out");
            Assert.AreEqual(125, temp.GetGrid(0).CellSize);
        }

        [Test]
        public void TemporalGridReaderNumberGridsTest()
        {
            TemporalGrid temp = new TemporalGridFileReader().createTemporalGridFromFile("noise.out");
            Assert.AreEqual(4, temp.GetNumberOfGrids());
        }

        [Test]
        public void TemporalGridReaderGridIndexTest()
        {
            TemporalGrid temp = new TemporalGridFileReader().createTemporalGridFromFile("noise.out");
            Point res = new Point();
            res.X = 53;
            res.Y = 21;

            Assert.AreEqual(res, temp.GetGrid(0).CoordinateToGridIndex(110658, 478103));
        }


    }
}