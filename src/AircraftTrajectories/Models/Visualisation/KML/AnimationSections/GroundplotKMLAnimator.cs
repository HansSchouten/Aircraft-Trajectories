﻿
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    using Trajectory;

    class GroundplotKMLAnimator : KMLAnimatorSectionInterface
    {
        protected string _plotgroundCoordinates = "";
        protected Trajectory _trajectory;

        public GroundplotKMLAnimator(Trajectory trajectory)
        {
            _trajectory = trajectory;
        }

        public string KMLSetup()
        {
            return "";
        }

        public string KMLAnimationStep(int t)
        {
            _plotgroundCoordinates += _trajectory.Longitude(t) + "," + _trajectory.Latitude(t) + "," + 0 + "\n";
            return "";
        }

        public string KMLFinish()
        {
            return @"
<Style id='plotground_style'>
	<LineStyle>
		<color>30F0BE14</color>
		<gx:physicalWidth>300</gx:physicalWidth>
		<gx:outerColor>25FFFFFF</gx:outerColor>
		<gx:outerWidth>0.95</gx:outerWidth>
	</LineStyle>
</Style>
<Placemark id='plotground_placemark'>
    <name>Plotground</name>
    <styleUrl>#plotground_style</styleUrl>
    <LineString id='plotground'>
        <extrude>0</extrude>
        <tesselate>0</tesselate>
        <altitudeMode>absolute</altitudeMode>
        <coordinates>
        " + _plotgroundCoordinates + @"
        </coordinates>
    </LineString>
</Placemark>
            ";
        }
    }
}