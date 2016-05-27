using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Visualisation.AnimationSection
{
    using AircraftTrajectories.Models.Space3D;
    using AircraftTrajectories.Models.Trajectory;
    using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
    using System.Xml;

    [TestFixture]
    public class AirplotKMLAnimatorTest
    {
        [Test]
        public void AirplotKMLAnimatorConstructorTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var animator = new AirplotKMLAnimator(trajectory);
            Assert.IsNotNull(animator);
        }

        [Test]
        public void AirplotKMLAnimatorSetupTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var animator = new AirplotKMLAnimator(trajectory);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<root>" + animator.KMLSetup() + "</root>");
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void AirplotKMLAnimatorStepTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var animator = new AirplotKMLAnimator(trajectory);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<root>" + animator.KMLAnimationStep(0) + "</root>");
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void AirplotKMLAnimatorFinishTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var animator = new AirplotKMLAnimator(trajectory);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<root>" + animator.KMLFinish() + "</root>");
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
