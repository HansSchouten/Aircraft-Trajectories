
namespace AircraftTrajectories.Models.Visualisation.KML
{
    public interface KMLAnimatorInterface
    {
        /// <summary>
        /// Return a string in KML format containing all definitions 
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
    }
}
