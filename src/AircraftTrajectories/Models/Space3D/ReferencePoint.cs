namespace AircraftTrajectories.Models.Space3D
{
    public class ReferencePoint
    {
        public Point3D Point { get; protected set; }
        public GeoPoint3D GeoPoint { get; protected set; }

        public ReferencePoint(GeoPoint3D geoPoint, Point3D point = null)
        {
            GeoPoint = geoPoint;
            Point = (point == null) ? new Point3D(0, 0, 0, CoordinateUnit.metric) : point;
        }
    }
}