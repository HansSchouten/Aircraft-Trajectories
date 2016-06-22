
using AircraftTrajectories.Models.Space3D;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AircraftTrajectories.Models.Trajectory
{
    public class TrajectoriesFileReader
    {

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
            string flight_id = "";
            var trajectories = new List<Trajectory>();
            TrajectoryGenerator trajectoryGenerator = new TrajectoryGenerator(new Aircraft("GP7270", "wing"), referencePoint);


            // Loop through the positions of all trajectories
            for (int i = 0; i < _trackData.Length; i++)
            {
                // Switch to the next trajectory
                if (i == _trackData.Length - 1 || (_trackData[i][0] != flight_id && i > 0))
                {
                    trajectories.Add(trajectoryGenerator.GenerateTrajectory());

                    // Prepare next trajectory
                    var aircraft = new Aircraft("GP7270", "wing");
                    trajectoryGenerator = new TrajectoryGenerator(aircraft, referencePoint);
                }

                // Prevent failing on empty lines
                if (_trackData[i].Count() == 0) { continue; }
                flight_id = _trackData[i][0];

                // Parse the next position of the current trajectory
                //DateTime t = DateTime.Parse(_trackData[i][14]);
                double x = 0;
                double.TryParse(_trackData[i][4], out x);
                double y = 0;
                double.TryParse(_trackData[i][5], out y);
                double z = 0;
                double.TryParse(_trackData[i][6], out z);
                z = z * 0.3040 * 100;
                trajectoryGenerator.AddDatapoint(x, y, z);
            }

            return trajectories;
        }

    }
}
