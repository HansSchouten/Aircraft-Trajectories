using System;

namespace AircraftTrajectories.Models.Space3D
{
    class GeographicToMetric
    {
        public Point3D ConvertToXY(double longitude, double latitude)
        {
            double RadiansPerDegree = Math.PI / 180;
            double Rad = latitude * RadiansPerDegree;
            double FSin = Math.Sin(Rad);
            double DegreeEqualsRadians = 0.017453292519943;
            double EarthsRadius = 6378137;

            double y = EarthsRadius / 2.0 * Math.Log((1.0 + FSin) / (1.0 - FSin));
            double x = longitude * DegreeEqualsRadians * EarthsRadius;

            return new Point3D(x, y, 0, CoordinateUnit.metric);
        }
    }
}
