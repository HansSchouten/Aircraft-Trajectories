using System;

namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras
{
    using Space3D;
    using Trajectory;

    public class TopViewKMLAnimatorCamera : KMLAnimatorCameraInterface
    {
        protected Aircraft _aircraft;
        protected Trajectory _trajectory;

        public TopViewKMLAnimatorCamera(Aircraft aircraft, Trajectory trajectory)
        {
            _aircraft = aircraft;
            _trajectory = trajectory;
        }

        public string KMLSetup()
        {
            return "";
        }

        public string KMLAnimationStep(int t)
        {
            GeoPoint3D location = _trajectory.GeoPoint(_trajectory.Duration / 2);
            return @"
<gx:FlyTo>
    <gx:duration>1.0</gx:duration>
    <LookAt>
        <latitude>" + location.Latitude + @"</latitude>
        <longitude>" + location.Longitude + @"</longitude>
        <altitude>" + 15000 + @"</altitude>
        <altitudeMode>absolute</altitudeMode>
        <heading>0</heading>
        <tilt>0</tilt>
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
