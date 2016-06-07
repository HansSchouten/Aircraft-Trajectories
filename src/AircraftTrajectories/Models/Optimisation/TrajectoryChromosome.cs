using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;

namespace AircraftTrajectories.Models.Optimisation
{
    public class TrajectoryChromosome : ChromosomeBase
    {
        protected int _numberOfGenes;
        protected int _numberOfSegments;

        public TrajectoryChromosome(int length, int numberOfSegments) : base(length)
        {
            _numberOfGenes = length;
            _numberOfSegments = numberOfSegments;
            for (int i = 0; i < length; i++) {
                ReplaceGene(i, GenerateGene(i));
            }
        }

        public override IChromosome CreateNew()
        {
            return new TrajectoryChromosome(_numberOfGenes, _numberOfSegments);
        }

        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(RandomizationProvider.Current.GetDouble());
        }

        public static int ChromosomeLength(int numberOfSegments)
        {
            return (int) (Math.Floor(numberOfSegments / 2.0) * 4 + Math.Ceiling(numberOfSegments / 2.0) * 3);
        }

        public static int SegmentSettingIndex(int segmentIndex, int setting)
        {
            return (int)(Math.Floor((segmentIndex - 1) / 2.0) * 4 + Math.Ceiling((segmentIndex - 1) / 2.0) * 3 + setting);
        }
    }
}