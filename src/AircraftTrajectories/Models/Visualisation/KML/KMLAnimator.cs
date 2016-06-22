using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
using System.Collections.Generic;
using System.Text;

namespace AircraftTrajectories.Models.Visualisation.KML
{
    using AnimationSections.Cameras;

    public class KMLAnimator
    {
        protected List<KMLAnimatorSectionInterface> _animators;
        protected KMLAnimatorCameraInterface _camera;
        public int Duration = 1;

        /// <summary>
        /// Construct a KMLAnimator object
        /// </summary>
        /// <param name="animators">A list of objects all implementing KMLAnimatorInterface</param>
        public KMLAnimator(List<KMLAnimatorSectionInterface> animators, KMLAnimatorCameraInterface camera)
        {
            _animators = animators;
            _camera = camera;
        }

        /// <summary>
        /// Creates the actual KML file by starting an animation playlist
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        protected string CreateAnimationKML(int duration)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\">");
            builder.AppendLine("<Document>");

            // Add each kml section definition of this animation
            foreach (KMLAnimatorSectionInterface kmlAnimator in _animators)
            {
                builder.Append(kmlAnimator.KMLSetup());
            }

            // Add all kml updates of each animation section
            builder.AppendLine("<gx:Tour><name>Flight animation</name><gx:Playlist>");
            for (int t = 0; t < duration; t++)
            {
                builder.Append(@"
<gx:AnimatedUpdate>
    <gx:duration>" + Duration + @"</gx:duration>
    <Update>
        <Change>");
                foreach (KMLAnimatorSectionInterface kmlAnimator in _animators)
                {
                    builder.Append(kmlAnimator.KMLAnimationStep(t));
                }
                builder.Append(@"
        </Change>
    </Update>
</gx:AnimatedUpdate>");
                builder.Append(_camera.KMLAnimationStep(t));
            }
            builder.AppendLine("</gx:Playlist></gx:Tour>");

            // Add the post animation definitions
            foreach (KMLAnimatorSectionInterface kmlAnimator in _animators)
            {
                builder.Append(kmlAnimator.KMLFinish());
            }

            builder.AppendLine("</Document>");
            builder.AppendLine("</kml>");
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
