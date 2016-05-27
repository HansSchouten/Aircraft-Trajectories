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
    public class AnnoyanceKMLAnimatorTest
    {
        protected bool completed = false;

        [Test]
        public void AnnoyanceKMLAnimatorConstructorTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");

            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            var population = new List<double[]> { 
                new double[3]{100, 200, 2.5},
                new double[3]{110, 210, 3}
            };

            var animator = new AnnoyanceKMLAnimator(noiseModel.TemporalGrid, population);
            Assert.IsNotNull(animator);

            completed = false;
        }


        [Test]
        public void AicraftKMLAnimatorSetupTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");

            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            var population = new List<double[]> {
                new double[3]{100, 200, 2.5},
                new double[3]{110, 210, 3}
            };

            var animator = new AnnoyanceKMLAnimator(noiseModel.TemporalGrid, population);

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
        public void AicraftKMLAnimatorStepTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");

            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            var population = new List<double[]> {
                new double[3]{100, 200, 2.5},
                new double[3]{110, 210, 3}
            };

            var animator = new AnnoyanceKMLAnimator(noiseModel.TemporalGrid, population);

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
        public void AicraftKMLAnimatorFinishTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");
            var aircraft = new Aircraft("GP7270", "wing");

            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            var population = new List<double[]> {
                new double[3]{100, 200, 2.5},
                new double[3]{110, 210, 3}
            };

            var animator = new AnnoyanceKMLAnimator(noiseModel.TemporalGrid, population);

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