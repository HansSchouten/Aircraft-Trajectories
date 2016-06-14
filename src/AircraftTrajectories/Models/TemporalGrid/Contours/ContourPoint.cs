using AircraftTrajectories.Models.TemporalGrid;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AircraftTrajectories.Models.Contours
{
    public class ContourPoint
    {
        /// <summary>
        /// The contour this point is part of
        /// </summary>
        public Contour Parent { get; set; }
        /// <summary>
        /// The location that this contour point occupies
        /// </summary>
        public Point Location { get; set; }
        /// <summary>
        /// The value as meassured at the location of this contourpoint
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// The coordinate of this contour point
        /// </summary>
        public Coordinate Coordinate { get; set; }
        /// <summary>
        /// The direction this contourpoint points to
        /// </summary>
        public ContourDirection Direction { get; set; }

        /// <summary>
        /// Finds next contourpoint on a grid that is a candidate to the current point
        /// </summary>
        /// <param name="vgrid"></param>
        /// <param name="hgrid"></param>
        /// <returns></returns>
        public ContourPoint FindNext(IEnumerable<ContourPoint>[][] vgrid, IEnumerable<ContourPoint>[][] hgrid)
        {
            IEnumerable<ContourPoint> candidates = null;
            switch (Direction)
            {
                case ContourDirection.East:
                    if (Coordinate.Y != vgrid[0].Length - 1) {
                        setEast(hgrid, vgrid, candidates, Coordinate.X, Coordinate.Y);
                    }
                    break;
                case ContourDirection.West:
                    if (Coordinate.Y != 0) {
                        setWest(hgrid, vgrid, candidates, Coordinate.X, Coordinate.Y);
                    }
                    break;
                case ContourDirection.North:
                    if (Coordinate.X != 0) {
                        setNorth(hgrid, vgrid, candidates, Coordinate.X, Coordinate.Y);
                    }
                    break;
                case ContourDirection.South:
                    if (Coordinate.X != hgrid.Length - 1) {
                        setSouth(hgrid, vgrid, candidates, Coordinate.X, Coordinate.Y);
                    }
                    break;
            }
            return (candidates == null) ? null : candidates
                .Where(x => x.Parent == null && x.Value == Value).FirstOrDefault();
        }

        /// <summary>
        /// Set each step in the contours' candidates to the required direction
        /// </summary>
        /// <param name="cand"></param>
        /// <param name="grid"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        protected IEnumerable<ContourPoint> setDirection(IEnumerable<ContourPoint>[][] grid, int X, int Y, ContourDirection dir)
        {
            IEnumerable<ContourPoint> candidates = (grid[X][Y]).Where(x => x.Direction == dir);

            return candidates;
        }

        /// <summary>
        /// Concatenate the contour's candidates with specified direction
        /// </summary>
        /// <param name="cand"></param>
        /// <param name="grid"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        protected IEnumerable<ContourPoint> concatenateDirection(IEnumerable<ContourPoint> cand, IEnumerable<ContourPoint>[][] grid, int X, int Y, ContourDirection dir)
        {
            IEnumerable<ContourPoint> candidates = cand.Concat(
                (grid[X][Y].Where(x => x.Direction == dir)));

            return candidates;
        }

        /// <summary>
        /// Set the direction of each point in the contour's candidates to East
        /// </summary>
        /// <param name="hgrid"></param>
        /// <param name="vgrid"></param>
        /// <param name="candidates"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        protected void setEast(IEnumerable<ContourPoint>[][] hgrid, IEnumerable<ContourPoint>[][] vgrid, IEnumerable<ContourPoint> candidates, int X, int Y)
        {
            candidates = setDirection(vgrid, Coordinate.X, Coordinate.Y + 1, ContourDirection.East);

            if (Y < hgrid[X].Length)
            {
                if (X < hgrid.Length)
                {
                    candidates = concatenateDirection(candidates, hgrid, X + 1, Y, ContourDirection.South);
                }

                candidates = concatenateDirection(candidates, hgrid, X, Y, ContourDirection.North);
            }
        }

        /// <summary>
        /// Set the direction of each point in the contour's candidates to West
        /// </summary>
        /// <param name="hgrid"></param>
        /// <param name="vgrid"></param>
        /// <param name="candidates"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        protected void setWest(IEnumerable<ContourPoint>[][] hgrid, IEnumerable<ContourPoint>[][] vgrid, IEnumerable<ContourPoint> candidates, int X, int Y)
        {
            candidates = setDirection(vgrid, Coordinate.X, Coordinate.Y - 1, ContourDirection.West);

            if (X < hgrid.Length)
            {
                candidates = concatenateDirection(candidates, hgrid, X + 1, Y - 1, ContourDirection.South);
            }
            candidates = concatenateDirection(candidates, hgrid, X, Y - 1, ContourDirection.North);
        }

        /// <summary>
        /// Set the direction of each point in the contour's candidates to North
        /// </summary>
        /// <param name="hgrid"></param>
        /// <param name="vgrid"></param>
        /// <param name="candidates"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        protected void setNorth(IEnumerable<ContourPoint>[][] hgrid, IEnumerable<ContourPoint>[][] vgrid, IEnumerable<ContourPoint> candidates, int X, int Y)
        {
            candidates = setDirection(hgrid, Coordinate.X - 1, Coordinate.Y, ContourDirection.North);

            if (X > 0)
            {
                candidates = concatenateDirection(candidates, vgrid, X - 1, Y, ContourDirection.West);

                if (Y < vgrid[X - 1].Length + 1)
                {
                    candidates = concatenateDirection(candidates, vgrid, X - 1, Y + 1, ContourDirection.East);
                }
            }
        }

        /// <summary>
        /// Set the direction of each point in the contour's candidates to South
        /// </summary>
        /// <param name="hgrid"></param>
        /// <param name="vgrid"></param>
        /// <param name="candidates"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        protected void setSouth(IEnumerable<ContourPoint>[][] hgrid, IEnumerable<ContourPoint>[][] vgrid, IEnumerable<ContourPoint> candidates, int X, int Y)
        {
            candidates = setDirection(hgrid, Coordinate.X + 1, Coordinate.Y, ContourDirection.South);

            if (X < vgrid.Length)
            {
                candidates = concatenateDirection(candidates, vgrid, X, Y + 1, ContourDirection.East);
                candidates = concatenateDirection(candidates, vgrid, X, Y, ContourDirection.West);
            }
        }
    }
}