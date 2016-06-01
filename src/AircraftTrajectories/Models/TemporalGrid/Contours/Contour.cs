using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AircraftTrajectories.Models.Contours
{
    public class Contour
    {
        public Contour(ContourPoint first, IEnumerable<ContourPoint>[][] vgrid, IEnumerable<ContourPoint>[][] hgrid, bool isClosed)
        {
            Value = first.Value;
            IsClosed = isClosed;
            Points = GetPoints(first, vgrid, hgrid).ToArray();
        }

        /// <summary>
        /// Returns the contour points on a grid starting in a given contour point
        /// </summary>
        /// <param name="first"></param>
        /// <param name="vgrid"></param>
        /// <param name="hgrid"></param>
        /// <returns></returns>
        private IEnumerable<ContourPoint> GetPoints(ContourPoint first, IEnumerable<ContourPoint>[][] vgrid, IEnumerable<ContourPoint>[][] hgrid)
        {
            first.Parent = this;
            yield return first;
            ContourPoint next, current = first;
            while ((next = current.FindNext(vgrid, hgrid)) != null && next != first)
            {
                next.Parent = this;
                yield return next;
                current = next;
            }
        }

        public ContourPoint[] Points { get; private set; }

        public int Value { get; private set; }

        public bool IsClosed { get; private set; }

        /// <summary>
        /// Creates contours based on noise data and given grid
        /// </summary>
        /// <param name="data"></param>
        /// <param name="hgrid"></param>
        /// <param name="vgrid"></param>
        /// <returns></returns>
        public static IEnumerable<Contour> CreateContours(double[][] data, out IEnumerable<ContourPoint>[][] hgrid, out IEnumerable<ContourPoint>[][] vgrid)
        {
            if (data == null)
            {
                hgrid = vgrid = null;
                return null;
            }
            hgrid = new IEnumerable<ContourPoint>[data.Length][];
            vgrid = new IEnumerable<ContourPoint>[data.Length - 1][];
            for (int x = 0; x < data.Length; x++)
            {
                if (x != data.Length - 1)
                {
                    vgrid[x] = new IEnumerable<ContourPoint>[data[x].Length];
                }

                hgrid[x] = new IEnumerable<ContourPoint>[data[x].Length - 1];
                for (int y = 0; y < data[x].Length; y++)
                {
                    var value = data[x][y];
                    if (x != 0)
                    {
                        vgrid[x - 1][y] = Enumerable.Range(Math.Min((int)value, (int)data[x - 1][y]) + 1, Math.Abs((int)data[x - 1][y] - (int)value))
                            .Select(v => new ContourPoint
                            {
                                Coordinate = new Coordinate(x - 1, y),
                                Location = new
                                    Point((x - (v - value) / (data[x - 1][y] - value)), y),
                                Direction = (value > data[x - 1][y]) ? ContourDirection.East : ContourDirection.West,
                                Value = v
                            }).ToList();
                    }
                    if (y < data[x].Length - 1)
                    {
                        hgrid[x][y] = Enumerable.Range(Math.Min((int)value, (int)data[x][y + 1]) + 1, Math.Abs((int)data[x][y + 1] - (int)value))
                            .Select(v => new ContourPoint
                            {
                                Coordinate = new Coordinate(x, y),
                                Location = new Point(x, (y + (v - value) / (data[x][y + 1] - value))),
                                Direction = (value > data[x][y + 1]) ? ContourDirection.South : ContourDirection.North,
                                Value = v
                            }).ToList();
                    }
                }
            }
            return GenerateContours(vgrid, hgrid);
        }

        /// <summary>
        /// Generates the contours by finding closed circles
        /// </summary>
        /// <param name="vgrid"></param>
        /// <param name="hgrid"></param>
        /// <returns></returns>
        private static IEnumerable<Contour> GenerateContours(IEnumerable<ContourPoint>[][] vgrid, IEnumerable<ContourPoint>[][] hgrid)
        {
            // iterate the frame
            foreach (var l in vgrid)
            {
                foreach (var i in l[0].Where(i => i.Direction == ContourDirection.East))
                {
                    yield return new Contour(i, vgrid, hgrid, false);
                }
                foreach (var i in l.Last().Where(i => i.Direction == ContourDirection.West))
                {
                    yield return new Contour(i, vgrid, hgrid, false);
                }
            }
            foreach (var i in hgrid[0].SelectMany(l => l.Where(i => i.Direction == ContourDirection.South)))
            {
                yield return new Contour(i, vgrid, hgrid, false);
            }
            foreach (var i in hgrid.Last().SelectMany(l => l.Where(i => i.Direction == ContourDirection.North)))
            {
                yield return new Contour(i, vgrid, hgrid, false);
            }

            // find circles
            for (int y = 1; y < vgrid.Length - 1; y++)
            {
                for (int x = 1; x < vgrid[y].Length - 1; x++)
                {
                    foreach (var i in vgrid[y][x].Where(i => i.Parent == null))
                    {
                        yield return new Contour(i, vgrid, hgrid, true);
                    }
                }
            }
        }

    }
}