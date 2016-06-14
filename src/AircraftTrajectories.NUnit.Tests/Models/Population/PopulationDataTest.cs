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

        [Test]
        public void PopulationChanceTest()
        {
            var populationData = new PopulationData(Globals.testdataDirectory + "population.dat");
            populationData.Chance = 1;

            Assert.AreEqual(1, populationData.Chance);
        }

        [Test]
        public void PopulationCapacityTest()
        {
            var populationData = new PopulationData(Globals.testdataDirectory + "population.dat");
            populationData.Chance = 1;

            Assert.AreEqual(4, populationData.getPopulationData().Capacity);
        }

    }
}