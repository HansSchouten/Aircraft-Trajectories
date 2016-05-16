using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AircraftTrajectories.Models.IntegratedNoiseModel
{
    using AircraftTrajectories.Models.Trajectory;
    using AircraftTrajectories.Models.Space3D;
    using AircraftTrajectories.Models.TemporalGrid;
    using System.Collections.Generic;

    class IntegratedNoiseModel
    {
        protected string _currentFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        protected Trajectory _trajectory;
        protected Aircraft _aircraft;
        protected TemporalGrid _temporalGrid;
        public double Progress { get; protected set; }


        /// <summary>
        /// Construct an IntegratedNoiseModel
        /// </summary>
        /// <param name="trajectory">The trajectory for which the aircraft noise will be calculated</param>
        /// <param name="aircraft"></param>
        public IntegratedNoiseModel(Trajectory trajectory, Aircraft aircraft)
        {
            _trajectory = trajectory;
            _aircraft = aircraft;
        }

        /// <summary>
        /// Calculate the noise produced by the aircraft for every second along the defined trajectory
        /// </summary>
        public TemporalGrid CalculateNoise()
        {
            _temporalGrid = new TemporalGrid();
            _temporalGrid.Interval = 1;

            Progress = 0;
            for (int t = 0; t <= _trajectory.Duration; t++)
            {
                CreatePositionFile(t);
                ExecuteINMTM();
                double[][] noiseData = ReadNoiseData();
                Grid grid = NoiseDataToGrid(noiseData);
                _temporalGrid.addGrid(grid);

                Progress = (t / _trajectory.Duration) * 100;
                Console.WriteLine(Progress);
            }
            Progress = 100;

            return _temporalGrid;
        }

        protected void CreatePositionFile(int t)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(_currentFolder + "/current_position.dat", false))
            {
                file.WriteLine("Sys");
                file.WriteLine("====================================================================================");
                file.WriteLine("met");
                file.WriteLine("         x         y         h         V         T         m");
                file.WriteLine("====================================================================================");
                file.WriteLine(_trajectory.X(t)        + "      " + _trajectory.Y(t)        + "     " + _trajectory.Z(t)        + "     400      50000        2");
                file.WriteLine(_trajectory.X(t + 0.01) + "      " + _trajectory.Y(t + 0.01) + "     " + _trajectory.Z(t + 0.01) + "     400      50000        2");
                file.WriteLine();
                file.WriteLine("nois_id / engine mount");
                file.WriteLine("====================================================================================");
                file.WriteLine(_aircraft.EngineId);
                file.WriteLine(_aircraft.EngineMount);
            }
        }

        protected void ExecuteINMTM()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = Application.StartupPath;
            startInfo.FileName = "INMTM_v3.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "current_position.dat schiphol_grid2D.dat";
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }

        protected double[][] ReadNoiseData()
        {
            string rawNoise = File.ReadAllText(_currentFolder + "/noise.out");
            double[][] noiseData = rawNoise
                .Split('\n')
                .Skip(2)
                .Select(q =>
                    q.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(Convert.ToDouble)
                     .ToArray()
                )
                .ToArray();
            return noiseData;
        }

        protected Grid NoiseDataToGrid(double[][] noiseData)
        {
            // Store noise levels in a 2D grid
            double[][] noiseDataGrid = { };
            double currentX = noiseData[0][0];
            List<double> column = new List<double>();
            int columnIndex = 0;
            for (int i = 0; i < noiseData.Length - 1; i++) {
                // Check whether we encountered a new column
                if (currentX != noiseData[i][0]) {
                    // Check whether this was the first column
                    if (columnIndex == 0) {
                        // Now the total number of columns of the grid is known
                        int numberOfColumns = noiseData.Length / column.Count;
                        noiseDataGrid = new double[numberOfColumns][];
                    }
                    // Add the column to the grid
                    noiseDataGrid[columnIndex] = column.ToArray();

                    column = new List<double>();
                    currentX = noiseData[i][0];
                    columnIndex++;
                }
                
                column.Add(noiseData[i][4]);
            }
            noiseDataGrid[columnIndex] = column.ToArray();

            return new Grid(noiseDataGrid);
        }

    }
}