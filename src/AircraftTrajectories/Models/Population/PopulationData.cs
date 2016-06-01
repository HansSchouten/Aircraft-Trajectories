using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AircraftTrajectories.Models.Population
{
    public class PopulationData
    {
        protected string _file;
        public double Chance { get; set; }

        /// <summary>
        /// Creates a populationdata object based on input file
        /// </summary>
        /// <param name="file"></param>
        public PopulationData(string file)
        {
            _file = file;
            Chance = 0.03;
        }

        /// <summary>
        /// Reads the population data from the input file into the required format
        /// Based on calculated chances
        /// </summary>
        /// <returns></returns>
        public List<double[]> getPopulationData()
        {
            string rawData = File.ReadAllText(_file);
            int[][] populationData;
            populationData = rawData
                .Split('\n')
                .Select(q =>
                    q.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(x => {
                         return (int)decimal.Parse(x, NumberStyles.Float);
                     })
                     .ToArray()
                )
                .ToArray();

            var inGridPoints = new List<int[]>();
            int minX = 104062;
            int maxX = 112658;
            int minY = 475470;
            int maxY = 485564;
            foreach (int[] row in populationData)
            {
                if (row.Length < 3) { continue; }
                int x = row[0];
                int y = row[1];
                if (x > minX && x < maxX && y > minY && y < maxY)
                {
                    inGridPoints.Add(new int[] { x, y, row[2] });
                }
            }

            var chosenPoints = new List<double[]>();
            int i = 0;
            foreach (int[] row in inGridPoints)
            {
                i++;
                if (randomBool(Chance, i))
                {
                    chosenPoints.Add(new double[] { row[0], row[1], row[2] });
                }
            }
            return chosenPoints;
        }

        /// <summary>
        /// Returns true with the given chance
        /// </summary>
        /// <param name="chance"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        private bool randomBool(double chance, int seed)
        {
            Random randomListCell = new Random(seed);
            return (chance > randomListCell.NextDouble());
        }

    }
}
