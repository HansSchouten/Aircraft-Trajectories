using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Visualisation.KML.AnimationSection
{
    using AircraftTrajectories.Models.Space3D;
    using AircraftTrajectories.Models.Trajectory;
    using AircraftTrajectories.Models.IntegratedNoiseModel;
    using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
    using System.Collections.Generic;
    using System.Xml;

    [TestFixture]
    public class ContourKMLAnimatorTest
    {
        protected bool completed = false;

        [Test]
        public void ContourKMLAnimatorConstructorTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");

            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            var labeledContours = new List<int> {
                65, 75, 80
            };

            var animator = new ContourKMLAnimator(noiseModel.TemporalGrid, trajectory, labeledContours);
            Assert.IsNotNull(animator);

            completed = false;
        }


        [Test]
        public void ContourKMLAnimatorSetupTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");

            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            var labeledContours = new List<int> {
                65, 75, 80
            };

            var animator = new ContourKMLAnimator(noiseModel.TemporalGrid, trajectory, labeledContours);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<root>" + animator.KMLSetup() + "</root>");
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }

            completed = false;
        }

        [Test]
        public void ContourKMLAnimatorStepTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");

            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            var labeledContours = new List<int> {
                65, 75, 80
            };

            var animator = new ContourKMLAnimator(noiseModel.TemporalGrid, trajectory, labeledContours);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<root>" + animator.KMLAnimationStep(0) + "</root>");
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }

            completed = false;
        }

        [Test]
        public void ContourKMLAnimatorFinishTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");

            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            var labeledContours = new List<int> {
                65, 75, 80
            };

            var animator = new ContourKMLAnimator(noiseModel.TemporalGrid, trajectory, labeledContours);

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<root>" + animator.KMLFinish() + "</root>");
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }

            completed = false;
        }

        protected void INMCompleted()
        {
            completed = true;
        }
    }
}