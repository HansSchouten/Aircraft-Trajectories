using AircraftTrajectories.Models.Optimisation;
using AircraftTrajectories.Models.Population;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Views;
using AircraftTrajectories.Views.Optimisation;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Infrastructure.Threading;
using System;
using System.Threading;
using System.Windows.Forms;

namespace AircraftTrajectories.Presenters
{
    public class OptimisationPresenter
    {
        protected IOptimisationForm _view;
        protected StartupForm _startForm;

        public OptimisationPresenter(IOptimisationForm view, StartupForm startForm)
        {
            _view = view;
            _view.RunOptimisation += delegate (object sender, EventArgs e) { RunOptimisation(); };
            _view.CancelOptimisation += delegate (object sender, EventArgs e) { CancelOptimisation(); };
            _view.VisualiseTrajectory += delegate (object sender, EventArgs e) { VisualiseTrajectory(); };
            _view.SaveTrajectory += delegate (object sender, EventArgs e) { SaveTrajectory(); };
            _startForm = startForm;
        }

        Thread thread;
        GeneticAlgorithm ga;
        DateTime startTime;
        protected void RunOptimisation()
        {
            thread = new Thread(RunGeneticAlgorithm);
            thread.Start();
        }
        protected void RunGeneticAlgorithm()
        {
            try
            {
                var referencePoint = new ReferencePoint(new GeoPoint3D(_view.StartLongitude, _view.StartLatitude), new Point3D(0, 0));
                TrajectoryFitness.ReferencePoint = referencePoint;
                TrajectoryFitness.GeoEndPoint = new GeoPoint3D(_view.EndLongitude, _view.EndLatitude);
                TrajectoryFitness.OptimiseFuel = !_view.MinimiseNoise;
                TrajectoryFitness.TakeoffHeading = _view.TakeoffHeading;
                TrajectoryFitness.TakeoffSpeed = _view.TakeoffSpeed;
                TrajectoryFitness.trajectories = null;
                TrajectoryFitness.Best = int.MinValue;
                TrajectoryFitness.PopulationData = new PopulationData2("population.dat", referencePoint);

                var selection = new EliteSelection();
                var crossover = new OrderedCrossover();
                var mutation = new ReverseSequenceMutation();
                var fitness = new TrajectoryFitness();
                var chromosome = new TrajectoryChromosome(TrajectoryChromosome.ChromosomeLength(_view.NumberOfSegments), _view.NumberOfSegments);
                var population = new Population(_view.PopulationSize, _view.PopulationSize, chromosome);
                ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
                var executor = new SmartThreadPoolTaskExecutor();
                executor.MinThreads = 1;
                executor.MaxThreads = 1;
                ga.TaskExecutor = executor;
                ga.Termination = new GenerationNumberTermination(_view.NumberOfGenerations);

                startTime = DateTime.Now;

                ga.TerminationReached += OptimisationCompleted;
                ga.GenerationRan += UpdatePercentage;
                ga.Start();
            }
            catch (ThreadAbortException)
            {
                // ignore it
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void CancelOptimisation()
        {
            if (thread != null)
            {
                TrajectoryFitness.PopulationData = null;
                thread.Abort();
            }
        }
        protected void OptimisationCompleted(object sender, EventArgs e)
        {
            TrajectoryFitness.PopulationData = null;
            var best = ga.BestChromosome;
            TrajectoryFitness fitness = new TrajectoryFitness();
            fitness.Evaluate(best);
            _view.Invoke(delegate
            {
                _view.OptimisationCompleted(fitness.FlightSimulator);
            });
        }



        protected void VisualiseTrajectory()
        {
            var best = ga.BestChromosome;
            TrajectoryFitness fitness = new TrajectoryFitness();
            fitness.Evaluate(best);
            _startForm.Visualise(fitness.FlightSimulator.CreateTrajectory());
        }

        protected void SaveTrajectory()
        {

        }

        protected void UpdatePercentage(object sender, EventArgs e)
        {
            _view.Invoke(delegate
            {
                double factor = (double) ga.GenerationsNumber / _view.NumberOfGenerations;
                double secElapsed = DateTime.Now.Subtract(startTime).TotalSeconds;

                _view.Percentage = (int) (factor * 100);
                _view.TimeLeft = (int) Math.Ceiling(((secElapsed / factor) - secElapsed) / 60.0);
            });
        }
    }
}