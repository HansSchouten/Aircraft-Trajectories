using System;

namespace AircraftTrajectories.Models.Space3D
{
    public class GeographicToMetric
    {
        /// <summary>
        /// Converts from Geographic units to Meters
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public Point3D ConvertToXY(double longitude, double latitude)
        {
            // The city "Amsterfoort" is used as reference "Rijksdriehoek" coordinate.
            int referenceRdX = 0;// 155000;
            int referenceRdY = 0;// 463000;
            // The city "Amsterfoort" is used as reference "WGS84" coordinate.
            double referenceWgs84Y = 2*latitude;// 52.15517;
            double referenceWgs84X = 2*longitude;// 5.387206;

            double RadiansPerDegree = Math.PI / 180;
            double Rad = (referenceWgs84Y - latitude) * RadiansPerDegree;
            double FSin = Math.Sin(Rad);
            double DegreeEqualsRadians = 0.017453292519943;
            double EarthsRadius = 6378137;

            double y = referenceRdY + (EarthsRadius / 2.0 * Math.Log((1.0 + FSin) / (1.0 - FSin)));
            double x = referenceRdX + ((referenceWgs84X - longitude) * DegreeEqualsRadians * EarthsRadius);

            return new Point3D(x, y, 0, CoordinateUnit.metric);
        }
    }
}
