using System;

namespace AircraftTrajectories.Views.Visualisation
{
    public interface IVisualisationForm
    {
        event EventHandler CalculateNoise;
        event EventHandler PrepareVisualisation;
        
        string NoiseFile { get; }
        string TrajectoryFile { get; }
        bool OneTrajectory { get; }
        bool ExternalNoise { get; }
    }
}
