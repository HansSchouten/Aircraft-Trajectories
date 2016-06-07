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
                settings.Insert(0, 1);
                settings.Insert(1, 1);
                settings.Insert(2, 1);
                settings.Insert(3, 0);
            }

            FlightSimulator f = new FlightSimulator(aircraft, new Point3D(18000, 0, 0, CoordinateUnit.metric), 1, settings);

            f.Simulate();

            var trajectory = f.createTrajectory();

            var INMaircraft = new Aircraft("GP7270", "wing");
            var noiseModel = new IntegratedNoiseModel(trajectory, INMaircraft);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            TemporalGrid temporalGrid = noiseModel.TemporalGrid;

            GridConverter converter = new GridConverter(temporalGrid, GridTransformation.MAX);
            Grid last = converter.transform().GetGrid(temporalGrid.GetNumberOfGrids());

            double sum = 0;
            for (int c=0; c < last.Data.Length; c++)
            {
                for (int r = 0; r < last.Data.Length; r++)
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
