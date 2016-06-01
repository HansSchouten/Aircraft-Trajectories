
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
            Assert.AreEqual(15, isa.T, 0.01);
            Assert.AreEqual(101325, isa.P);
            Assert.AreEqual(1.22500, isa.Rho);
            Assert.AreEqual(340.294, isa.VSound);
        }

        [Test]
        public void ISA1000ftTest()
        {
            var isa = new ISA(1000);
            Assert.AreEqual(13.0188, isa.T, 0.01);
            Assert.AreEqual(97716.6, isa.P);
            Assert.AreEqual(1.11164, isa.Rho);
            Assert.AreEqual(336.434, isa.VSound);
        }
    }
}
