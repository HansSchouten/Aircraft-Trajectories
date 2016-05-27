﻿using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace AircraftTrajectories.Models.IntegratedNoiseModel
{
    using AircraftTrajectories.Models.Trajectory;
    using AircraftTrajectories.Models.TemporalGrid;
    using System.Collections.Generic;

    public class IntegratedNoiseModel
    {
        
        protected Trajectory _trajectory;
        protected Aircraft _aircraft;
        public TemporalGrid TemporalGrid { get; protected set; }

        protected BackgroundWorker _backgroundWorker;
        protected ProgressBar _progressBar;

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
        /// Starts the noise calculation while keeping track of the progress
        /// </summary>
        /// <param name="calculationCompletedCallback"></param>
        /// <param name="progressBar"></param>
        public void StartCalculation(Action calculationCompletedCallback, ProgressBar progressBar = null)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerSupportsCancellation = false;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.ProgressChanged += delegate (object sender, ProgressChangedEventArgs e)
            {
                if (_progressBar != null)
                {
                    _progressBar.Value = e.ProgressPercentage;
                }
            };
            _backgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
            {
                calculationCompletedCallback();
            };

            _progressBar = progressBar;
            _backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Calculate the noise produced by the aircraft for every second along the defined trajectory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TemporalGrid = new TemporalGrid();
            TemporalGrid.Interval = 1;

            Progress = 0;
            for (int t = 0; t <= _trajectory.Duration; t++)
            {
                CreatePositionFile(t);
                ExecuteINMTM();
                double[][] noiseData = ReadNoiseData();
                Grid grid = NoiseDataToGrid(noiseData);
                TemporalGrid.AddGrid(grid);

                Progress = (t / (double)_trajectory.Duration) * 100;
                _backgroundWorker.ReportProgress((int) Progress);
                Console.WriteLine(Progress);
            }
            Progress = 100;
            _backgroundWorker.ReportProgress(100);
        }

        /// <summary>
        /// Creates the position file for a particular time step
        /// </summary>
        /// <param name="t"></param>
        protected void CreatePositionFile(int t)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Globals.currentDirectory + "current_position.dat", false))
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

        /// <summary>
        /// Starts the execution process of the noise model
        /// </summary>
        protected void ExecuteINMTM()
        {
            Process process = new Process();
            process.StartInfo.FileName = Globals.currentDirectory + "INMTM_v3.exe";
            process.StartInfo.Arguments = "current_position.dat schiphol_grid2D.dat";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
        }
        
        /// <summary>
        /// Parses the noise output values of the noise model
        /// </summary>
        /// <returns></returns>
        protected double[][] ReadNoiseData()
        {
            string rawNoise = File.ReadAllText(Globals.currentDirectory + "noise.out");
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

        /// <summary>
        /// Converts the noise output into a grid of noise values
        /// </summary>
        /// <param name="noiseData"></param>
        /// <returns></returns>
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