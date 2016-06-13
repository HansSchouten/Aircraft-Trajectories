using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AircraftTrajectories.Models.Optimisation
{
    /// <summary>
    /// A singleton class that interpolates noise values based on the NPD database
    /// </summary>
    public class NoisePowerDistance
    {
        private static NoisePowerDistance instance;
        protected Dictionary<string, List<double[]>> _INMData;
        protected List<double> _distances = new List<double>() { 200, 400, 630, 1000, 2000, 4000, 6300, 10000, 16000, 25000 };
        protected List<double> _logDistances = new List<double>() { 2.301, 2.602, 2.799, 3.0, 3.301, 3.602, 3.799, 4.0, 4.204, 4.398 };
        public Grid LAMaxGrid { get; set; }

        /// <summary>
        /// Create a NPD object
        /// </summary>
        private NoisePowerDistance()
        {
            readINMData();
        }
        
        /// <summary>
        /// Read and process the NPD database
        /// </summary>
        protected void readINMData()
        {
            _INMData = new Dictionary<string, List<double[]>>();
            string currentOperatingMode = "", currentKey = "";
            var engineData = new List<double[]>();
            foreach (string[] row in ReadRawData()) {
                if (row[2] != currentOperatingMode) {
                    if (currentOperatingMode != "") {
                        _INMData.Add(currentKey, engineData);
                    }
                    engineData = new List<double[]>();
                    currentOperatingMode = row[2];
                    currentKey = row[0] + row[1] + row[2];
                }

                double[] engineThrustNoiseData = new double[11];
                for (int c = 3; c < row.Length-1; c++) {
                    engineThrustNoiseData[c-3] = Double.Parse(row[c]);
                }
                engineData.Add(engineThrustNoiseData);
            }
        }

        /// <summary>
        /// Read the raw NPD database from disk
        /// </summary>
        /// <returns></returns>
        protected string[][] ReadRawData()
        {
            string rawData = File.ReadAllText(Globals.currentDirectory + "INM_data.dat");
            string[][] INMData = rawData
                .Split('\n')
                .Skip(2)
                .Select(q =>
                    q.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(Convert.ToString)
                     .ToArray()
                )
                .ToArray();
            return INMData;
        }

        /// <summary>
        /// Return the singleton instance
        /// </summary>
        public static NoisePowerDistance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NoisePowerDistance();
                }
                return instance;
            }
        }

        /// <summary>
        /// Return the interpolated noise value
        /// </summary>
        /// <param name="engineId">The engine used by the aircraft</param>
        /// <param name="noiseMetric">The noise metric we are currently calculating</param>
        /// <param name="operationalMode">The operational mode (takeoff, landing, overflight)</param>
        /// <param name="distance">The slant range from the aircraft to the point on the ground</param>
        /// <param name="thrust">The current trust setting</param>
        /// <returns></returns>
        public double GetNoiseValue(string engineId, char noiseMetric, char operationalMode, double distance, double thrust)
        {
            double D = Math.Log10(distance);
            List<double[]> engineData;
            _INMData.TryGetValue(engineId+noiseMetric+operationalMode, out engineData);

            int PIndex1, PIndex2, DIndex1, DIndex2;
            selectP(engineData, thrust, out PIndex1, out PIndex2);
            double D1, D2;
            selectD(engineData, D, out DIndex1, out DIndex2, out D1, out D2);

            double P1 = engineData[PIndex1][0];
            double P2 = engineData[PIndex2][0];
            double LP1D1 = engineData[PIndex1][DIndex1];
            double LP1D2 = engineData[PIndex1][DIndex2];
            double LP2D1 = engineData[PIndex2][DIndex1];
            double LP2D2 = engineData[PIndex2][DIndex2];

            double LP1D = LP1D1 + (((LP1D2 - LP1D1) * (D - D1)) / (D2 - D1));
            double LP2D = LP2D1 + (((LP2D2 - LP2D1) * (D - D1)) / (D2 - D1));
            return LP1D + (((LP2D - LP1D) * (thrust - P1)) / (P2 - P1));
        }
        
        /// <summary>
        /// Calculate the noise over the whole grid for the given aircraft position and thrust
        /// </summary>
        /// <param name="aircraftPosition">The current aircraft position</param>
        /// <param name="thrust">The current thrust</param>
        public void CalculateNoise(Point3D aircraftPosition, double thrust)
        {
            int cellSize = 250;
            double dx, dy, D;
            double[][] data = new double[200][];
            for (int x = 0; x < 200 * cellSize; x = x + cellSize) {
                double[] col = new double[200];
                for (int y = 0; y < 200 * cellSize; y = y + cellSize) {
                    dx = (x - aircraftPosition.X);
                    dy = (y - aircraftPosition.Y);
                    D = Math.Log10(Math.Sqrt(dx * dx + dy * dy + aircraftPosition.Z * aircraftPosition.Z));

                    var noise = GetNoiseValue("GP7270", 'M', 'D', D, thrust);
                    col[y / cellSize] = (LAMaxGrid == null) ? noise : Math.Max(noise, LAMaxGrid.Data[x / cellSize][y / cellSize]);
                }
                data[x / cellSize] = col;
            }
            Grid grid = new Grid(data, false);
            LAMaxGrid = grid;
        }

        /// <summary>
        /// Select the thrust values from the given enginedata closest to the given real thrust value
        /// </summary>
        /// <param name="engineData">The engine data from the NPD database</param>
        /// <param name="realThrust">The current thrust value</param>
        /// <param name="PIndex1">The index of the closest thrust value</param>
        /// <param name="PIndex2">The index of the second closest thrust value</param>
        protected void selectP(List<double[]> engineData, double realThrust, out int PIndex1, out int PIndex2)
        {
            PIndex1 = 0;
            PIndex2 = 1;

            for (int i=1; i < engineData.Count-1; i++) {
                if (realThrust >= engineData[i][0]) {
                    PIndex1 = i;
                    PIndex2 = i+1;
                }
            }
        }

        /// <summary>
        /// Select the distance values from the given enginedata closest to the given real distance value
        /// </summary>
        /// <param name="engineData">The engine data from the NPD database</param>
        /// <param name="realDistance">The current distance value</param>
        /// <param name="DIndex1">The index of the closest distance value</param>
        /// <param name="DIndex2">The index of the second closest distance value</param>
        /// <param name="D1">The distance value stored at DIndex1</param>
        /// <param name="D2">The distance value stored at DIndex2</param>
        protected void selectD(List<double[]> engineData, double realDistance, out int DIndex1, out int DIndex2, out double D1, out double D2)
        {
            DIndex1 = 1;
            DIndex2 = 2;
            D1 = _logDistances[0];
            D2 = _logDistances[1];

            for (int i = 1; i < _logDistances.Count - 1; i++) {
                if (realDistance >= _logDistances[i]) {
                    DIndex1 = i + 1;
                    DIndex2 = i + 2;
                    D1 = _logDistances[i];
                    D2 = _logDistances[i+1];
                }
            }
        }
    }
}
