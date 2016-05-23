using System;

namespace AircraftTrajectories.Models.Space3D
{
    public class Point3D
    {
        public CoordinateUnit CoordinateUnits { get; protected set; }
        public double X { get; protected set; }
        public double Y { get; protected set; }
        public double Z { get; protected set; }

        public Point3D(double x, double y, double z, CoordinateUnit coordinateUnits)
        {
            X = x;
            Y = y;
            Z = z;
            CoordinateUnits = coordinateUnits;
        }
    }
}
