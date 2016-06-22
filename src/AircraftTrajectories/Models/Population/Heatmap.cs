using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AircraftTrajectories.Models.Population
{
    using Space3D;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using TemporalGrid;

    public class Heatmap
    {
        protected string _file;
        public int[][] _populationData;

        public Heatmap(string file)
        {
            _file = file;
        }

        public void getPopulationArray()
        {
            string rawData = File.ReadAllText(_file);
            _populationData = rawData
                .Split('\n')
                .Select(q =>
                    q.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(x => {
                         return (int)decimal.Parse(x, NumberStyles.Float);
                     })
                     .ToArray()
                )
                .ToArray();

        }


        public void inputGHeat()
        {
            RDToGeographic converter = new RDToGeographic();

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"points.txt"))

                foreach (int[] row in _populationData)
                {
                    PointF point = converter.convertToLatLong(row[0], row[1]);

                    file.WriteLine(row[2] + "," + point.Y + "," + point.X);
                }

        }



    }
}
