using System.Collections.Generic;
using System.Linq;

namespace AircraftTrajectories.Models.TemporalGrid
{
    using Contours;
    using Space3D;
    using System;
    using System.Drawing;

    /// <summary>
    /// Class that represents a two-dimensional dataset
    /// </summary>
    public class Grid
    {
        public IEnumerable<Contour> Contours { get; protected set; }
        public double[][] Data { get; set; }
        public Point3D LowerLeftCorner { get; set; }
        protected ReferencePoint _referencePoint;
        public ReferencePoint ReferencePoint
        {
            get
            {
                return _referencePoint;
            }
            set
            {
                _referencePoint = value;
                Converter = new MetricToGeographic(_referencePoint);
            }
        }
        public MetricToGeographic Converter { get; set; }
        public int CellSize { get; set; }

        /// <summary>
        /// Construct a Grid
        /// </summary>
        /// <param name="data"></param>
        public Grid(double[][] data, Point3D _lowerLeftCorner, bool calculateContours = true)
        {
            ReferencePoint = new ReferencePointRD();
            Converter = new MetricToGeographic(ReferencePoint);
            LowerLeftCorner = _lowerLeftCorner;
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
        protected Grid CreateEmptyGrid(int height, int width, Point3D lowerLeftCorner, int cellSize = 125)
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

            Grid newGrid = new Grid(data, lowerLeftCorner, false);
            newGrid.CellSize = cellSize;
            return newGrid;
        }

        /// <summary>
        /// Retuns GeoPoint based on the entered x y index of the grid
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <returns></returns>
        public GeoPoint3D GridCoordinate(double gridX, double gridY)
        {
            return Converter.ConvertToLongLat(LowerLeftCorner.X + (gridX * CellSize), LowerLeftCorner.Y + (gridY * CellSize));
        }

        /// <summary>
        /// Returns a x y index of the grid based on the entered corner coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Point CoordinateToGridIndex(double x, double y)
        {
            int gridX = (int)Math.Floor((decimal)(x - LowerLeftCorner.X) / CellSize);
            int gridY = (int)Math.Floor((decimal)(y - LowerLeftCorner.Y) / CellSize);
            return new Point(gridX, gridY);
        }
    }
}