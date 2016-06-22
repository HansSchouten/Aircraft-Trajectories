using AircraftTrajectories.Models.Space3D;
using System;

namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    public class CustomMapKMLAnimator : KMLAnimatorSectionInterface
    {
        protected string _mapFile;
        protected GeoPoint3D _bottomLeft;
        protected GeoPoint3D _upperRight;

        public CustomMapKMLAnimator(string mapFile, GeoPoint3D bottomLeft, GeoPoint3D upperRight)
        {
            _mapFile = mapFile;
            _bottomLeft = bottomLeft;
            _upperRight = upperRight;
        }

        public string KMLSetup()
        {
            return @"
<GroundOverlay>
    <name>Custom map</name>
    <Icon>
        <href>" + _mapFile + @"</href>
    </Icon>
    <LatLonBox>
        <north>" + _upperRight.Latitude + @"</north>
        <south>" + _bottomLeft.Latitude + @"</south>
        <east>" + _upperRight.Longitude + @"</east>
        <west>" + _bottomLeft.Longitude + @"</west>
    </LatLonBox>
</GroundOverlay>
            ";
        }

        public string KMLAnimationStep(int t)
        {
            return "";
        }

        public string KMLFinish()
        {
            return "";
        }
    }
}
