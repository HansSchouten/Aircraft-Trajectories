using AircraftTrajectories.Models.Trajectory;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Trajectory
{
    [TestFixture]
    public class AircraftTest
    {
        [Test]
        public void Aircraft()
        {
            Aircraft aircraft = new Aircraft("2CF650", "m");
            Assert.IsNotNull(aircraft);
        }

        [Test]
        public void AircraftModel()
        {
            Aircraft aircraft = new Aircraft("2CF650", "m");

            Assert.AreEqual(aircraft.EngineId, "2CF650");
            Assert.AreEqual(aircraft.EngineMount, "m");
        }

        [Test]
        public void AircraftEgineMount()
        {
            Aircraft aircraft = new Aircraft("2CF650", "m");

            Assert.AreEqual(aircraft.EngineMount, "m");
        }

    }
}
