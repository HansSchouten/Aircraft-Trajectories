
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    using Optimisation;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Trajectory;

    public class FitnessGroundPlotKMLAnimator : KMLAnimatorSectionInterface
    {
        protected List<Trajectory> _trajectories;

        int max;
        int min;
        public FitnessGroundPlotKMLAnimator(List<Trajectory> trajectories)
        {
            _trajectories = trajectories;
            min = int.MaxValue;
            max = int.MinValue;
            foreach (Trajectory trajectory in trajectories)
            {
                min = (int) Math.Min(min, trajectory.Fitness);
                max = (int) Math.Max(max, trajectory.Fitness);
            }
        }

        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the plotted ground path
        /// </summary>
        /// <returns></returns>
        public string KMLSetup()
        {
            Color bestColor = Color.FromArgb(220, 255, 255, 255);

            Color c1 = Color.FromArgb(220, 20, 240, 0);
            Color c2 = Color.FromArgb(220, 20, 0, 255);
            Color[] colors = interpolateColors(c1, c2, max-min+1);

            string contourSetup = @"
<Folder>
    <open>0</open>
    <name>Groundpaths</name>
            ";
            for (int i = 0; i < _trajectories.Count; i++)
            {
                var c = colors[(int) _trajectories[i].Fitness - min];
                int width = 80;
                if (_trajectories[i].Fitness == TrajectoryFitness.Best)
                {
                    c = bestColor;
                    width = 200;
                }
                contourSetup += @"
<Style id='multipleplotground_style" + i + @"'>
	<LineStyle>
		<color>30F0BE14</color>
		<gx:physicalWidth>" + width + @"</gx:physicalWidth>
		<gx:outerColor>" + string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B) + @"</gx:outerColor>
		<gx:outerWidth>0.95</gx:outerWidth>
	</LineStyle>
</Style>
<Placemark id='multipleplotground_placemark'>
    <name>Plotground</name>
    <styleUrl>#multipleplotground_style" + i + @"</styleUrl>
    <LineString id='multipleplotground" + i + @"'>
        <extrude>0</extrude>
        <tesselate>0</tesselate>
        <altitudeMode>absolute</altitudeMode>
        <coordinates>
4.75251913070679,52.3099670410156,0
4.75250101089478,52.3100242614746,0
4.75250101089478,52.3099365234375,0
4.75250101089478,52.3098793029785,0
4.75248289108276,52.3098487854004,0
4.75248289108276,52.3098220825195,0
4.75246524810791,52.3097610473633,0
4.7524471282959,52.3097343444824,0
        </coordinates>
    </LineString>
</Placemark>
                ";
            }
            contourSetup += "</Folder>";
            return contourSetup;
        }

        /// <summary>
        /// Return a string in KML format containing all updates
        /// for the plotted ground path at the given moment in time
        /// </summary>
        /// <returns></returns>
        public string KMLAnimationStep(int i)
        {
            string plotgroundCoordinates = "";
            Trajectory trajectory = _trajectories[i];
            for (int t = 0; t < trajectory.Duration; t++)
            {
                plotgroundCoordinates += trajectory.Longitude(t) + "," + trajectory.Latitude(t) + "," + 0 + "\n";
            }

            return @"
            <LineString targetId='multipleplotground" + i + @"'>
                <coordinates>
                " + plotgroundCoordinates + @"
                </coordinates>
            </LineString>
            ";
        }

        /// <summary>
        /// Return a string in KML format containing all updates
        /// for the plotted ground path at the given moment in time
        /// </summary>
        /// <returns></returns>
        public string KMLFinish()
        {
            return "";
        }

        /// <summary>
        /// Interpolates the colors between chosen noise values
        /// </summary>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="numberOfIntervals"></param>
        /// <returns></returns>
        protected Color[] interpolateColors(Color lowerBound, Color upperBound, double numberOfIntervals)
        {
            Color[] colorPalette = new Color[(int) numberOfIntervals];

            double interval_A = (upperBound.A - lowerBound.A) / numberOfIntervals;
            double interval_R = (upperBound.R - lowerBound.R) / numberOfIntervals;
            double interval_G = (upperBound.G - lowerBound.G) / numberOfIntervals;
            double interval_B = (upperBound.B - lowerBound.B) / numberOfIntervals;

            double current_A = lowerBound.A;
            double current_R = lowerBound.R;
            double current_G = lowerBound.G;
            double current_B = lowerBound.B;

            for (var i = 0; i < numberOfIntervals; i++)
            {
                colorPalette[i] = Color.FromArgb((int) current_A, (int) current_R, (int) current_G, (int) current_B);

                current_A += interval_A;
                current_R += interval_R;
                current_G += interval_G;
                current_B += interval_B;
            }

            return colorPalette;
        }
    }
}
