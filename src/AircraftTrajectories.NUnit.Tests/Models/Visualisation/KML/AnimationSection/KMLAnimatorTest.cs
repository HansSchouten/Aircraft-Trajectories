using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Models.Visualisation.KML;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
using NUnit.Framework;
using System.Collections.Generic;

namespace AircraftTrajectories.NUnit.Tests.Models.Visualisation.KML.AnimationSection
{
    using AircraftTrajectories.Models.IntegratedNoiseModel;
    using AircraftTrajectories.Models.TemporalGrid;
    using AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras;
    using System;
    using System.IO;
    using System.Xml;
    using Views;

    [TestFixture]
    class KMLAnimatorTest
    {
        protected bool INMCompleted = false;

        [Test]
        public void KMLAnimatorConstructorTest()
        {
            var animator = new KMLAnimator(new List<KMLAnimatorSectionInterface>(), null);
            Assert.IsNotNull(animator);
        }

        [Test]
        public void KMLAnimatorFullTest()
        {
            if(File.Exists(Globals.currentDirectory + "test.kml"))
            {
                File.Delete(Globals.currentDirectory + "test.kml");
            }

            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var aircraft = new Aircraft("GP7270", "wing");
            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(calculationCompleted);
            while (!INMCompleted) { }

            TemporalGrid temporalGrid = noiseModel.TemporalGrid;

            var population = new List<double[]> {
                new double[3]{104064, 475499, 2.5},
                new double[3]{104262, 476470, 3}
            };

            var camera = new FollowKMLAnimatorCamera(aircraft, trajectory);
            var sections = new List<KMLAnimatorSectionInterface>() {
                new AircraftKMLAnimator(aircraft, trajectory),
                new AirplotKMLAnimator(trajectory),
                new GroundplotKMLAnimator(trajectory),
                new ContourKMLAnimator(temporalGrid, trajectory, new List<int>() { 65, 70, 75 }),
                new AnnoyanceKMLAnimator(temporalGrid, population)
            };
            var animator = new KMLAnimator(sections, camera);
            animator.AnimationToFile(trajectory.Duration, Globals.currentDirectory + "test.kml");

            String output = File.ReadAllText(Globals.currentDirectory + "test.kml");

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(output);
            }
            catch (XmlException ex)
            {
                Assert.Fail(ex.Message);
            }

            INMCompleted = false;
        }

        protected void calculationCompleted()
        {
            INMCompleted = true;
        }

    }
}
