using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;

namespace AircraftTrajectories.Models.Optimisation
{
    public class TrajectoryFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            var aircraft = new Boeing747_400();
            FlightSimulator f = new FlightSimulator(aircraft);
            f.A = (double)chromosome.GetGene(0).Value;
            f.B = (double)chromosome.GetGene(1).Value;
            f.C = (double)chromosome.GetGene(2).Value;
            f.Simulate();
            return int.MaxValue-f.fuel;
        }
    }
}
