
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    using Trajectory;

    public class AirplotKMLAnimator : KMLAnimatorSectionInterface
    {
        protected string _plotairCoordinates = "";
        protected Trajectory _trajectory;

        public AirplotKMLAnimator(Trajectory trajectory)
        {
            _trajectory = trajectory;
        }

        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the plotted air path
        /// </summary>
        /// <returns></returns>
        public string KMLSetup()
        {
            return @"
<Style id='plotair_style'>
    <LineStyle>
        <color>60F0B414</color>
        <width>0</width>
    </LineStyle>
    <PolyStyle>
        <color>60F0B414</color>
        <colorMode>normal</colorMode>
        <fill>1</fill>
    </PolyStyle>
</Style>
<Placemark id='plotair_placemark'>
    <name>Plotair</name>
    <styleUrl>#plotair_style</styleUrl>
    <LineString id='plotair'>
        <extrude>1</extrude>
        <tesselate>1</tesselate>
        <altitudeMode>absolute</altitudeMode>
        <coordinates>
        </coordinates>
    </LineString>
</Placemark>
            ";
        }

        /// <summary>
        /// Return a string in KML format containing all updates
        /// for the plotted air path at the given moment in time
        /// </summary>
        /// <returns></returns>
        public string KMLAnimationStep(int t)
        {
            _plotairCoordinates += _trajectory.Longitude(t) + "," + _trajectory.Latitude(t) + "," + _trajectory.Z(t) + "\n";
            return @"
            <LineString targetId='plotair'>
            <coordinates>
            " + _plotairCoordinates + @"
            </coordinates>
            </LineString>
            ";
        }

        /// <summary>
        /// Returns a string in KML format containing all after animation definitions 
        /// that are required for the plotted air path
        /// </summary>
        /// <returns></returns>
        public string KMLFinish()
        {
            return "";
        }
    }
}
