
using AircraftTrajectories.Models.Optimisation;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Optimalisation
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

        [Test]
        public void NPDGetNoiseKnownTest()
        {
            NoisePowerDistance npd = NoisePowerDistance.Instance;
            double noise = npd.GetNoiseValue("2CF650", 'E', 'A', 400, 10000);
            Assert.AreEqual(101.1, noise, 0.01);
        }

        [Test]
        public void NPDGetNoiseVerticalInterpolatedTest()
        {
            NoisePowerDistance npd = NoisePowerDistance.Instance;
            double noise = npd.GetNoiseValue("2JT8D", 'M', 'D', 1000, 11000);
            Assert.AreEqual(97, noise, 0.01);
        }

        [Test]
        public void NPDGetNoiseHorizontalInterpolatedTest()
        {
            NoisePowerDistance npd = NoisePowerDistance.Instance;
            double noise = npd.GetNoiseValue("2CF680", 'E', 'A', 1500, 7000);
            Assert.AreEqual(87.21, noise, 0.01);
        }

        [Test]
        public void NPDGetNoiseBilinearInterpolatedTest()
        {
            NoisePowerDistance npd = NoisePowerDistance.Instance;
            double noise = npd.GetNoiseValue("2CF680", 'E', 'A', 1500, 9500);
            Assert.AreEqual(88.07, noise, 0.01);
        }
    }
}
