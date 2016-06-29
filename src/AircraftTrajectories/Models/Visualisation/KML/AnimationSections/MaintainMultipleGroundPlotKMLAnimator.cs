
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    using System.Collections.Generic;
    using Trajectory;

    public class MaintainMultipleGroundPlotKMLAnimator : KMLAnimatorSectionInterface
    {
        protected List<Trajectory> _trajectories;

        public MaintainMultipleGroundPlotKMLAnimator(List<Trajectory> trajectories)
        {
            _trajectories = trajectories;
        }

        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the plotted ground path
        /// </summary>
        /// <returns></returns>
        public string KMLSetup()
        {
            string contourSetup = @"
<Folder> 
 <open>0</open>
 <name>Groundpaths</name>
            ";
            for (int i = 0; i < _trajectories.Count; i++)
            {
                contourSetup += @"
<Style id='multipleplotground_style'>
	<LineStyle>
		<color>30F0BE14</color>
		<gx:physicalWidth>150</gx:physicalWidth>
		<gx:outerColor>EEFF4444</gx:outerColor>
		<gx:outerWidth>0.95</gx:outerWidth>
	</LineStyle>
</Style>
<Placemark id='multipleplotground_placemark'>
    <name>Plotground</name>
    <styleUrl>#multipleplotground_style</styleUrl>
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
    }
}
