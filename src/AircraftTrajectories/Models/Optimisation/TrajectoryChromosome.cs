using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;

namespace AircraftTrajectories.Models.Optimisation
{
    public class TrajectoryChromosome : ChromosomeBase
    {
        protected int _numberOfControlPoints;

        public TrajectoryChromosome(int length) : base(length)
        {
            _numberOfControlPoints = length;
            for (int i = 0; i < length; i++) {
                ReplaceGene(i, GenerateGene(i));
            }
        }

        public override IChromosome CreateNew()
        {
            return new TrajectoryChromosome(_numberOfControlPoints);
        }

        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(RandomizationProvider.Current.GetDouble());
        }
    }
}