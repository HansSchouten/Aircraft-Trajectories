using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AircraftTrajectories.Models.Population;
using AircraftTrajectories.Models.Space3D;
using System.Drawing;
using System.Windows.Forms;

namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    public class HeatmapKMLAnimator : KMLAnimatorSectionInterface
    {
        List<double[]> _population;
        public int DotSize;
        public string DotFile;

        public HeatmapKMLAnimator(PopulationData population)
        {
            DotSize = 3000;
            DotFile = "dot2_30.png";
            _population = population.getPopulationData();
        }

        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the aircraft
        /// </summary>
        /// <returns></returns>
        public string KMLSetup()
        {
            RDToGeographic converter = new RDToGeographic();
            string heatmapSetup = "<Folder>";
            for (int i = 1; i < _population.Count; i++)
            {
                if (i % 1000 == 0)
                {
                    Console.WriteLine(i);
                }
                double[] row = _population[i];
                var point = converter.convertToLatLong(row[0], row[1]);
                var test = new GeoPoint3D(point.X, point.Y, 0);
                var upperRight = test.MoveInDirection(DotSize, 45);
                var lowerLeft = test.MoveInDirection(DotSize, 225);

                heatmapSetup += @"
    <GroundOverlay>
      <name> house" + i + @"</name>
      <Icon>
        <href>" + DotFile + @"</href>
      </Icon>
      <LatLonBox>
        <north>" + upperRight.Latitude + @"</north>
        <south>" + lowerLeft.Latitude + @"</south>
        <east>" + upperRight.Longitude + @"</east>
        <west>" + lowerLeft.Longitude + @"</west>
      </LatLonBox>
    </GroundOverlay>
                    ";
            }
            heatmapSetup += @"</Folder>";
                return heatmapSetup;
        }

        /// <summary>
        /// Return a string in KML format containing all updates
        /// for the aircraft at the given moment in time
        /// </summary>
        /// <returns></returns>
        public string KMLAnimationStep(int t)
        {

            return @"
                      ";
        }

        /// <summary>
        /// Returns a string in KML format containing all after animation definitions 
        /// that are required for the aircraft
        /// </summary>
        /// <returns></returns>
        public string KMLFinish()
        {
            return "";
        }
    }


}

