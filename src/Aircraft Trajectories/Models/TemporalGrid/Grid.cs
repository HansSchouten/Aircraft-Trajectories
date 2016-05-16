using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftTrajectories.Models.TemporalGrid
{
    using Contours;

    /// <summary>
    /// Class that represents a two-dimensional dataset
    /// </summary>
    class Grid
    {
        public IEnumerable<Contour> Contours { get; protected set; }
        protected double[][] _data;


        /// <summary>
        /// Construct a Grid
        /// </summary>
        /// <param name="data"></param>
        public Grid(double[][] data)
        {
            _data = data;
            CalculateContours();
        }

        protected void CalculateContours()
        {
            IEnumerable<ContourPoint>[][] hgrid, vgrid;
            Contours = Contour.CreateContours(_data, out hgrid, out vgrid).ToArray();
        }
    }
}
