
namespace AircraftTrajectories.Models.Space3D
{
    public enum CoordinateUnit { imperial, metric, geographic };

    public class CoordinateConversion
    {
        /// <summary>
        /// Conversion constant from feet to meters
        /// </summary>
        public static double FeetToMeters { get { return 0.3048; } }

        /// <summary>
        /// Converts the coordinates of a 3D point into requred unit
        /// </summary>
        /// <param name="point"></param>
        /// <param name="newUnits"></param>
        /// <returns></returns>
        public Point3D ConvertCoordinates(Point3D point, CoordinateUnit newUnits)
        {
            Point3D metricPoint = ConvertToMetrics(point);
            return ConvertMetricsTo(metricPoint, newUnits);
        }

        /// <summary>
        /// Converts the corodinates of a 3D point in meters
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected Point3D ConvertToMetrics(Point3D point)
        {
            double newX = point.X;
            double newY = point.Y;
            double newZ = point.Z;

            switch (point.CoordinateUnits)
            {
                case CoordinateUnit.imperial:
                    newX = point.X * FeetToMeters;
                    newY = point.Y * FeetToMeters;
                    newZ = point.Z * FeetToMeters;
                    break;

                case CoordinateUnit.metric:
                    break;

                case CoordinateUnit.geographic:
                    var converter = new GeographicToMetric(new ReferencePointRD());
                    var metricPoint = converter.ConvertToXY(point.X, point.Y);
                    newX = metricPoint.X;
                    newY = metricPoint.Y;
                    break;
            }

            return new Point3D(newX, newY, newZ, CoordinateUnit.metric);
        }

        /// <summary>
        /// Converts the coordinates of a 3D point from meters to required unit
        /// </summary>
        /// <param name="point"></param>
        /// <param name="newUnits"></param>
        /// <returns></returns>
        protected Point3D ConvertMetricsTo(Point3D point, CoordinateUnit newUnits)
        {
            double newX = point.X;
            double newY = point.Y;
            double newZ = point.Z;

            switch (newUnits)
            {
                case CoordinateUnit.imperial:
                    newX = point.X / FeetToMeters;
                    newY = point.Y / FeetToMeters;
                    newZ = point.Z / FeetToMeters;
                    break;

                case CoordinateUnit.metric:
                    break;

                case CoordinateUnit.geographic:
                    var converter = new MetricToGeographic(new ReferencePointRD());
                    var geoPoint = converter.ConvertToLongLat(point.X, point.Y);
                    newX = geoPoint.Longitude;
                    newY = geoPoint.Latitude;
                    break;
            }

            return new Point3D(newX, newY, newZ, newUnits);
        }

    }

}
