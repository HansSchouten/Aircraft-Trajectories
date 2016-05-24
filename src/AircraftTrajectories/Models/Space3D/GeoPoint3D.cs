using System;

namespace AircraftTrajectories.Models.Space3D
{
    public class GeoPoint3D
    {
        public CoordinateUnit CoordinateUnits { get; protected set; }
        public double Longitude { get; protected set; }
        public double Latitude { get; protected set; }
        public double Z { get; protected set; }

        public GeoPoint3D(double longitude, double latitude, double z)
        {
            Longitude = longitude;
            Latitude = latitude;
            Z = z;
            CoordinateUnits = CoordinateUnit.geographic;
        }
    }
}
