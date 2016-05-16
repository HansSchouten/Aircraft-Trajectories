using System;
using MathNet.Numerics.Interpolation;
using AircraftTrajectories.Models.Space3D;

namespace AircraftTrajectories.Models.Trajectory
{
    class Trajectory
    {
        protected CubicSpline _xSpline;
        protected CubicSpline _ySpline;
        protected CubicSpline _zSpline;
        protected CubicSpline _longitudeSpline;
        protected CubicSpline _latitudeSpline;
        public double Duration { get; set; }


        public Trajectory(CubicSpline xSpline, CubicSpline ySpline, CubicSpline zSpline, CubicSpline longitudeSpline, CubicSpline latitudeSpline)
        {
            _xSpline = xSpline;
            _ySpline = ySpline;
            _zSpline = zSpline;
            _longitudeSpline = longitudeSpline;
            _latitudeSpline = latitudeSpline;
        }

        public double X(double t)
        {
            return _xSpline.Interpolate(t);
        }

        public double Y(double t)
        {
            return _ySpline.Interpolate(t);
        }

        public double Z(double t)
        {
            return _zSpline.Interpolate(t);
        }

        public double Longitude(double t)
        {
            return _longitudeSpline.Interpolate(t);
        }

        public double Latitude(double t)
        {
            return _latitudeSpline.Interpolate(t);
        }

        public GeoPoint3D GeoPoint(double t)
        {
            return new GeoPoint3D(Longitude(t), Latitude(t), Z(t));
        }

        public Point3D Point3D(double t)
        {
            return new Point3D(X(t), Y(t), Z(t), CoordinateUnit.metric);
        }

    }
}