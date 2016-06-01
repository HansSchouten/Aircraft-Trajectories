using System.Collections.Generic;

namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    using Space3D;
    using System;
    using System.Drawing;
    using TemporalGrid;

    public class AnnoyanceKMLAnimator : KMLAnimatorSectionInterface
    {
        protected TemporalGrid _temporalGrid;
        protected List<double[]> _populationData;
        public string ImageDefault { get; set; }
        public string ImageAnnoyed { get; set; }

        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the population annoyance
        /// </summary>
        /// <returns></returns>
        public AnnoyanceKMLAnimator(TemporalGrid temporalGrid, List<double[]> populationData)
        {
            _temporalGrid = temporalGrid;
            _populationData = populationData;

            ImageDefault = "happy2.png";
            ImageAnnoyed = "angry2.png";
        }

        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the population annoyance
        /// </summary>
        /// <returns></returns>
        public string KMLSetup()
        {
            int houseId = 0;
            string setupString = "";
            foreach (double[] row in _populationData)
            {
                houseId++;
                Point3D geoPoint = (new Point3D(row[0], row[1], 0, CoordinateUnit.metric)).ConvertTo(CoordinateUnit.geographic);
                var coordinates = geoPoint.X + "," + geoPoint.Y + ",0";
                setupString += @"
<Placemark>
	<Style>
        <IconStyle id=""house_iconstyle_" + houseId + @""">
            <scale>0.9</scale>
            <Icon>
                <href>" + ImageDefault + @"</href>
            </Icon>
        </IconStyle>
	</Style>
    <Point>
        <coordinates>" + coordinates + @"</coordinates>
    </Point>
</Placemark> 
                ";
            }

            // Load ImageAnnoyed into Google Earth memory
            setupString += @"
<Placemark>
	<Style>
        <IconStyle>
            <Icon>
                <href>" + ImageAnnoyed + @"</href>
            </Icon>
        </IconStyle>
	</Style>
    <Point>
        <coordinates></coordinates>
    </Point>
</Placemark> 
            ";

            return setupString;
        }

        /// <summary>
        /// Return a string in KML format containing all updates
        /// for the population annoyance at the given moment in time
        /// </summary>
        /// <returns></returns>
        public string KMLAnimationStep(int t)
        {
            Grid grid = _temporalGrid.GetGrid(t);
            int houseId = 0;
            string updateString = "";
            foreach (double[] row in _populationData)
            {
                houseId++;
                Point gridIndex = _temporalGrid.CoordinateToGridIndex(row[0], row[1]);
                double noiseAtHouse = grid.Data[gridIndex.X][gridIndex.Y];
                bool awakens = randomBool(getChance(noiseAtHouse), houseId);
                if (awakens)
                {
                    updateString += @"
                    <IconStyle targetId='house_iconstyle_" + houseId + @"'>
                        <scale>1.3</scale>
                        <Icon>
                            <href>" + ImageAnnoyed + @"</href>
                        </Icon>
                    </IconStyle>
                    ";
                }
            }
            return updateString;
        }

        /// <summary>
        /// Returns a string in KML format containing all after animation definitions 
        /// that are required for the population annoyance
        /// </summary>
        /// <returns></returns>
        public string KMLFinish()
        {
            return "";
        }

        /// <summary>
        /// Returns the chance of someone getting annoyed by a noise value
        /// </summary>
        /// <param name="noiseValue"></param>
        /// <returns></returns>
        protected double getChance(double noiseValue)
        {
            return 0.0087 * Math.Pow(noiseValue - 63, 1.79);
        }

        /// <summary>
        /// Returns true with a given chance
        /// </summary>
        /// <param name="chance"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        protected bool randomBool(double chance, int seed)
        {
            Random randomListCell = new Random(seed);
            return (chance > randomListCell.NextDouble());
        }

    }
}
