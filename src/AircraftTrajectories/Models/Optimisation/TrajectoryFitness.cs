using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Collections.Generic;

namespace AircraftTrajectories.Models.Optimisation
{
    using GeneticSharp.Domain.Randomizations;
    using IntegratedNoiseModel;
    using TemporalGrid;

    public class TrajectoryFitness : IFitness
    {

        public double Evaluate(IChromosome chromosome)
        {
            try
            {
                var trajectoryChromosome = (TrajectoryChromosome)chromosome;
                var aircraft = new Boeing747_400();
                var settings = new List<double>();

                for (int i = 0; i < chromosome.Length; i++)
                {
                    settings.Add((double)chromosome.GetGene(i).Value);
                }

                FlightSimulator f = new FlightSimulator(aircraft, new Point3D(18000, 18000, 0, CoordinateUnit.metric), trajectoryChromosome.NumberOfSegments, settings);
                var time = DateTime.Now;
                f.Simulate();

                for (int i=0; i<(160*160*f.duration); i++)
                {
                    var x1 = 10.0;
                    var x2 = 12.0;
                    var y1 = 20.0;
                    var y2 = 23.0;
                    var z1 = 25.0;
                    var z2 = 30.0;
                    var d = Math.Sqrt((x1 - x2)*(x1 - x2) + (y1 - y2)*(y1 - y2) + (z1 - z2)*(z1 - z2));

                    var P = 18000.0;
                    var LP1D1 = 80.0;
                    var LP2D1 = 85.0;
                    var LP1D2 = 79.0;
                    var LP2D2 = 83.0;
                    var P1 = 10000.0;
                    var P2 = 15000.0;
                    var D1 = 200.0;
                    var D2 = 800.0;

                    var LP1D = LP1D1 + (((LP1D2 - LP1D1) * (d - D1)) / (D2 - D1));
                    var LP2D = LP2D1 + (((LP2D2 - LP2D1) * (d - D1)) / (D2 - D1));
                    var LPD = LP1D1 + (((LP2D - LP1D) * (P - P1)) / (P2 - P1));

                    /*
                    var LP1D = LP1D1 + ( ((LP1D2 - LP1D1) * (Math.Log10(d) - Math.Log10(D1))) / (Math.Log10(D2) - Math.Log10(D1)) );
                    var LP2D = LP2D1 + ( ((LP2D2 - LP2D1) * (Math.Log10(d) - Math.Log10(D1))) / (Math.Log10(D2) - Math.Log10(D1)) );
                    var LPD = LP1D1 + (((LP2D - LP1D) * (P - P1)) / (P2 - P1));
                    */
                    //Console.WriteLine(LPD);
                }

                /*
                var trajectory = f.createTrajectory();
                var INMaircraft = new Aircraft("GP7270", "wing");
                var noiseModel = new IntegratedNoiseModel(trajectory, INMaircraft, true);

                int randomNumber = RandomizationProvider.Current.GetInt(0, 10000000);
                //Console.WriteLine(randomNumber);
                noiseModel.FileSuffix = randomNumber.ToString();

                //Console.WriteLine("INM started");
                noiseModel.RunINMFullTrajectory();
                //Console.WriteLine("INM completed");

                TemporalGrid temporalGrid = noiseModel.TemporalGrid;
                GridConverter converter = new GridConverter(temporalGrid, GridTransformation.MAX);
                Grid last = converter.transform().GetGrid(temporalGrid.GetNumberOfGrids() - 1);

                double sum = 0;
                for (int c = 0; c < last.Data.Length; c++)
                {
                    for (int r = 0; r < last.Data[0].Length; r++)
                    {
                        sum += last.Data[c][r];
                    }
                }
                */
                Console.WriteLine(DateTime.Now.Subtract(time).TotalMilliseconds);
                return 0;// int.MaxValue - sum;
            } catch (Exception ex)
            {
                Console.WriteLine("Evaluation exception");
                return int.MaxValue;
            }
        }
        
    }
}