﻿using System;

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

        public GeoPoint3D MoveInDirection(double distance, double heading)
        {
            var R = 6378137;
            var brng = heading * Math.PI / 180;
            var lat1 = Latitude * Math.PI / 180;
            var lon1 = Longitude * Math.PI / 180;

            var lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(distance / R) +
                 Math.Cos(lat1) * Math.Sin(distance / R) * Math.Cos(brng));
            var lon2 = lon1 + Math.Atan2(Math.Sin(brng) * Math.Sin(distance / R) * Math.Cos(lat1),
                         Math.Cos(distance / R) - Math.Sin(lat1) * Math.Sin(lat2));

            double newLatitude = lat2 / (Math.PI / 180);
            double newLongitude = lon2 / (Math.PI / 180);
            return new GeoPoint3D(newLongitude, newLatitude, Z);
        }

        public double HeadingTo(GeoPoint3D destination)
        {
            var dLon = (destination.Longitude - Longitude) * (Math.PI / 180);
            var dPhi = Math.Log(
                Math.Tan((destination.Latitude * (Math.PI / 180)) / 2 + Math.PI / 4) / Math.Tan((Latitude * (Math.PI / 180)) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);

            return ((Math.Atan2(dLon, dPhi) * 180 / Math.PI) + 180) % 360;
        }
    }
}
