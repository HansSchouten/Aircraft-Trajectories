﻿using AircraftTrajectories.Models.TemporalGrid;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AircraftTrajectories.NUnit.Tests.TemporalGrid
{
    [TestFixture]
    public class GridTest
    {
        [Test]
        public void Grid()
        {
            /*
            string currentFolder = Globals.testdataDirectory + "noise.out";
            string rawTrackData = File.ReadAllText(currentFolder);
            double[][] trackData = rawTrackData
                .Split('\n')
                .Select(q =>
                    q.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(Convert.ToDouble)
                     .ToArray()
                )
                .ToArray();
            
            Grid grid = new Grid(trackData);

            Assert.AreEqual("", grid.Contours.GetEnumerator().MoveNext().ToString());
            Assert.AreEqual("", grid.Data.GetLength(1).ToString());
            */
        }

        [Test]
        public void AircraftModel()
        {
            
        }

    }
}