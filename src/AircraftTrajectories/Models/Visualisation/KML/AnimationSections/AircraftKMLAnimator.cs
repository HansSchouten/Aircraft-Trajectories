
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    using Trajectory;
    using Space3D;

    public class AircraftKMLAnimator : KMLAnimatorSectionInterface
    {
        protected Aircraft _aircraft;
        protected Trajectory _trajectory;

        public AircraftKMLAnimator(Aircraft aircraft, Trajectory trajectory)
        {
            _aircraft = aircraft;
            _trajectory = trajectory;
        }

        public string KMLSetup()
        {
            GeoPoint3D startingPoint = _trajectory.GeoPoint(0);

            return @"
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
            ";
        }

        public string KMLAnimationStep(int t)
        {
            GeoPoint3D animationPoint = _trajectory.GeoPoint(t);

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
            ";
        }

        public string KMLFinish()
        {
            return "";
        }
    }
}
