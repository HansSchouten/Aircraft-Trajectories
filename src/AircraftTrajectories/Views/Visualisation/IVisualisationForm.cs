using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    public interface IVisualisationForm
    {
        event EventHandler CalculateNoise;
        event EventHandler CancelNoiseCalculation;
        event EventHandler PrepareVisualisation;
        event EventHandler CancelVisualisationPreparation;

        string NoiseFile { get; }
        string TrajectoryFile { get; }
        bool OneTrajectory { get; }
        bool ExternalNoise { get; }

        int Percentage { set; }
        int TimeLeft { set; }

        void Invoke(MethodInvoker methodInvoker);
        void NoiseCalculationCompleted();
    }
}
