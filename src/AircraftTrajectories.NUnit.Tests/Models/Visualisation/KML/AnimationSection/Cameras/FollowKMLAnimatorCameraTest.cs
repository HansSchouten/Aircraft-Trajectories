using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Visualisation.KML.AnimationSection.Cameras
{
    using AircraftTrajectories.Models.Space3D;
    using AircraftTrajectories.Models.Trajectory;
    using AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras;
    using System.Xml;

    [TestFixture]
    public class FollowKMLAnimatorCameraTest
    {
        protected string kmlRoot = "<kml xmlns='http://www.opengis.net/kml/2.2' xmlns:gx='http://www.google.com/kml/ext/2.2' xmlns:kml='http://www.opengis.net/kml/2.2' xmlns:atom='http://www.w3.org/2005/Atom'>";
        protected string kmlRootClose = "</kml>";

        [Test]
        public void FollowKMLAnimatorCameraConstructorTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");
            var camera = new FollowKMLAnimatorCamera(aircraft, trajectory);
            Assert.IsNotNull(camera);
        }

        [Test]
        public void AicraftKMLAnimatorSetupTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");
            var camera = new FollowKMLAnimatorCamera(aircraft, trajectory);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(kmlRoot + camera.KMLSetup() + kmlRootClose);
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void AicraftKMLAnimatorStepTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");
            var camera = new FollowKMLAnimatorCamera(aircraft, trajectory);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(kmlRoot + camera.KMLAnimationStep(0) + kmlRootClose);
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void AicraftKMLAnimatorFinishTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");
            var camera = new FollowKMLAnimatorCamera(aircraft, trajectory);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(kmlRoot + camera.KMLFinish() + kmlRootClose);
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}