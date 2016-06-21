using AircraftTrajectories.Models.Space3D;
using System;
using System.Collections.Generic;
using System.Drawing;

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
        /// A list of subsequent Grid objects
        /// </summary>
        protected List<Grid> _grids;


        /// <summary>
        /// Construct a TemporalGrid
        /// </summary>
        public TemporalGrid() 
        {
            _grids = new List<Grid>();

            Interval = 1;
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

        /// <summary>
        /// Returns the number of grids stored
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfGrids()
        {
            return _grids.Count;
        }
    }
}