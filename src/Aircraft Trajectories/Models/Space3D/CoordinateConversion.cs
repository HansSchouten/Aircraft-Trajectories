using System;

namespace AircraftTrajectories.Models.Space3D
{
    enum CoordinateUnit { imperial, metric, geographic };

    class CoordinateConversion
    {
        public static double FeetToMeters { get { return 0.3048; } }

        public Point3D ConvertCoordinates(Point3D point, CoordinateUnit newUnits)
        {
            Point3D metricPoint = ConvertToMetrics(point);
            return ConvertMetricsTo(metricPoint, newUnits);
        }

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
                    var converter = new GeographicToMetric();
                    var metricPoint = converter.ConvertToXY(point.X, point.Y);
                    newX = metricPoint.X;
                    newY = metricPoint.Y;
                    Console.WriteLine(newX + " " + newY);
                    break;
            }

            return new Point3D(newX, newY, newZ, CoordinateUnit.metric);
        }

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
                    var converter = new MetricToGeographic();
                    var geoPoint = converter.ConvertToLongLat(point.X, point.Y);
                    newX = geoPoint.X;
                    newY = geoPoint.Y;
                    break;
            }

            return new Point3D(newX, newY, newZ, newUnits);
        }

    }

}
