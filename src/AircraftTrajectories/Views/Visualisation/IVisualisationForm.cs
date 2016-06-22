using AircraftTrajectories.Models.Space3D;
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
        bool CustomReference { get; }
        GeoPoint3D GeoReference { get; }
        Point3D MetricReference { get; }
        string MapFile { get; }
        GeoPoint3D MapBottomLeft { get; }
        GeoPoint3D MapUpperRight { get; }
        int NumberOfContours { get; }
        int ContourStartValue { get; }

        int Percentage { set; }
        int TimeLeft { set; }

        void Invoke(MethodInvoker methodInvoker);
        void NoiseCalculationCompleted();
        void PreparationCalculationCompleted();
    }
}
