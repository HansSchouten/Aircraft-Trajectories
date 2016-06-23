﻿using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Optimisation
{
    public interface IOptimisationForm
    {
        event EventHandler RunOptimisation;
        event EventHandler CancelOptimisation;
        event EventHandler SaveTrajectory;
        event EventHandler VisualiseTrajectory;

        // Genetic Algorithm
        int PopulationSize { get; }
        int NumberOfGenerations { get; }
        // Trajectory
        int NumberOfSegments { get; }
        double StartLatitude { get; }
        double StartLongitude { get; }
        double EndLatitude { get; }
        double EndLongitude { get; }

        int Percentage { set; }
        int TimeLeft { set; }

        void Invoke(MethodInvoker methodInvoker);
        void OptimisationCompleted();
    }
}