
namespace AircraftTrajectories.Models.Space3D
{
    public class ReferencePointRD : ReferencePoint
    {

        public ReferencePointRD() : base(
            new GeoPoint3D(5.387206, 52.15517, 0),
            new Point3D(155000, 463000, 0, CoordinateUnit.metric)
        ) { }

    }
}