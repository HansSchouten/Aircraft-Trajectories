using System.Collections.Generic;
using System.Text;

namespace AircraftTrajectories.Models.Visualisation.KML
{
    public class KMLAnimator
    {
        protected List<KMLAnimatorInterface> _animators;

        /// <summary>
        /// Construct a KMLAnimator object
        /// </summary>
        /// <param name="animators">A list of objects all implementing KMLAnimatorInterface</param>
        public KMLAnimator(List<KMLAnimatorInterface> animators)
        {
            _animators = animators;
        }

        protected string CreateAnimationKML(int duration)
        {
            StringBuilder builder = new StringBuilder();
            
            // Add each kml section definition of this animation
            foreach (KMLAnimatorInterface kmlAnimator in _animators)
            {
                builder.Append(kmlAnimator.KMLSetup());
            }
            
            // Add all kml updates of each animation section
            for (int t = 0; t < duration; t++)
            {
                foreach (KMLAnimatorInterface kmlAnimator in _animators)
                {
                    builder.Append(kmlAnimator.KMLAnimationStep(t));
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generate a file containing the animation in kml format
        /// </summary>
        /// <param name="duration">the duration of the animation</param>
        /// <param name="filepath">the filepath at which this animation needs to be stored</param>
        public void AnimationToFile(int duration, string filepath)
        {
            string animationKMLString = CreateAnimationKML(duration);
            System.IO.StreamWriter file = new System.IO.StreamWriter(filepath);
            file.Write(animationKMLString);
            file.Close();
        }
    }
}
