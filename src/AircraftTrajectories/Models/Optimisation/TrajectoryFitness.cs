using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Collections.Generic;

namespace AircraftTrajectories.Models.Optimisation
{
    using System.Device.Location;
    using System.Drawing;
    using System.Drawing.Imaging;
    using TemporalGrid;
    using IntegratedNoiseModel;

    public class TrajectoryFitness : IFitness
    {
        public FlightSimulator FlightSimulator { get; set; }
        public static ReferencePoint ReferencePoint { get; set; }
        public static GeoPoint3D GeoEndPoint { get; set; }
        public static bool OptimiseFuel = false;
        public static double TakeoffHeading;
        public static double TakeoffSpeed;

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
                FlightSimulator._speed = TakeoffSpeed;
                FlightSimulator._heading = TakeoffHeading * Math.PI / 180;

                var time = DateTime.Now;
                FlightSimulator.Simulate();
                Console.WriteLine(DateTime.Now.Subtract(time).TotalMilliseconds);





                /*                  INTERNAL NOISE              */
                /*
                var grid = FlightSimulator.NoiseMaxGrid;
                //Console.WriteLine("calculated:"+grid.Data[159][159]);
                double[][] noiseDataGrid = grid.Data;
                var color = new Models.ColorMap();
                var cmap = color.Custom(noiseDataGrid);
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
                b.Save(@"C:\Users\Hans Schouten\Desktop\noise.png", ImageFormat.Png);
				*/
				
                /*
                TemporalGrid temporalGrid = noiseModel.TemporalGrid;
                GridConverter converter = new GridConverter(temporalGrid, GridTransformation.MAX);
                Grid last = converter.transform().GetGrid(temporalGrid.GetNumberOfGrids() - 1);
                */








                /*                      INM                     */
                var trajectory = FlightSimulator.CreateTrajectory();
                var INMaircraft = new Aircraft("GP7270", "wing");
                var noiseModel = new IntegratedNoiseModel(trajectory, INMaircraft);
                noiseModel.NoiseMetric = 0;
                Console.WriteLine("INM started");
                noiseModel.RunINMFullTrajectory();
                Console.WriteLine("INM completed");

                //Console.WriteLine("INM:" + noiseModel.TemporalGrid.GetGrid(0).Data[160][160]);
                var noiseDataGrid = noiseModel.TemporalGrid.GetGrid(0).Data;
                var color = new Models.ColorMap();
                var cmap = color.Custom(noiseDataGrid);
                var b = new Bitmap(noiseDataGrid.Length, noiseDataGrid[0].Length);
                var l = new LockBitmap(b);
                l.LockBits();
                for (int x = 0; x < noiseDataGrid.Length - 1; x++)
                {

                    for (int y = 0; y < noiseDataGrid[0].Length - 1; y++)
                    {
                        l.SetPixel(x, y, Color.FromArgb(Math.Max(0,cmap[x, y, 0]), Math.Max(cmap[x, y, 1],0), Math.Max(cmap[x,y,2], 0)));
                    }
                }
                l.UnlockBits();
                b.Save(@"C:\Users\Hans Schouten\Desktop\INM.png", ImageFormat.Png);











                if (OptimiseFuel)
                {
                    return int.MaxValue - FlightSimulator.fuel;
                } else
                {
                    Grid noiseMax = noiseModel.TemporalGrid.GetGrid(0);
                    double sum = 0;
                    for (int c = 0; c < noiseMax.Data.Length; c++)
                    {
                        for (int r = 0; r < noiseMax.Data[0].Length; r++)
                        {
                            sum += noiseMax.Data[c][r];
                        }
                    }
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