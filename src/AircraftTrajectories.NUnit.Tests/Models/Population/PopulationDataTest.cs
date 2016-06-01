using AircraftTrajectories.Models.Population;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.Models.Population
{

    [TestFixture]
    public class PopulationDataTest
    {
        [Test]
        public void GetPopulationDataTest()
        {
            var populationData = new PopulationData(Globals.testdataDirectory + "population.dat");
            populationData.Chance = 1;
            var data = populationData.getPopulationData();
            Assert.AreEqual(2, data.Count);
        }
    }
}