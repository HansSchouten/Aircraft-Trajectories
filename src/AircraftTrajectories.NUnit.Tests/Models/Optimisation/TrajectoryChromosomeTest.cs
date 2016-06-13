using NUnit.Framework;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System.Collections.Generic;
using AircraftTrajectories.Models.Optimisation;


namespace AircraftTrajectories.Models.Optimisation
{
    [TestFixture]

    class TrajectoryChromosomeTest
    {
        [Test]
        public void NumberOfSegmentsTest()
        {
            TrajectoryChromosome chromosome = new TrajectoryChromosome(3, 4);

            Assert.AreEqual(4, chromosome.NumberOfSegments);
        }

        [Test]
        public void GenerateChromsomeTest()
        {
            TrajectoryChromosome chromosome = new TrajectoryChromosome(3, 4);
            chromosome.CreateNew();
            Assert.AreEqual(3, chromosome.Length);
            Assert.AreEqual(4, chromosome.NumberOfSegments);
        }

        [Test]
        public void GenerateGeneTest()
        {
            TrajectoryChromosome chromosome = new TrajectoryChromosome(3, 4);
            chromosome.GenerateGene(2);

            Assert.IsNotNull(chromosome.GetGene(2));
        }

        [Test]
        public void ChromosomeLengthTest()
        {
            TrajectoryChromosome chromosome = new TrajectoryChromosome(3, 4);
            Assert.AreEqual(17, Optimisation.TrajectoryChromosome.ChromosomeLength(5));
        }
    }
}
