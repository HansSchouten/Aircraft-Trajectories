using System;
using System.Collections.Generic;

namespace AircraftTrajectories.Models.TemporalGrid
{
    /// <summary>
    /// Class that represents grid values changing over time
    /// </summary>
    class TemporalGrid
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
        }

        /// <summary>
        /// Add a new grid to the collection of grids
        /// </summary>
        /// <param name="grid">the grid that will be added</param>
        public void addGrid(Grid grid)
        {
            _grids.Add(grid);
        }

    }
}
