using System.Collections.Generic;
using System.Linq;

namespace AircraftTrajectories.Models.Contours
{
    public class ContourPoint
    {
        public Contour Parent { get; set; }

        public Point Location { get; set; }

        public int Value { get; set; }

        public Coordinate Coordinate { get; set; }

        public ContourDirection Direction { get; set; }

        public ContourPoint FindNext(IEnumerable<ContourPoint>[][] vgrid, IEnumerable<ContourPoint>[][] hgrid)
        {
            IEnumerable<ContourPoint> candidates = null;
            switch (Direction)
            {
                case ContourDirection.East:
                    if (Coordinate.Y == vgrid[0].Length - 1)
                    {
                        return null;
                    }
                    candidates = (vgrid[Coordinate.X][Coordinate.Y + 1]).Where(x => x.Direction == ContourDirection.East);
                    if (Coordinate.Y < hgrid[Coordinate.X].Length)
                    {
                        if (Coordinate.X < hgrid.Length)
                        {
                            candidates = candidates
                                .Concat(
                                    (hgrid[Coordinate.X + 1][Coordinate.Y]).Where(
                                        x => x.Direction == ContourDirection.South));
                        }

                        candidates =
                            candidates.Concat(
                                (hgrid[Coordinate.X][Coordinate.Y].Where(x => x.Direction == ContourDirection.North)));
                    }
                    break;
                case ContourDirection.West:
                    if (Coordinate.Y == 0)
                    {
                        return null;
                    }
                    candidates = (vgrid[Coordinate.X][Coordinate.Y - 1]).Where(x => x.Direction == ContourDirection.West);
                    if (Coordinate.X < hgrid.Length)
                    {
                        candidates = candidates
                            .Concat((hgrid[Coordinate.X + 1][Coordinate.Y - 1]).Where(x => x.Direction == ContourDirection.South));
                    }
                    candidates =
                        candidates.Concat(
                            (hgrid[Coordinate.X][Coordinate.Y - 1].Where(x => x.Direction == ContourDirection.North)));
                    break;
                case ContourDirection.North:
                    if (Coordinate.X == 0)
                    {
                        return null;
                    }
                    candidates = hgrid[Coordinate.X - 1][Coordinate.Y].Where(x => x.Direction == ContourDirection.North);
                    if (Coordinate.X > 0)
                    {
                        candidates =
                            candidates.Concat(
                                (vgrid[Coordinate.X - 1][Coordinate.Y].Where(
                                    x => x.Direction == ContourDirection.West)));
                        if (Coordinate.Y < vgrid[Coordinate.X - 1].Length + 1)
                        {
                            candidates =
                                candidates.Concat((vgrid[Coordinate.X - 1][Coordinate.Y + 1]).Where(
                                    x => x.Direction == ContourDirection.East));

                        }
                        
                    }
                    break;
                case ContourDirection.South:
                    if (Coordinate.X == hgrid.Length - 1)
                    {
                        return null;
                    }
                    candidates = hgrid[Coordinate.X + 1][Coordinate.Y].Where(x => x.Direction == ContourDirection.South);
                    if (Coordinate.X < vgrid.Length)
                    {
                        candidates =
                            candidates.Concat((vgrid[Coordinate.X][Coordinate.Y + 1]).Where(
                                x => x.Direction == ContourDirection.East));

                        candidates =
                            candidates.Concat(
                                (vgrid[Coordinate.X][Coordinate.Y].Where(
                                    x => x.Direction == ContourDirection.West)));
                    }
                    break;
            }
            if (candidates == null)
            {
                return null;
            }
            return candidates
                .Where(x => x.Parent == null && x.Value == Value).FirstOrDefault();
        }
    }
}