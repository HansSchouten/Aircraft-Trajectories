
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{

    public class LegendKMLAnimator : KMLAnimatorSectionInterface
    {

        public LegendKMLAnimator() { }

        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the plotted ground path
        /// </summary>
        /// <returns></returns>
        public string KMLSetup()
        {
            return @"
            <ScreenOverlay>
            <name>Legenda</name>
            <Icon> <href>gradientImage.png</href></Icon>
            <overlayXY x= ""0.03"" y= ""0.85"" xunits= ""fraction"" yunits= ""fraction"" />
            <screenXY x = ""0.03"" y =""0.85"" xunits =""fraction"" yunits =""fraction""/>
            <rotationXY x = ""0.5"" y = ""0.5"" xunits =""fraction"" yunits =""fraction""/>
            <size x = ""0"" y = ""0"" xunits = ""pixels"" yunits = ""pixels"" />
            </ScreenOverlay >

            <ScreenOverlay >
            <name> Legenda Title </name >
            <Icon> <href> titleImage.png </href>
            </Icon>
            <overlayXY x = ""0.02"" y = ""0.94"" xunits = ""fraction"" yunits = ""fraction"" />
            <screenXY x = ""0.02"" y = ""0.94"" xunits = ""fraction"" yunits = ""fraction"" />
            <rotationXY x = ""0.5"" y = ""0.5"" xunits = ""fraction"" yunits = ""fraction"" />
            <size x = ""0"" y = ""0"" xunits = ""pixels"" yunits = ""pixels"" />
            </ScreenOverlay >
            ";
        }

        /// <summary>
        /// Return a string in KML format containing all updates
        /// for the plotted ground path at the given moment in time
        /// </summary>
        /// <returns></returns>
        public string KMLAnimationStep(int t) { 

            return "";
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
