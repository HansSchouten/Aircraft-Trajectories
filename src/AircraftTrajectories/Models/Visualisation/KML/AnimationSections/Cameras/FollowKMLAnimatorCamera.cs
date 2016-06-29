
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras
{
    using Space3D;
    using Trajectory;

    public class FollowKMLAnimatorCamera : KMLAnimatorCameraInterface
    {
        protected Aircraft _aircraft;
        protected Trajectory _trajectory;

        public FollowKMLAnimatorCamera(Aircraft aircraft, Trajectory trajectory)
        {
            _aircraft = aircraft;
            _trajectory = trajectory;
        }

        public string KMLSetup()
        {
            var cameraHeading = (_trajectory.Heading(0) - 40) % 360;
            GeoPoint3D aircraftLocation = _trajectory.GeoPoint(0);
            GeoPoint3D cameraLocation = aircraftLocation.MoveInDirection(-1600, cameraHeading);

            return @"
    <LookAt>
        <latitude>" + cameraLocation.Latitude + @"</latitude>
        <longitude>" + cameraLocation.Longitude + @"</longitude>
        <altitude>" + (cameraLocation.Z + 350) + @"</altitude>
        <altitudeMode>absolute</altitudeMode>
        <heading>" + cameraHeading + @"</heading>
        <tilt>" + 75 + @"</tilt>
    </LookAt>
            ";
        }

        public string KMLAnimationStep(int t)
        {
            var cameraHeading = (_trajectory.Heading(t) - 40) % 360;
            GeoPoint3D aircraftLocation = _trajectory.GeoPoint(t);
            GeoPoint3D cameraLocation = aircraftLocation.MoveInDirection(-1600, cameraHeading);

            if (t == 0)
            {
                return @"
<gx:FlyTo>
    <gx:duration>1.0</gx:duration>
    <LookAt>
        <latitude>" + cameraLocation.Latitude + @"</latitude>
        <longitude>" + cameraLocation.Longitude + @"</longitude>
        <altitude>" + (cameraLocation.Z + 350) + @"</altitude>
        <altitudeMode>absolute</altitudeMode>
        <heading>" + cameraHeading + @"</heading>
        <tilt>" + 75 + @"</tilt>
    </LookAt>
</gx:FlyTo>
                ";
            }

            return @"
<gx:FlyTo>
    <gx:duration>1.0</gx:duration>
    <gx:flyToMode>smooth</gx:flyToMode>
    <LookAt>
        <latitude>" + cameraLocation.Latitude + @"</latitude>
        <longitude>" + cameraLocation.Longitude + @"</longitude>
        <altitude>" + (cameraLocation.Z + 350) + @"</altitude>
        <altitudeMode>absolute</altitudeMode>
        <heading>" + cameraHeading + @"</heading>
        <tilt>" + 75 + @"</tilt>
    </LookAt>
</gx:FlyTo>
            ";
        }

        public string KMLFinish()
        {
            return "";
        }
    }
}
