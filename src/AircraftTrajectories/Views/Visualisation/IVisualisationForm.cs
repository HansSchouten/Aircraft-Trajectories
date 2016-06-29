using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    public interface IVisualisationForm
    {
        event EventHandler CalculateNoise;
        event EventHandler CancelNoiseCalculation;
        event EventHandler PrepareVisualisation;
        event EventHandler CancelVisualisationPreparation;
        event EventHandler VisualiseTrajectoryEvent;

        string NoiseFile { get; }
        string TrajectoryFile { get; }
        bool OneTrajectory { get; }
        bool ExternalNoise { get; }
        bool CustomReference { get; }
        bool Heatmap { get; }
        GeoPoint3D GeoReference { get; }
        Point3D MetricReference { get; }
        string MapFile { get; }
        GeoPoint3D MapBottomLeft { get; }
        GeoPoint3D MapUpperRight { get; }
        Trajectory Trajectory { get; set; }
        int NoiseMetric { get; }
        double PopulationFactor { get; }
        int PopulationDotSize { get; }
        string PopulationDotFile { get; }

        List<int> ContoursOfInterest { get; }
        double LowestContourValue { get; }
        double HighestContourValue { get; }
        double ContourValueStep { get; }
        bool VisualiseGradient { get; }
        bool VisualiseContoursOfInterest { get; }
        int CameraAltitude { get; }

        int Percentage { set; }
        int TimeLeft { set; }

        void Invoke(MethodInvoker methodInvoker);
        void NoiseCalculationCompleted();
        void PreparationCalculationCompleted();
    }
}
