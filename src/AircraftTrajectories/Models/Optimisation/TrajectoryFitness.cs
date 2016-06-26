using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Collections.Generic;

namespace AircraftTrajectories.Models.Optimisation
{
    using System.Device.Location;
    using TemporalGrid;

    public class TrajectoryFitness : IFitness
    {
        public FlightSimulator FlightSimulator { get; set; }
        public static ReferencePoint ReferencePoint { get; set; }
        public static GeoPoint3D GeoEndPoint { get; set; }
        public static bool OptimiseFuel = false;

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

                var startPoint = ReferencePoint.GeoPoint;
                var start = new GeoCoordinate(startPoint.Latitude, startPoint.Longitude);
                var endDLong = new GeoCoordinate(startPoint.Latitude, GeoEndPoint.Longitude);
                double xDistance = start.GetDistanceTo(endDLong);
                if (start.Longitude > GeoEndPoint.Longitude) xDistance = -xDistance;
                var endDLat = new GeoCoordinate(GeoEndPoint.Latitude, startPoint.Longitude);
                double yDistance = start.GetDistanceTo(endDLat);
                if (start.Latitude > GeoEndPoint.Latitude) yDistance = -yDistance;
                var endPoint = new Point3D(xDistance, yDistance);

                var trajectoryGenerator = new TrajectoryGenerator(new Aircraft("PW4056", "wing"), ReferencePoint);
                FlightSimulator = new FlightSimulator(aircraft, endPoint, trajectoryChromosome.NumberOfSegments, settings, trajectoryGenerator);
                var time = DateTime.Now;
                FlightSimulator.Simulate();

                /*
                var grid = f.NoiseMaxGrid;
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

            //Console.WriteLine("INM started");
            noiseModel.RunINMFullTrajectory();
            //Console.WriteLine("INM completed");
            */

                /*
            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            GridConverter converter = new GridConverter(temporalGrid, GridTransformation.MAX);
            Grid last = converter.transform().GetGrid(temporalGrid.GetNumberOfGrids() - 1);
            */

            Grid noiseMax = FlightSimulator.NoiseMaxGrid;
            double sum = 0;
            for (int c = 0; c < noiseMax.Data.Length; c++)
            {
                for (int r = 0; r < noiseMax.Data[0].Length; r++)
                {
                    sum += noiseMax.Data[c][r];
                }
            }


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
                if (OptimiseFuel)
                {
                    return int.MaxValue - FlightSimulator.fuel;
                } else
                {
                    return int.MaxValue - sum;
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Evaluation exception");
                return int.MaxValue;
            }
        }
        
    }
}