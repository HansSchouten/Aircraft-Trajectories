namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    public interface KMLAnimatorSectionInterface
    {
        /// <summary>
        /// Return a string in KML format containing all pre animation definitions 
        /// that are required fot this animation section
        /// </summary>
        /// <returns>a string in KML format</returns>
        string KMLSetup();

        /// <summary>
        /// Return a string in KML format containing all updates
        /// for this animation section at the given moment in time
        /// </summary>
        /// <returns>a string in KML format</returns>
        string KMLAnimationStep(int t);

        /// <summary>
        /// Return a string in KML format containing all after animation definitions 
        /// that are required fot this animation section
        /// </summary>
        /// <returns>a string in KML format</returns>
        string KMLFinish();
    }
}
