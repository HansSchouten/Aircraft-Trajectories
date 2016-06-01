using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

namespace AircraftTrajectories.Models.Optimalisation
{
    public class TrajectoryChromosome : ChromosomeBase
    {
        private int _numberOfPositions;

        public TrajectoryChromosome(int length) : base(length)
        {
            _numberOfPositions = length;
            var citiesIndexes = RandomizationProvider.Current.GetUniqueInts(length, 0, length);

            for (int i = 0; i < length; i++) {
                ReplaceGene(i, new Gene(citiesIndexes[i]));
            }
        }

        public override IChromosome CreateNew()
        {
            return new TrajectoryChromosome(_numberOfPositions);
        }

        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(RandomizationProvider.Current.GetInt(0, _numberOfPositions));
        }
    }
}