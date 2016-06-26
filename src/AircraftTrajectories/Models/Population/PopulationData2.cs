using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AircraftTrajectories.Models.Population
{
    public class PopulationData2
    {
        protected ReferencePoint _referencePoint;
        protected double[][] _populationData;

        /// <summary>
        /// Creates a populationdata object based on input file
        /// </summary>
        public PopulationData2(string file, ReferencePoint referencePoint)
        {
            _referencePoint = referencePoint;
            ReadFile(file);
        }

        protected void ReadFile(string file)
        {
            string rawData = File.ReadAllText(file);
            _populationData = rawData
                .Split('\n')
                .Select(q =>
                    q.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(x => {
                         return (double)decimal.Parse(x, NumberStyles.Float);
                     })
                     .ToArray()
                )
                .ToArray();

            var converter = new MetricToGeographic(new ReferencePointRD());
            for (int r = 0; r < _populationData.Length; r++)
            {
                double[] row = _populationData[r];
                if (row.Length < 3) { continue; }
                var point = converter.ConvertToLongLat(row[0], row[1]);
                _populationData[r][0] = point.Longitude;
                _populationData[r][1] = point.Latitude;
            }
        }

        public Grid FillGrid(Grid grid)
        {
            var lowerLeftGeographic = grid.GridGeoCoordinate(0, 0);
            var upperRightGeographic = grid.GridGeoCoordinate(grid.Data.Length, grid.Data[0].Length);

            foreach (double[] row in _populationData)
            {
                if (row.Length < 3) { continue; }
                if (row[0] < lowerLeftGeographic.Longitude || row[1] < lowerLeftGeographic.Latitude) { continue; }
                if (row[0] > upperRightGeographic.Longitude || row[1] > upperRightGeographic.Latitude) { continue; }

                var longitude = row[0];
                var latitude = row[1];

                int indexX = (int) Math.Floor((longitude - lowerLeftGeographic.Longitude) / (upperRightGeographic.Longitude - lowerLeftGeographic.Longitude) * (grid.Data.Length - 1));
                int indexY = (int) Math.Floor((latitude - lowerLeftGeographic.Latitude) / (upperRightGeographic.Latitude - lowerLeftGeographic.Latitude) * (grid.Data[0].Length - 1));
                
                grid.Data[indexX][indexY] += row[2];
            }

            return grid;
        }
    }
}