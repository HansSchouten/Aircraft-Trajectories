using AircraftTrajectories.Models.Space3D;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AircraftTrajectories.Models.Trajectory
{
    public class TrajectoriesFileReader
    {
        protected int _minX = int.MaxValue;
        protected int _minY = int.MaxValue;
        protected int _maxX = int.MinValue;
        protected int _maxY = int.MinValue;
        public Point3D LowerLeftPoint {
            get
            {
                return new Point3D(_minX, _minY);
            }
        }
        public Point3D UpperRightPoint
        {
            get
            {
                return new Point3D(_maxX, _maxY);
            }
        }

        public List<Trajectory> CreateFromFile(string filePath, ReferencePoint referencePoint)
        {
            // Parse the file containing multiple trajectories
            string rawTrackData = File.ReadAllText(filePath);
            var _trackData = rawTrackData
                .Split('\n')
                .Select(q =>
                    q.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(Convert.ToString)
                     .ToArray()
                )
                .ToArray();


            // Define variables
            DateTime t0 = DateTime.Parse(_trackData[0][14]);
            string flight_id = "";
            var trajectories = new List<Trajectory>();
            TrajectoryGenerator trajectoryGenerator = new TrajectoryGenerator(new Aircraft("GP7270", "wing"), referencePoint);


            // Loop through the positions of all trajectories
            for (int i = 0; i < _trackData.Length; i++)
            {
                // Switch to the next trajectory
                if (i == _trackData.Length - 1 || (_trackData[i][0] != flight_id && i > 0))
                {
                    Trajectory trajectory = trajectoryGenerator.GenerateTrajectory();
                    _minX = (int) Math.Min(trajectory.LowerLeftPoint.X, _minX);
                    _minY = (int) Math.Min(trajectory.LowerLeftPoint.Y, _minY);
                    _maxX = (int) Math.Max(trajectory.UpperRightPoint.X, _maxX);
                    _maxY = (int) Math.Max(trajectory.UpperRightPoint.X, _maxY);
                    trajectories.Add(trajectory);

                    // Prepare next trajectory
                    t0 = DateTime.Parse(_trackData[i][14]);
                    var aircraft = new Aircraft("GP7270", "wing");
                    trajectoryGenerator = new TrajectoryGenerator(aircraft, referencePoint);
                }

                // Prevent failing on empty lines
                if (_trackData[i].Count() == 0) { continue; }
                flight_id = _trackData[i][0];

                // Parse the next position of the current trajectory
                double x = 0;
                double.TryParse(_trackData[i][4], out x);
                x = x * 14.46875;
                double y = 0;
                double.TryParse(_trackData[i][5], out y);
                y = y * 14.46875;
                double z = 0;
                double.TryParse(_trackData[i][6], out z);
                z = z * 0.3040 * 100;
                DateTime t = DateTime.Parse(_trackData[i][14]);
                double time = t.Subtract(t0).TotalSeconds;
                trajectoryGenerator.AddDatapoint(x, y, z, 200, 60000, time);
            }
            
            return trajectories;
        }

    }
}
