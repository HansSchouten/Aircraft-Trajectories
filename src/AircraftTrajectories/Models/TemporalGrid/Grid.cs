using System.Collections.Generic;
using System.Linq;

namespace AircraftTrajectories.Models.TemporalGrid
{
    using Contours;
    using Space3D;

    /// <summary>
    /// Class that represents a two-dimensional dataset
    /// </summary>
    public class Grid
    {
        public IEnumerable<Contour> Contours { get; protected set; }
        public double[][] Data { get; set; }
        /// <summary>
        /// The coordinates of the lower left corner of the grid
        /// </summary>
        public Point3D LowerLeftCorner { get; set; }
        public int CellSize { get; set; }

        /// <summary>
        /// Construct a Grid
        /// </summary>
        /// <param name="data"></param>
        public Grid(double[][] data, bool calculateContours = true)
        {
            LowerLeftCorner = new Point3D(0, 0, 0, CoordinateUnit.metric);
            CellSize = 125;
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

        /// <summary>
        /// Create a grid of the specified size filled with zeros
        /// </summary>
        /// <param name="height">the number of vertical cells</param>
        /// <param name="width">the number of horizontal cells</param>
        /// <returns></returns>
        protected Grid CreateEmptyGrid(int height, int width, Point3D lowerLeftCorner = null, int cellSize = 125)
        {
            double[][] data = new double[width][];
            for (int x=0; x<width; x++)
            {
                double[] column = new double[height];
                for (int y=0; y<height; y++)
                {
                    column[y] = 0;
                }
                data[x] = column;
            }

            Grid newGrid = new Grid(data, false);
            if (lowerLeftCorner != null)
            {
                newGrid.LowerLeftCorner = lowerLeftCorner;
                newGrid.CellSize = cellSize;
            }
            return newGrid;
        }
    }
}
