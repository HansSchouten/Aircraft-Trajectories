using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Visualisation.KML.AnimationSection
{
    using AircraftTrajectories.Models.Space3D;
    using AircraftTrajectories.Models.Trajectory;
    using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
    using System.Xml;

    [TestFixture]
    public class GroundPlotKMLAnimatorTest
    {
        protected string kmlRoot = "<kml xmlns='http://www.opengis.net/kml/2.2' xmlns:gx='http://www.google.com/kml/ext/2.2' xmlns:kml='http://www.opengis.net/kml/2.2' xmlns:atom='http://www.w3.org/2005/Atom'>";
        protected string kmlRootClose = "</kml>";
        protected bool completed = false;

        [Test]
        public void GroundplotKMLAnimatorConstructorTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var animator = new GroundplotKMLAnimator(trajectory);
            Assert.IsNotNull(animator);
        }


        [Test]
        public void GroundplotKMLAnimatorSetupTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var animator = new GroundplotKMLAnimator(trajectory);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(kmlRoot + animator.KMLSetup() + kmlRootClose);
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GroundplotKMLAnimatorStepTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var animator = new GroundplotKMLAnimator(trajectory);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(kmlRoot + animator.KMLAnimationStep(0) + kmlRootClose);
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void GroundplotKMLAnimatorFinishTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var animator = new GroundplotKMLAnimator(trajectory);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(kmlRoot + animator.KMLFinish() + kmlRootClose);
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}