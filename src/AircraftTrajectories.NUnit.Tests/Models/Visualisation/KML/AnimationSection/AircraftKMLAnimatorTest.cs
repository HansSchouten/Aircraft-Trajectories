using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Visualisation.AnimationSection
{
    using AircraftTrajectories.Models.Space3D;
    using AircraftTrajectories.Models.Trajectory;
    using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
    using System.Xml;

    [TestFixture]
    public class AircraftKMLAnimatorTest
    {
        [Test]
        public void AircraftKMLAnimatorConstructorTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");
            var animator = new AircraftKMLAnimator(aircraft, trajectory);
            Assert.IsNotNull(animator);
        }

        [Test]
        public void AircraftKMLAnimatorSetupTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");
            var animator = new AircraftKMLAnimator(aircraft, trajectory);

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
        public void AircraftKMLAnimatorStepTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");
            var animator = new AircraftKMLAnimator(aircraft, trajectory);

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
        public void AircraftKMLAnimatorFinishTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");
            var animator = new AircraftKMLAnimator(aircraft, trajectory);

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