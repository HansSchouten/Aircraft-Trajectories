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

        /// <summary>
        /// Converts the coordinates of a 3D Point into the chosen unit
        /// </summary>
        /// <param name="targetUnit"></param>
        /// <returns></returns>
        public Point3D ConvertTo(CoordinateUnit targetUnit)
        {
            CoordinateConversion converter = new CoordinateConversion();
            return converter.ConvertCoordinates(this, targetUnit);
        }

        public double DistanceTo(Point3D other)
        {
            return Math.Sqrt(Math.Pow(other.X - X, 2)  + Math.Pow(other.Y - Y, 2) + Math.Pow(other.Z - Z, 2));
        }

        /// <summary>
        /// Calculates the heading to the given Point3D, based on metric coordinates
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double HeadingTo(Point3D other)
        {
            var dX = (other.X - X);
            var dY = (other.Y - Y);
            return ((90 - (180 / Math.PI) * Math.Atan2(dY, dX)) + 360) % 360;
        }
    }
}
