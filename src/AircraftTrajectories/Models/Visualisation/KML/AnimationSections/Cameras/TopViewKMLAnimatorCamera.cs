
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras
{
    using Space3D;

    public class TopViewKMLAnimatorCamera : KMLAnimatorCameraInterface
    {
        protected GeoPoint3D _cameraLocation { get; set; }

        public TopViewKMLAnimatorCamera(GeoPoint3D cameraLocation = null)
        {
            _cameraLocation = cameraLocation;
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
