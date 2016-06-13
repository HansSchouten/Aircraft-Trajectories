namespace AircraftTrajectories.Models.Optimisation
{
    /// <summary>
    /// Interface that defines an SimulatorModel, which is an aircraft that can be used in a simulation
    /// </summary>
    public interface ISimulatorModel
    {
        double TakeOffThrust(double TAS, double altitude);
        double ClimbThrust(double TAS, double altitude);
        double Drag(double airspeed, double altitude);
        double FuelFLow(double thrust, double TAS, double altitude);
        double MinimumTurnRadius(double TAS);

        double VClean { get; }
        double Mass { get; }
        double VMax { get; }
    }
}