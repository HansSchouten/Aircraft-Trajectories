using System;

namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras
{
    using Space3D;
    using Trajectory;

    public class TopViewKMLAnimatorCamera : KMLAnimatorCameraInterface
    {
        protected Aircraft _aircraft;
        protected Trajectory _trajectory;
        protected GeoPoint3D _cameraLocation { get; set; }

        public TopViewKMLAnimatorCamera(Aircraft aircraft, Trajectory trajectory, GeoPoint3D cameraLocation = null)
        {
            _aircraft = aircraft;
            _trajectory = trajectory;
            _cameraLocation = cameraLocation;
            if (cameraLocation == null)
            {
                _cameraLocation = _trajectory.GeoPoint(_trajectory.Duration / 2);
                _cameraLocation.Z = 15000;
            }
        }

        public string KMLSetup()
        {
            return "";
        }

        public string KMLAnimationStep(int t)
        {
            return @"
<gx:FlyTo>
    <gx:duration>1.0</gx:duration>
    <LookAt>
        <latitude>" + _cameraLocation.Latitude + @"</latitude>
        <longitude>" + _cameraLocation.Longitude + @"</longitude>
        <altitude>" + _cameraLocation.Z + @"</altitude>
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
