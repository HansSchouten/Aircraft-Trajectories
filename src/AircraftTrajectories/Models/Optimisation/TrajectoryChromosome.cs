using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;

namespace AircraftTrajectories.Models.Optimisation
{
    public class TrajectoryChromosome : ChromosomeBase
    {
        public int NumberOfSegments { get; protected set; }

        public TrajectoryChromosome(int numberOfGenes, int numberOfSegments) : base(numberOfGenes)
        {
            NumberOfSegments = numberOfSegments;
            for (int i = 0; i < Length; i++) {
                ReplaceGene(i, GenerateGene(i));
            }
        }

        public override IChromosome CreateNew()
        {
            return new TrajectoryChromosome(Length, NumberOfSegments);
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