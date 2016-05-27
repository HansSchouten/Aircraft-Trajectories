using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AircraftTrajectories.Models.Space3D;
using System.Drawing;

namespace AircraftTrajectories.NUnit.Tests.Models.Space3D
{
    [TestFixture]
    class Point3DTest
    {

        [Test]
        public void PointTest()
        {
            Point3D test = new Point3D(15555, 14656, 4500, CoordinateUnit.metric);
            Assert.IsNotNull(test);
        }

        [Test]
        public void PointXTest()
        {
            Point3D test = new Point3D(15555, 14656, 4500, CoordinateUnit.metric);
            Assert.AreEqual(15555, test.X);
        }

        [Test]
        public void PointYTest()
        {
            Point3D test = new Point3D(15555, 14656, 4500, CoordinateUnit.geographic);
            Assert.AreEqual(14656, test.Y);
        }

        [Test]
        public void PointZTest()
        {
            Point3D test = new Point3D(15555, 14656, 4500, CoordinateUnit.imperial);
            Assert.AreEqual(4500, test.Z);
        }


        [Test]
        public void PointConvertToTest()
        {
            Point3D test = new Point3D(15555, 14656, 4500, CoordinateUnit.metric);
            Point3D converted = test.ConvertTo(CoordinateUnit.geographic);

            Assert.AreEqual(3.52, Math.Round(converted.X, 2));
        }


    }
}
