using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Optimisation
{
    public interface IOptimisationForm
    {
        event EventHandler RunOptimisation;
        event EventHandler CancelOptimisation;

        // 
        // Takeoff
        // Genetic Algorithm
        int PopulationSize { get; }
        int NumberOfGenerations { get; }
        // Trajectory
        int NumberOfSegments { get; }
        double EndLatitude { get; }
        double EndLongitude { get; }

        int Percentage { set; }
        int TimeLeft { set; }

        void Invoke(MethodInvoker methodInvoker);
    }
}