using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;

namespace AircraftTrajectories.Models.Optimisation
{
    /// <summary>
    /// A class that represents a whole trajectory
    /// </summary>
    public class TrajectoryChromosome : ChromosomeBase
    {
        public int NumberOfSegments { get; protected set; }
        
        /// <summary>
        /// Construct a new TrajectoryChromosome
        /// </summary>
        /// <param name="numberOfGenes">The number of genes (i.e. control parameters) of the current trajectory</param>
        /// <param name="numberOfSegments">The number of segments this trajectory is devided in</param>
        public TrajectoryChromosome(int numberOfGenes, int numberOfSegments) : base(numberOfGenes)
        {
            NumberOfSegments = numberOfSegments;
            for (int i = 0; i < Length; i++) {
                ReplaceGene(i, GenerateGene(i));
            }
        }

        /// <summary>
        /// Create a new chromosome filled with random doubles between 0 and 1
        /// </summary>
        /// <returns></returns>
        public override IChromosome CreateNew()
        {
            return new TrajectoryChromosome(Length, NumberOfSegments);
        }

        /// <summary>
        /// Generate a new Gene
        /// </summary>
        /// <param name="geneIndex"></param>
        /// <returns></returns>
        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(RandomizationProvider.Current.GetDouble());
        }

        /// <summary>
        /// Calculate the number of control parameters that are needed to simulate a trajectory with the given number of segments
        /// </summary>
        /// <param name="numberOfSegments"></param>
        /// <returns></returns>
        public static int ChromosomeLength(int numberOfSegments)
        {
            return (int) (Math.Floor(numberOfSegments / 2.0) * 4 + Math.Ceiling(numberOfSegments / 2.0) * 3);
        }

        /// <summary>
        /// Return the setting index for a given segment and setting offset
        /// </summary>
        /// <param name="segmentIndex">The current segment</param>
        /// <param name="setting">The offset relative to the current segment index at which the setting is stored</param>
        /// <returns></returns>
        public static int SegmentSettingIndex(int segmentIndex, int setting)
        {
            return (int)(Math.Floor((segmentIndex - 1) / 2.0) * 4 + Math.Ceiling((segmentIndex - 1) / 2.0) * 3 + setting);
        }
    }
}