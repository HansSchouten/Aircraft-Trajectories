using AircraftTrajectories.Models.Optimisation;
using AircraftTrajectories.Models.Space3D;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views
{
    public partial class Optimize : Form
    {
        public Optimize()
        {
            InitializeComponent();
        }


        public Boeing747_400 aircraft;
        

        private void Optimize_Load(object sender, EventArgs e)
        {
            /*
            var aircraft = new Boeing747_400();
            FlightSimulator f = new FlightSimulator(aircraft, 3, new Point3D(18000, 0, 0, CoordinateUnit.metric));
            f.A = 0.5;
            f.B = 0.5;
            f.C = 0.5;
            f.Simulate();
            return;
            */
            var selection = new EliteSelection();
            var crossover = new OrderedCrossover();
            var mutation = new ReverseSequenceMutation();
            var fitness = new TrajectoryFitness();
            var chromosome = new TrajectoryChromosome(TrajectoryChromosome.ChromosomeLength(3), 3);
            var population = new Population(50, 70, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(50);

            Console.WriteLine("GA running...");
            ga.Start();
            Console.WriteLine("Best solution found has {0} fitness.", int.MaxValue - ga.BestChromosome.Fitness);
            Console.WriteLine(ga.BestChromosome.GetGene(0).Value + " " + ga.BestChromosome.GetGene(1).Value + " " + ga.BestChromosome.GetGene(2).Value);

            /*
            aircraft = new Boeing747_400();
            FlightSimulator f = new FlightSimulator(aircraft);
            f.Simulate();
            */
        }
    }
}