using System.Collections.Generic;

namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    using Space3D;
    using System;
    using TemporalGrid;
    using Trajectory;

    class AnnoyanceKMLAnimator : KMLAnimatorSectionInterface
    {
        protected TemporalGrid _temporalGrid;
        protected List<int[]> _populationData;

        public AnnoyanceKMLAnimator(TemporalGrid temporalGrid, List<int[]> populationData)
        {
            _temporalGrid = temporalGrid;
            _populationData = populationData;
        }

        public string KMLSetup()
        {
            int houseId = 0;
            string setupString = "";
            foreach (int[] row in _populationData)
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
                <href>happy2.png</href>
                </Icon>
            </IconStyle>
	    </Style>
        <Point>
            <coordinates>" + coordinates + @"</coordinates>
        </Point>
    </Placemark> 
                ";
            }

            // Force loading angry2.png
            setupString += @"
    <Placemark>
	    <Style>
            <IconStyle>
                <Icon>
                <href>angry2.png</href>
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

        public string KMLAnimationStep(int t)
        {
            Grid grid = _temporalGrid.GetGrid(t);
            int houseId = 0;
            string updateString = "";
            foreach (int[] row in _populationData)
            {
                houseId++;
                double noiseAtHouse = getNoiseValue(row[0], row[1], grid);
                bool awakens = randomBool(getChance(noiseAtHouse), houseId);
                if (awakens)
                {
                    updateString += @"
                        <IconStyle targetId=""house_iconstyle_" + houseId + @""">
                          <scale>1.35</scale>
                          <Icon>
                            <href>angry2.png</href>
                          </Icon>
                        </IconStyle>
                    ";
                }
            }
            return updateString;
        }

        public string KMLFinish()
        {
            return "";
        }


        private double getNoiseValue(int x, int y, Grid grid)
        {
            x -= 104062;
            y -= 475470;
            int stepX = 125;
            int stepY = 125;
            int gridX = (int)Math.Floor((decimal)x / stepX);
            int gridY = (int)Math.Floor((decimal)y / stepY);
            return grid.Data[gridX][gridY];
        }

        private double getChance(double noiseValue)
        {
            //return 0.0087 * Math.Pow(noiseValue - 50.5, 1.79);
            return 0.0087 * Math.Pow(noiseValue - 63, 1.79);
        }

        private Boolean randomBool(double chance, int seed)
        {
            Random randomListCell = new Random(seed);
            return (chance > randomListCell.NextDouble());
        }

    }
}
