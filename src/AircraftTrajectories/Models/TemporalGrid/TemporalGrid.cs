using AircraftTrajectories.Models.Space3D;
using System;
using System.Collections.Generic;

namespace AircraftTrajectories.Models.TemporalGrid
{
    /// <summary>
    /// Class that represents grid values changing over time
    /// </summary>
    public class TemporalGrid
    {
        /// <summary>
        /// The time in seconds between two subsequent grids
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// The coordinates of the lower left corner of the temporal grid
        /// </summary>
        public Point3D LowerLeftCorner { get; set; }
        /// <summary>
        /// The distance between 2 subsequent cells of the grid
        /// </summary>
        public int GridSize { get; set; }

        /// <summary>
        /// A list of subsequent Grid objects
        /// </summary>
        protected List<Grid> _grids;


        /// <summary>
        /// Construct a TemporalGrid
        /// </summary>
        public TemporalGrid() 
        {
            _grids = new List<Grid>();

            LowerLeftCorner = new Point3D(0, 0, 0, CoordinateUnit.metric);
            GridSize = 125;
        }

        /// <summary>
        /// Add a new grid to the collection of grids
        /// </summary>
        /// <param name="grid">the grid that will be added</param>
        public void AddGrid(Grid grid)
        {
            _grids.Add(grid);
        }

        /// <summary>
        /// Return the grid of the given moment in time
        /// </summary>
        /// <param name="t">the moment in time of which the grid is requested</param>
        /// <returns>the grid at the specified moment</returns>
        public Grid GetGrid(int t)
        {
            return _grids[t];
        }

        public int GetNumberOfGrids()
        {
            return _grids.Count;
        }

        public GeoPoint3D GridCoordinate(double xGridIndex, double yGridIndex)
        {
            var metricPoint = new Point3D(LowerLeftCorner.X + (xGridIndex * GridSize), LowerLeftCorner.Y + (yGridIndex * GridSize), 0, LowerLeftCorner.CoordinateUnits);
            var geoPoint = metricPoint.ConvertTo(CoordinateUnit.geographic);
            return new GeoPoint3D(geoPoint.X, geoPoint.Y, geoPoint.Z);
        }

        private double getNoiseValue(int x, int y, Grid grid)
        {
            x -= 104062;
            y -= 475470;
            int stepX = 125;
            int stepY = 125;
            int gridX = (int)Math.Floor((decimal)x / stepX);
            int gridY = (int)Math.Floor((decimal)y / stepY);
            return grid.Data[gridX][gridY];
        }
    }
}