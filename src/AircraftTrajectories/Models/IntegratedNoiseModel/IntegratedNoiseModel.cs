using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace AircraftTrajectories.Models.IntegratedNoiseModel
{
    using Trajectory;
    using TemporalGrid;
    using System.Collections.Generic;
    using Space3D;
    public class IntegratedNoiseModel
    {
        
        protected Trajectory _trajectory;
        protected Aircraft _aircraft;
        public TemporalGrid TemporalGrid { get; protected set; }
        public ReferencePoint ReferencePoint { get; set; }
        public string FileSuffix = "";
        public int TrajectoryBound { get; set; }
        public int NoiseMetric { get; set; }
        public bool IntegrateToCurrentPosition { get; set; }
        protected string _gridName;
        public List<double[]> PopulatedAreaNoise { get; protected set; }
        public string GridName {
            get { return _gridName;  }
            set {
                _gridName = value;
                _customGrid = true;
            }
        }
        protected bool _customGrid = false;
        public int CellSize { get; set; }

        // Noise calculations need to be added to a larger grid
        protected bool _mapToLargerGrid = false;
        protected Point3D _lowerLeftPoint;
        protected Point3D _upperRightPoint;

        // Max aircraft distance
        protected bool _useMaxDistanceFromAirport = false;
        protected Point3D _airportPoint;
        protected int _maxDistanceFromAirport;

        protected BackgroundWorker _backgroundWorker;


        /// <summary>
        /// Construct an IntegratedNoiseModel
        /// </summary>
        /// <param name="trajectory">The trajectory for which the aircraft noise will be calculated</param>
        /// <param name="aircraft"></param>
        public IntegratedNoiseModel(Trajectory trajectory, Aircraft aircraft)
        {
            CellSize = 125;
            IntegrateToCurrentPosition = false;
            _gridName = "Grid2D";
            NoiseMetric = 1;
            ReferencePoint = new ReferencePointRD();
            TrajectoryBound = 5000;
            _trajectory = trajectory;
            _aircraft = aircraft;
        }

        public void MapToLargerGrid(Point3D lowerLeftPoint, Point3D upperRightPoint)
        {
            lowerLeftPoint.X -= TrajectoryBound;
            lowerLeftPoint.Y -= TrajectoryBound;
            _lowerLeftPoint = lowerLeftPoint;

            upperRightPoint.X += TrajectoryBound;
            upperRightPoint.Y += TrajectoryBound;
            _upperRightPoint = upperRightPoint;

            _mapToLargerGrid = true;
        }

        public void MaxDistanceFromAirport(Point3D airport, int maxDistance)
        {
            _airportPoint = airport;
            _maxDistanceFromAirport = maxDistance;
            _useMaxDistanceFromAirport = true;

            if (_mapToLargerGrid)
            {
                _lowerLeftPoint.X = _airportPoint.X - maxDistance - (2 * CellSize);
                _lowerLeftPoint.Y = _airportPoint.Y - maxDistance - (2 * CellSize);
                _upperRightPoint.X = _airportPoint.X + maxDistance + (2 * CellSize);
                _upperRightPoint.Y = _airportPoint.Y + maxDistance + (2 * CellSize);
            }
        }

        public void StartCalculation(Action<double> progressChangedCallback)
        {
            CreateGridFile();
            TemporalGrid = new TemporalGrid();
            TemporalGrid.Interval = 1;

            double progress = 0;
            for (int t = 0; t <= _trajectory.Duration; t++)
            {
                if (IntegrateToCurrentPosition)
                {
                    CreateTrajectoryToCurrentPosition(t);
                } else
                {
                    CreatePositionFile(t);
                }
                ExecuteINMTM();

                double[][] noiseData = ReadNoiseData();
                Grid grid = NoiseDataToGrid(noiseData, !_mapToLargerGrid);
                if (_mapToLargerGrid)
                {
                    grid = Grid.MapOnLargerGrid(grid, _lowerLeftPoint, _upperRightPoint);
                }
                TemporalGrid.AddGrid(grid);

                progress = (t / (double)_trajectory.Duration) * 100;
                progressChangedCallback(progress);
            }
            progress = 100;
            progressChangedCallback(progress);
        }


        public void RunINMFullTrajectory()
        {
            TemporalGrid = new TemporalGrid();
            TemporalGrid.Interval = 1;

            CreateTrajectoryFile();
            CreateGridFile();
            ExecuteINMTM();

            if (PopulatedAreaNoise != null)
            {
                ReadPopulatedAreaNoise();
            }
            else
            {
                double[][] noiseData = ReadNoiseData();
                Grid grid = NoiseDataToGrid(noiseData, !_mapToLargerGrid);
                if (_mapToLargerGrid)
                {
                    Console.WriteLine(_lowerLeftPoint.X + "," + _lowerLeftPoint.Y);
                    Console.WriteLine(_upperRightPoint.X + "," + _upperRightPoint.Y);
                    grid = Grid.MapOnLargerGrid(grid, _lowerLeftPoint, _upperRightPoint);
                }
                grid.ReferencePoint = ReferencePoint;
                TemporalGrid.AddGrid(grid);
            }
        }

        /// <summary>
        /// Creates the position file for a particular time step
        /// </summary>
        /// <param name="t"></param>
        protected void CreatePositionFile(int t)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Globals.currentDirectory + "current_position"+ FileSuffix + ".dat", false))
            {
                file.WriteLine("Sys");
                file.WriteLine("====================================================================================");
                file.WriteLine("met");
                file.WriteLine("         x         y         h         V         T         m");
                file.WriteLine("====================================================================================");
                file.WriteLine(_trajectory.X(t)        + "      " + _trajectory.Y(t)        + "     " + _trajectory.Z(t)        + "     " + _trajectory.Speed(t) + "      " + _trajectory.Thrust(t) + "        2");
                file.WriteLine(_trajectory.X(t + 0.01) + "      " + _trajectory.Y(t + 0.01) + "     " + _trajectory.Z(t + 0.01) + "     " + _trajectory.Speed(t + 0.01) + "      " + _trajectory.Thrust(t + 0.01) + "        2");
                file.WriteLine();
                file.WriteLine("nois_id / engine mount");
                file.WriteLine("====================================================================================");
                file.WriteLine(_aircraft.EngineId);
                file.WriteLine(_aircraft.EngineMount);
            }
        }

        protected void CreateTrajectoryToCurrentPosition(int t_end)
        {
            using (StreamWriter file =
                    new StreamWriter(Globals.currentDirectory + "current_position" + FileSuffix + ".dat", false))
            {
                file.WriteLine("Sys");
                file.WriteLine("====================================================================================");
                file.WriteLine("met");
                file.WriteLine("         x         y         h         V         T         m");
                file.WriteLine("====================================================================================");
                for (int t = 0; t < t_end; t = t+20)
                {
                    file.WriteLine(_trajectory.X(t) + "      " + _trajectory.Y(t)  + "     " + _trajectory.Z(t) + "     " + _trajectory.Speed(t) + "       " + _trajectory.Thrust(t) + "        2");
                }
                file.WriteLine(_trajectory.X(t_end) + "      " + _trajectory.Y(t_end) + "     " + _trajectory.Z(t_end) + "     " + _trajectory.Speed(t_end) + "       " + _trajectory.Thrust(t_end) + "        2");
                file.WriteLine();
                file.WriteLine("nois_id / engine mount");
                file.WriteLine("====================================================================================");
                file.WriteLine(_aircraft.EngineId);
                file.Write(_aircraft.EngineMount);
            }
        }

        /// <summary>
        /// Creates the position file for the whole trajectory
        /// </summary>
        protected void CreateTrajectoryFile()
        {
            using (StreamWriter file =
                    new StreamWriter(Globals.currentDirectory + "current_position" + FileSuffix + ".dat", false))
            {
                file.WriteLine("Sys");
                file.WriteLine("====================================================================================");
                file.WriteLine("met");
                file.WriteLine("         x         y         h         V         T         m");
                file.WriteLine("====================================================================================");
                for (int t = 0; t < _trajectory.Duration; t = t+20)
                {
                    file.WriteLine(_trajectory.X(t) + "      " + _trajectory.Y(t)  + "     " + _trajectory.Z(t) + "     " + _trajectory.Speed(t) + "       " + _trajectory.Thrust(t) + "        2");
                }
                file.WriteLine();
                file.WriteLine("nois_id / engine mount");
                file.WriteLine("====================================================================================");
                file.WriteLine(_aircraft.EngineId);
                file.Write(_aircraft.EngineMount);
            }
        }

        protected void CreateGridFile()
        {
            if (_customGrid) { return; }

            double minX = (_trajectory.LowerLeftPoint.X - TrajectoryBound);
            double minY = (_trajectory.LowerLeftPoint.Y - TrajectoryBound);
            double maxX = (_trajectory.UpperRightPoint.X + TrajectoryBound);
            double maxY = (_trajectory.UpperRightPoint.Y + TrajectoryBound);

            if (_useMaxDistanceFromAirport)
            {
                minX = Math.Max(minX, _airportPoint.X - _maxDistanceFromAirport);
                minY = Math.Max(minY, _airportPoint.Y - _maxDistanceFromAirport);
                maxX = Math.Min(maxX, _airportPoint.X + _maxDistanceFromAirport);
                maxY = Math.Min(maxY, _airportPoint.Y + _maxDistanceFromAirport);
            }

            using (StreamWriter file =
                    new StreamWriter(Globals.currentDirectory + GridName + ".dat", false))
            {
                file.Write("     x_min     x_max      x_sz     y_min     y_max      y_sz\n");
                file.Write("====================================================================================\n");
                file.Write(minX + "      " + maxX + "     "+ CellSize + "     " + minY + "      " + maxY + "       "+ CellSize);
            }
        }

        public void UsePopulationGrid(Grid grid, int threshold)
        {
            PopulatedAreaNoise = new List<double[]>();
            GridName = "Grid3D";

            using (StreamWriter file =
                    new StreamWriter(Globals.currentDirectory + GridName + ".dat", false))
            {
                file.Write("                  x[m]                  y[m]                  z[m]\n");
                file.Write("====================================================================================");
                for (int r = 0; r < grid.Data.Length; r++)
                {
                    for (int c = 0; c < grid.Data[0].Length; c++)
                    {
                        double value = grid.Data[r][c];
                        if (value > threshold)
                        {
                            var point = grid.GridCoordinate(r, c);
                            PopulatedAreaNoise.Add(new double[] { value, 0 } );
                            file.Write("\n" + point.X + "      " + point.Y + "     0");
                        }
                    }
                }
            }
        }




        /// <summary>
        /// Starts the execution process of the noise model
        /// </summary>
        protected void ExecuteINMTM()
        {
            Process process = new Process();
            process.StartInfo.FileName = Globals.currentDirectory + "INMTM_v3.exe";
            process.StartInfo.Arguments = "current_position"+ FileSuffix + ".dat " + GridName + ".dat";
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

        protected void ReadPopulatedAreaNoise()
        {
            double[][] noiseValues = ReadNoiseData();
            for (int i = 0; i < noiseValues.Length-1; i++)
            {
                PopulatedAreaNoise[i][1] = noiseValues[i][3 + NoiseMetric];
            }
        }

        /// <summary>
        /// Converts the noise output into a grid of noise values
        /// </summary>
        /// <param name="noiseData"></param>
        /// <returns></returns>
        protected Grid NoiseDataToGrid(double[][] noiseData, bool calculateContours = true)
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
                
                column.Add(noiseData[i][3 + NoiseMetric]);
            }
            noiseDataGrid[columnIndex] = column.ToArray();
            
            Grid grid = new Grid(noiseDataGrid, new Point3D(noiseData[0][0], noiseData[0][1]), CellSize, calculateContours);
            grid.ReferencePoint = ReferencePoint;
            return grid;
        }

    }
}