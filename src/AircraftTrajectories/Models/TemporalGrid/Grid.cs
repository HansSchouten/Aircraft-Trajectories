using System.Collections.Generic;
using System.Linq;

namespace AircraftTrajectories.Models.TemporalGrid
{
    using Contours;

    /// <summary>
    /// Class that represents a two-dimensional dataset
    /// </summary>
    public class Grid
    {
        public IEnumerable<Contour> Contours { get; protected set; }
        public double[][] Data { get; set; }

        /// <summary>
        /// Construct a Grid
        /// </summary>
        /// <param name="data"></param>
        public Grid(double[][] data, bool calculateContours = true)
        {
            Data = data;
            if (calculateContours)
            {
                CalculateContours();
            }
        }

        /// <summary>
        /// Calculates the contours within a vertical and horizontal grid
        /// </summary>
        protected void CalculateContours()
        {
            IEnumerable<ContourPoint>[][] hgrid, vgrid;
            Contours = Contour.CreateContours(Data, out hgrid, out vgrid).ToArray();
        }
    }
}
