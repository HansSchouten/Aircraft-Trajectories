using NUnit.Framework;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System.Collections.Generic;
using AircraftTrajectories.Models.Optimisation;


namespace AircraftTrajectories.NUnit.Tests.Models.Optimisation
{
    [TestFixture]

    class TrajectoryFitnessTest
    {
        [Test]
        public void FitnessTest()
        {
            TrajectoryFitness fitness = new TrajectoryFitness();
            TrajectoryChromosome chromosome = new TrajectoryChromosome(10, 3);
            double fuel = fitness.Evaluate(chromosome);

            Assert.AreEqual(24537, fuel, 10000);
        }
    }
}
