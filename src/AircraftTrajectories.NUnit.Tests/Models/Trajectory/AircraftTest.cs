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
            Aircraft obj = new Aircraft("2CF650", "m");
            Assert.IsNotNull(obj);
        }

        [Test]
        public void AircraftModel()
        {
            Aircraft obj = new Aircraft("2CF650", "m");
            Assert.AreEqual(obj.EngineId, "2CF650");
            Assert.AreEqual(obj.EngineMount, "m");
        }

    }
}
