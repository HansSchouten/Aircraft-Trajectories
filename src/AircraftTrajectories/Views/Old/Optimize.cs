using AircraftTrajectories.Models.Optimisation;
using AircraftTrajectories.Models.Space3D;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Infrastructure.Threading;
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
            var selection = new EliteSelection();
            var crossover = new OrderedCrossover();
            var mutation = new ReverseSequenceMutation();
            var fitness = new TrajectoryFitness();
            var chromosome = new TrajectoryChromosome(TrajectoryChromosome.ChromosomeLength(3), 3);
            var population = new Population(35, 40, chromosome);
            //72 (6)
            //67 (2)
            //49 (3)
            //102 (1)
            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            var executor = new SmartThreadPoolTaskExecutor();
            executor.MinThreads = 1;
            executor.MaxThreads = 1;
            ga.TaskExecutor = executor;
            ga.Termination = new GenerationNumberTermination(10);

            Console.WriteLine("GA running...");
            var t = DateTime.Now;
            ga.Start();
            Console.WriteLine("Time:"+DateTime.Now.Subtract(t).TotalSeconds);

            Console.WriteLine("Best solution found has {0} fitness.", int.MaxValue - ga.BestChromosome.Fitness);
            Console.WriteLine(ga.BestChromosome.GetGene(0).Value + " " + ga.BestChromosome.GetGene(1).Value + " " + ga.BestChromosome.GetGene(2).Value);
        }
    }
}