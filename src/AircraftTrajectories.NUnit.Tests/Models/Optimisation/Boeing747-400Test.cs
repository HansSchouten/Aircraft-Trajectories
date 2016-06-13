using AircraftTrajectories.Models.Optimisation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftTrajectories.NUnit.Tests.Models.Optimisation
{
    [TestFixture]
    class Boeing747_400Test
    {
        [Test]
        public void VCleanTest()
        {
            var aircraft = new Boeing747_400();
            Assert.AreEqual(280, aircraft.VClean);
        }

        [Test]
        public void DragPolarTest()
        {
            var aircraft = new Boeing747_400();
            aircraft.Drag(4500, 50000);
            Assert.AreEqual(280, aircraft.VClean);
        }

        [Test]
        public void TakeOffThrustTest()
        {
            var aircraft = new Boeing747_400();
            double thrust = aircraft.TakeOffThrust(550, 50000);

            Assert.AreEqual(-47361, thrust, 100);
        }

        [Test]
        public void ClimbThrustTest()
        {
            var aircraft = new Boeing747_400();
            double thrust = aircraft.ClimbThrust(5450, 234000);

            Assert.IsNaN(thrust);
        }

        [Test]
        public void FuelFlowTest()
        {
            var aircraft = new Boeing747_400();
            double fuel = aircraft.FuelFLow(1200, 500, 50000);

            Assert.AreEqual(0.1393, fuel, 1);
        }

        [Test]
        public void LiftCoefficientTest()
        {
            var aircraft = new Boeing747_400();
            double lift = aircraft.MinimumTurnRadius(15000);

            Assert.AreEqual(1571038, lift, 100);
        }
    }
}
