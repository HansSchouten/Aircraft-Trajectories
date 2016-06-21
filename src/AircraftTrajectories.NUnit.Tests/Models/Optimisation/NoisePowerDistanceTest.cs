
using AircraftTrajectories.Models.Optimisation;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Optimisation
{
    [TestFixture]
    class NoisePowerDistanceTest
    {
        [Test]
        public void NPDReadDataTest()
        {
            Assert.DoesNotThrow(() => {
                NoisePowerDistance instance = NoisePowerDistance.Instance;
            });
        }
    }
}
