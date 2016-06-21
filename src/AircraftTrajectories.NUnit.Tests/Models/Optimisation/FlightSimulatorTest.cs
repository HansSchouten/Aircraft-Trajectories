using AircraftTrajectories.Models.Optimisation;
using AircraftTrajectories.Models.Space3D;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AircraftTrajectories.NUnit.Tests.Models.Optimisation
{
    [TestFixture]
    class FlightSimulatorTest
    {
        [Test]
        public void AngleDifferenceTest()
        {
            var aircraft = new Boeing747_400();
            FlightSimulator sim = new FlightSimulator(aircraft, null, 0, null);

            Assert.AreEqual(R(45), sim.AngleDifference(R(45), R(90)), 0.001);

            Assert.AreEqual(R(100), sim.AngleDifference(R(350), R(90)), 0.001);
            Assert.AreEqual(R(260), sim.AngleDifference(R(90), R(350)), 0.001);

            Assert.AreEqual(R(190), sim.AngleDifference(R(90), R(280)), 0.001);
            Assert.AreEqual(R(170), sim.AngleDifference(R(280), R(90)), 0.001);

            Assert.AreEqual(R(180), sim.AngleDifference(R(360), R(180)), 0.001);
            Assert.AreEqual(R(180), sim.AngleDifference(R(0), R(180)), 0.001);

            Assert.AreEqual(R(91), sim.AngleDifference(R(359), R(90)), 0.001);
            Assert.AreEqual(R(200), sim.AngleDifference(R(250), R(90)), 0.001);
            Assert.AreEqual(R(190), sim.AngleDifference(R(180), R(10)), 0.001);
            Assert.AreEqual(R(359), sim.AngleDifference(R(90), R(89)), 0.001);
            Assert.AreEqual(R(271), sim.AngleDifference(R(179), R(90)), 0.001);
        }
        private double R(double degrees)
        {
            return degrees * Math.PI / 180;
        }
        
        [Test]
        public void FlightSimulatorFlyStraightTest()
        {
            var aircraft = new Boeing747_400();
            var settings = new List<double>() { 1, 1, 1 };
            FlightSimulator sim = new FlightSimulator(aircraft, new Point3D(18000, 0, 0, CoordinateUnit.metric), 1, settings);
            sim.Simulate();

            Assert.AreEqual(18000, sim._x, 150);
            Assert.AreEqual(0, sim._y, 0.001);
        }

        [Test]
        public void FlightSimulatorFlyFarStraightTest()
        {
            var aircraft = new Boeing747_400();
            var settings = new List<double>() { 1, 1, 1 };
            FlightSimulator sim = new FlightSimulator(aircraft, new Point3D(50000, 0, 0, CoordinateUnit.metric), 1, settings);
            sim.Simulate();

            Assert.AreEqual(50000, sim._x, 150);
            Assert.AreEqual(0, sim._y, 0.001);
        }

        [Test]
        public void FlightSimulatorBasic3SegmentLeftTurnTest()
        {
            var aircraft = new Boeing747_400();
            var settings = new List<double>() { 1,1,0.92,  1,1,0.9,0,  1,1,1 };
            FlightSimulator sim = new FlightSimulator(aircraft, new Point3D(50000, 50000, 0, CoordinateUnit.metric), 3, settings);
            sim.Simulate();

            Assert.AreEqual(50000, sim._x, 150);
            Assert.AreEqual(50000, sim._y, 150);
        }

        [Test]
        public void FlightSimulatorBasic3SegmentRightTurnTest()
        {
            var aircraft = new Boeing747_400();
            var settings = new List<double>() { 1,1,1,0, 1,1,1,1, 1,1,1,0 };
            FlightSimulator sim = new FlightSimulator(aircraft, new Point3D(50000, 50000, 0, CoordinateUnit.metric), 3, settings);
            sim.Simulate();

            Assert.AreEqual(50000, sim._x, 150);
            Assert.AreEqual(50000, sim._y, 150);
        }

        [Test]
        public void FlightSimulator3SegmentTestContinueStraight()
        {
            var aircraft = new Boeing747_400();
            var settings = new List<double>() { 1,1,1,0, 1,1,1,1, 1,1,1,0 };
            FlightSimulator sim = new FlightSimulator(aircraft, new Point3D(10000, 0, 0, CoordinateUnit.metric), 3, settings);
            sim.Simulate();

            Assert.AreEqual(10000, sim._x, 150);
            Assert.AreEqual(0, sim._y, 150);
        }
    }
}