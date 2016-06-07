using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Collections.Generic;

namespace AircraftTrajectories.Models.Optimisation
{
    using AircraftTrajectories.Models.IntegratedNoiseModel;
    using AircraftTrajectories.Models.TemporalGrid;

    public class TrajectoryFitness : IFitness
    {
        bool completed = false;

        public double Evaluate(IChromosome chromosome)
        {
            
            var aircraft = new Boeing747_400();
            var settings = new List<double>();

            for (int i = 0; i < chromosome.Length; i++)
            {
                settings.Add((double) chromosome.GetGene(i).Value);
            }

            FlightSimulator f = new FlightSimulator(aircraft, new Point3D(18000, 0, 0, CoordinateUnit.metric), 1, settings);

            f.Simulate();

            var trajectory = f.createTrajectory();

            var INMaircraft = new Aircraft("GP7270", "wing");
            var noiseModel = new IntegratedNoiseModel(trajectory, INMaircraft, true);
            noiseModel.StartCalculation(INMCompleted);
            Console.WriteLine(0);
            while (!completed) { }

            TemporalGrid temporalGrid = noiseModel.TemporalGrid;

            GridConverter converter = new GridConverter(temporalGrid, GridTransformation.MAX);
            Grid last = converter.transform().GetGrid(temporalGrid.GetNumberOfGrids()-1);
            Console.WriteLine(2);
            double sum = 0;
            for (int c=0; c < last.Data.Length; c++)
            {
                for (int r = 0; r < last.Data[0].Length; r++)
                {
                    sum += last.Data[c][r];
                }
            }

            return int.MaxValue - sum;
            
        }

    private void INMCompleted()
    {
        completed = true;
    }
}
}
