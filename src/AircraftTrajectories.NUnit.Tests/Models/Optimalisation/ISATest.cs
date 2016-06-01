
using AircraftTrajectories.Models.Optimalisation;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Optimalisation
{
    [TestFixture]
    class ISATest
    {
        [Test]
        public void ISA0ftTest()
        {
            var isa = new ISA(0);
            Assert.AreEqual(288.15, isa.T, 0.01);
            Assert.AreEqual(101325, isa.P, 0.01);
            Assert.AreEqual(1.22500, isa.Rho, 0.01);
            Assert.AreEqual(340.294, isa.VSound, 0.01);
        }

        [Test]
        public void ISA1000ftTest()
        {
            var isa = new ISA(1000);
            Assert.AreEqual(286.1688, isa.T, 0.01);
            Assert.AreEqual(97716.63, isa.P, 0.01);
            Assert.AreEqual(1.18955, isa.Rho, 0.01);
            Assert.AreEqual(339.122, isa.VSound, 0.01);
        }
    }
}
