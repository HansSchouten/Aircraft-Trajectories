using AircraftTrajectories.Models.Space3D;
using MathNet.Numerics.Interpolation;
using System;
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
        protected double totalDuration;
        protected TrajectoryGenerator _trajectoryGenerator;

        /// <summary>
        /// Construct a TrajectoryFileReader
        /// </summary>
        /// <param name="units"></param>
        public TrajectoryFileReader(CoordinateUnit units, TrajectoryGenerator trajectoryGenerator)
        {
            CoordinateUnits = units;
            _trajectoryGenerator = trajectoryGenerator;
        }


        /// <summary>
        /// Create a Trajectory object with the data found in the given file
        /// </summary>
        /// <param name="filepath">the file of which the data should be read</param>
        /// <returns>a Trajectory object</returns>
        public Trajectory CreateTrajectoryFromFile(string filepath)
        {
            readFromFile(filepath);

            double t = 0;
            double previousV = 0;
            Point3D previousPoint = null;
            foreach (double[] row in _trackData)
            {
                if (row.Length == 0) { continue; }

                var currentPoint = new Point3D(row[0], row[1], row[2], CoordinateUnits);
                var metricPoint = currentPoint.ConvertTo(CoordinateUnit.metric);
                var currentV = row[3];

                if (previousPoint != null)
                {
                    t += previousPoint.DistanceTo(metricPoint) / ((currentV + previousV) / 2);
                }

                _trajectoryGenerator.AddDatapoint(metricPoint.X, metricPoint.Y, metricPoint.Z, row[3], row[4], t);

                previousPoint = currentPoint;
                previousV = currentV;
            }

            Trajectory trajectory = _trajectoryGenerator.GenerateTrajectory();
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

    }
}
