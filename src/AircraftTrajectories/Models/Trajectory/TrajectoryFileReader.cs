using AircraftTrajectories.Models.Space3D;
using MathNet.Numerics.Interpolation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AircraftTrajectories.Models.Trajectory
{
    /// <summary>
    /// Class that contains the logic to read a trajectory from file and construct a Trajectory object
    /// </summary>
    public class TrajectoryFileReader
    {
        public CoordinateUnit CoordinateUnits { get; set; }
        protected double[][] _trackData;
        protected List<double> _tData, _xData, _yData, _zData, _longitudeData, _latitudeData;
        protected double totalDuration;

        /// <summary>
        /// Construct a TrajectoryFileReader
        /// </summary>
        /// <param name="units"></param>
        public TrajectoryFileReader(CoordinateUnit units)
        {
            CoordinateUnits = units;

            // Initialise empty lists
            _tData = new List<double>();
            _xData = new List<double>();
            _yData = new List<double>();
            _zData = new List<double>();
            _longitudeData = new List<double>();
            _latitudeData = new List<double>();
        }


        /// <summary>
        /// Create a Trajectory object with the data found in the given file
        /// </summary>
        /// <param name="filepath">the file of which the data should be read</param>
        /// <returns>a Trajectory object</returns>
        public Trajectory createTrajectoryFromFile(string filepath)
        {
            readFromFile(filepath);
            convertCoordinates();
            calculateTimeSteps();

            var xSpline = CubicSpline.InterpolateNatural(_tData, _xData);
            var ySpline = CubicSpline.InterpolateNatural(_tData, _yData);
            var zSpline = CubicSpline.InterpolateNatural(_tData, _zData);
            var longitudeSpline = CubicSpline.InterpolateNatural(_tData, _longitudeData);
            var latitudeSpline = CubicSpline.InterpolateNatural(_tData, _latitudeData);

            var trajectory = new Trajectory(xSpline, ySpline, zSpline, longitudeSpline, latitudeSpline);
            trajectory.Duration = (int) totalDuration;
            return trajectory;
        }

        /// <summary>
        /// Read the data from the given file and store it as an array of double arrays
        /// </summary>
        /// <param name="filepath"></param>
        protected void readFromFile(string filepath)
        {
            string rawTrackData = File.ReadAllText(filepath);
            _trackData = rawTrackData
                .Split('\n')
                .Select(q =>
                    q.Split(new[]{" "}, StringSplitOptions.RemoveEmptyEntries)
                     .Select(Convert.ToDouble)
                     .ToArray()
                )
                .ToArray();
        }

        /// <summary>
        /// Convert the input coordinates to metric and geographic coordinates
        /// </summary>
        protected void convertCoordinates()
        {
            foreach (double[] row in _trackData)
            {
                var currentPoint = new Point3D(row[0], row[1], row[2], CoordinateUnits);
                var metricPoint = currentPoint.ConvertTo(CoordinateUnit.metric);
                var geographicPoint = currentPoint.ConvertTo(CoordinateUnit.geographic);

                _xData.Add(metricPoint.X);
                _yData.Add(metricPoint.Y);
                _zData.Add(metricPoint.Z);
                _longitudeData.Add(geographicPoint.X);
                _latitudeData.Add(geographicPoint.Y);
            }
        }

        /// <summary>
        /// Calculate the time between each point in the trajectory
        /// </summary>
        protected void calculateTimeSteps()
        {
            totalDuration = 0;
            _tData.Add(0);

            for (int row = 1; row < _trackData.Length; row++)
            {
                double deltaX = _xData[row] - _xData[row-1];
                double deltaY = _yData[row] - _yData[row-1];
                double deltaZ = _zData[row] - _zData[row-1];
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
                double averageSpeed = ((_trackData[row-1][3] + _trackData[row][3]) / 2) * 0.514;          // TODO: support multiple speed units (default is kts)
                double duration = distance / averageSpeed;
                totalDuration += duration;

                _tData.Add(totalDuration);
            }
        }

    }
}
