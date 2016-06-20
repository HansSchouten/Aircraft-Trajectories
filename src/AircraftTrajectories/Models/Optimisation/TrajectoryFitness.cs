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
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;
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

                FlightSimulator f = new FlightSimulator(aircraft, new Point3D(30000, 30000, 0, CoordinateUnit.metric), trajectoryChromosome.NumberOfSegments, settings);
                var time = DateTime.Now;
                f.Simulate();



                /*
                var grid = f.LAMaxGrid;
                //Console.WriteLine("calculated:"+grid.Data[159][159]);
                double[][] noiseDataGrid = grid.Data;
                var c = new Models.ColorMap();
                var cmap = c.Custom(noiseDataGrid);
                Bitmap b = new Bitmap(200, 200);
                LockBitmap l = new LockBitmap(b);
                l.LockBits();
                for (int x = 0; x < noiseDataGrid.Length - 1; x++)
                {
                    for (int y = 0; y < noiseDataGrid[0].Length - 1; y++)
                    {
                        l.SetPixel(x, y, Color.FromArgb(Math.Max(0,cmap[x, y, 0]), Math.Max(cmap[x, y, 1],0), Math.Max(cmap[x,y,2], 0)));
                    }
                }
                l.UnlockBits();
                b.Save(@"C:\Users\hanss\Desktop\AT.png", ImageFormat.Png);



            
                var trajectory = f.createTrajectory();
                var INMaircraft = new Aircraft("GP7270", "wing");
                var noiseModel = new IntegratedNoiseModel(trajectory, INMaircraft, true);
                noiseModel.GridName = "optGrid2D";

                int randomNumber = RandomizationProvider.Current.GetInt(0, 10000000);
                //Console.WriteLine(randomNumber);
                noiseModel.FileSuffix = randomNumber.ToString();

                //Console.WriteLine("INM started");
                noiseModel.RunINMFullTrajectory();
                //Console.WriteLine("INM completed");
                */


                /*
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


                /*
                Console.WriteLine("INM:" + noiseModel.TemporalGrid.GetGrid(0).Data[160][160]);
                noiseDataGrid = noiseModel.TemporalGrid.GetGrid(0).Data;
                c = new Models.ColorMap();
                cmap = c.Custom(noiseDataGrid);
                b = new Bitmap(200, 200);
                l = new LockBitmap(b);
                l.LockBits();
                for (int x = 0; x < noiseDataGrid.Length - 1; x++)
                {

                    for (int y = 0; y < noiseDataGrid[0].Length - 1; y++)
                    {
                        l.SetPixel(x, y, Color.FromArgb(Math.Max(0,cmap[x, y, 0]), Math.Max(cmap[x, y, 1],0), Math.Max(cmap[x,y,2], 0)));
                    }
                }
                l.UnlockBits();
                b.Save(@"C:\Users\hanss\Desktop\INM.png", ImageFormat.Png);
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