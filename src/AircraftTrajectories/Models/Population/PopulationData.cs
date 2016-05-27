using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace AircraftTrajectories.Models.Population
{
    public class PopulationData
    {
        protected string _file;

        public PopulationData(string file)
        {
            _file = file;
        }

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

            double chance = 0.03;
            var chosenPoints = new List<double[]>();
            int i = 0;
            foreach (int[] row in inGridPoints)
            {
                i++;
                if (randomBool(chance, i))
                {
                    chosenPoints.Add(new double[] { row[0], row[1], row[2] });
                }
            }
            return chosenPoints;
        }

        private Boolean randomBool(double chance, int seed)
        {
            Random randomListCell = new Random(seed);
            return (chance > randomListCell.NextDouble());
        }

    }
}
