using AircraftTrajectories.Models.Space3D;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System.Collections.Generic;

namespace AircraftTrajectories.Models.Optimisation
{
    /// <summary>
    /// Class that represents the fitness function of the genetic algorithm
    /// </summary>
    public class TrajectoryFitness : IFitness
    {
        /// <summary>
        /// Evaluate one chromosome (i.e. simulate a trajectory) and return its fitness
        /// </summary>
        /// <param name="chromosome">The chromome currently evaluated</param>
        /// <returns></returns>
        public double Evaluate(IChromosome chromosome)
        {
            var trajectoryChromosome = (TrajectoryChromosome)chromosome;
            var aircraft = new Boeing747_400();
            var settings = new List<double>();

            for (int i = 0; i < chromosome.Length; i++)
            {
                settings.Add((double)chromosome.GetGene(i).Value);
            }

            FlightSimulator f = new FlightSimulator(aircraft, new Point3D(30000, 30000, 0, CoordinateUnit.metric), trajectoryChromosome.NumberOfSegments, settings);
            f.Simulate();
            return f.fuel;
        }        
    }
}