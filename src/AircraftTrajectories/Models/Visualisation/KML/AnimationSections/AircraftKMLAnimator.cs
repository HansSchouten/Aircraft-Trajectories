
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    using Trajectory;
    using Space3D;
    using System;
    public class AircraftKMLAnimator : KMLAnimatorSectionInterface
    {
        protected Aircraft _aircraft;
        protected Trajectory _trajectory;

        public AircraftKMLAnimator(Aircraft aircraft, Trajectory trajectory)
        {
            _aircraft = aircraft;
            _trajectory = trajectory;
        }

        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the aircraft
        /// </summary>
        /// <returns></returns>
        public string KMLSetup()
        {
            GeoPoint3D startingPoint = _trajectory.GeoPoint(0);

            return @"
<Style id=""pushpin"">
    <IconStyle>
        <scale>0</scale>
    </IconStyle>
    <LabelStyle>
        <color>FFFFFFFF</color>
        <scale>0.45</scale>
    </LabelStyle>
</Style >

<Placemark>
	<name>Aircraft model</name>
	<visibility>1</visibility>
	<Model id='model'>
		<altitudeMode>absolute</altitudeMode>
		<Location id='model_location'>
			<latitude>" + startingPoint.Latitude + @"</latitude>
			<longitude>" + startingPoint.Longitude + @"</longitude>
			<altitude>" + startingPoint.Z + @"</altitude>
		</Location>
		<Orientation id='model_orientation'>
			<heading>0.0</heading>
			<tilt>0.0</tilt>
			<roll>0.0</roll>
		</Orientation>
		<Scale id='model_scale'>
			<x>3.5</x>
			<y>3.5</y>
			<z>3.5</z>
		</Scale>
		<Link>
			<href>" + _aircraft.Model + @"</href>
		</Link>
	</Model>
</Placemark>

<Placemark id='pin'>
		<name>" + _aircraft.Model.ToString().Remove(_aircraft.Model.ToString().Length - 4) + @"</name>
        <styleUrl>#pushpin</styleUrl>
        <altitudeMode>absolute</altitudeMode>
		<MultiGeometry>
			<Point id='aircraftpin'>
                <altitudeMode>absolute</altitudeMode>
				<coordinates>4.73729753494263,52.2891273498535,10.668</coordinates>
			</Point>		
			<Polygon>
			</Polygon>
		</MultiGeometry>
	</Placemark>
                    ";
        }

        /// <summary>
        /// Return a string in KML format containing all updates
        /// for the aircraft at the given moment in time
        /// </summary>
        /// <returns></returns>
        public string KMLAnimationStep(int t)
        {
            GeoPoint3D animationPoint = _trajectory.GeoPoint(t).MoveInDirection(70, (_trajectory.Heading(t) + 210) % 360);

            return @"
            <Location targetId='model_location'>
			    <latitude>" + animationPoint.Latitude + @"</latitude>
			    <longitude>" + animationPoint.Longitude + @"</longitude>
			    <altitude>" + animationPoint.Z + @"</altitude>
            </Location>
            <Orientation targetId='model_orientation'>
                <heading>" + _trajectory.Heading(t) + @"</heading>
                <tilt>" + _trajectory.Tilt(t) + @"</tilt>
                <roll>" + _trajectory.BankAngle(t) + @"</roll>
            </Orientation>
            <Placemark targetId='pin'>
            <name>" + _aircraft.Model.ToString().Remove(_aircraft.Model.ToString().Length - 4) + @" " + Math.Round(animationPoint.Z, 3) +
            @"</name>
            </Placemark>
            <Point targetId='aircraftpin'>
            <coordinates>" + animationPoint.Longitude + @","
            + animationPoint.Latitude + @","
            + (animationPoint.Z + 50) + @"</coordinates>
            </Point>
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