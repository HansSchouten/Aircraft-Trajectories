using System;
using MathNet.Numerics.Interpolation;
using AircraftTrajectories.Models.Space3D;
using System.Device.Location;

namespace AircraftTrajectories.Models.Trajectory
{
    public class Trajectory
    {
        protected CubicSpline _xSpline;
        protected CubicSpline _ySpline;
        protected CubicSpline _zSpline;
        protected CubicSpline _longitudeSpline;
        protected CubicSpline _latitudeSpline;
        public int Duration { get; set; }

        /// <summary>
        /// Constructs a trajectory based on the coordinates of a given spline 
        /// </summary>
        /// <param name="xSpline"></param>
        /// <param name="ySpline"></param>
        /// <param name="zSpline"></param>
        /// <param name="longitudeSpline"></param>
        /// <param name="latitudeSpline"></param>
        public Trajectory(CubicSpline xSpline, CubicSpline ySpline, CubicSpline zSpline, CubicSpline longitudeSpline, CubicSpline latitudeSpline)
        {
            _xSpline = xSpline;
            _ySpline = ySpline;
            _zSpline = zSpline;
            _longitudeSpline = longitudeSpline;
            _latitudeSpline = latitudeSpline;
        }

        /// <summary>
        /// Returns the x coordinate of a given spline
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public double X(double t)
        {
            return _xSpline.Interpolate(t);
        }

        /// <summary>
        /// Returns the y coordinate of a given spline
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public double Y(double t)
        {
            return _ySpline.Interpolate(t);
        }

        /// <summary>
        /// Returns the z coordinate of a given spline
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public double Z(double t)
        {
            return _zSpline.Interpolate(t);
        }

        /// <summary>
        /// Returns the longitude value of a given spline
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public double Longitude(double t)
        {
            return _longitudeSpline.Interpolate(t);
        }

        /// <summary>
        /// Returns the latitude value of a given spline
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public double Latitude(double t)
        {
            return _latitudeSpline.Interpolate(t);
        }

        /// <summary>
        /// Returns a 3D GeoPoint with the spline coordinates for timestep t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public GeoPoint3D GeoPoint(double t)
        {
            return new GeoPoint3D(Longitude(t), Latitude(t), Z(t));
        }

        /// <summary>
        /// Returns a 3D Point with the spline coordinates for timestep t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point3D Point3D(double t)
        {
            return new Point3D(X(t), Y(t), Z(t), CoordinateUnit.metric);
        }

        /// <summary>
        /// Returns the airspeed
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public double Airspeed(double t)
        {
            return (200 * 0.514);
        }

        /// <summary>
        /// Calculates the heading on a given timestep t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public double Heading(double t)
        {
            GeoPoint3D point1 = GeoPoint(t);
            GeoPoint3D point2 = GeoPoint(t + 1);

            return point1.HeadingTo(point2);
        }

        /// <summary>
        /// Calculates the tilt on a given timestep t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public double Tilt(double t)
        {
            return Math.Atan((Z(t + 1) - Z(t)) / 103) * (180 / Math.PI);
        }

        /// <summary>
        /// Calculates the bankangle on a given timestep t
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public double BankAngle(int t)
        {
            if(t < 1)
            {
                return 0;
            }

            //source: http://www.regentsprep.org/regents/math/geometry/gcg6/RCir.htm
            GeoPoint3D point1 = GeoPoint(t - 1);
            GeoPoint3D point2 = GeoPoint(t);
            GeoPoint3D point3 = GeoPoint(t + 1);

            double m_r = (point2.Longitude - point1.Longitude) / (point2.Latitude - point1.Latitude);
            double m_t = (point3.Longitude - point2.Longitude) / (point3.Latitude - point2.Latitude);
            double x_c = (m_r * m_t * (point3.Longitude - point1.Longitude) + m_r * (point2.Latitude + point3.Latitude) - m_t * (point1.Latitude + point2.Latitude)) / (2 * (m_r - m_t));
            double y_c = -(1 / m_r) * (x_c - ((point1.Latitude + point2.Latitude) / 2)) + ((point1.Longitude + point2.Longitude) / 2);

            if (double.IsInfinity(x_c))
            {
                return 0;
            }

            GeoCoordinate c1 = new GeoCoordinate(point1.Latitude, point1.Longitude);
            GeoCoordinate centroid = new GeoCoordinate(x_c, y_c);
            double radius = c1.GetDistanceTo(centroid);

            double TAS = Airspeed(t);
            double g = 9.81;
            return Math.Atan(((TAS * TAS) / radius) / g) * (180 / Math.PI);
        }
    }
}