namespace AircraftTrajectories.Models.Optimisation
{
    public interface ISimulatorModel
    {
        double TakeOffThrust(double TAS, double altitude);
        double ClimbThrust(double TAS, double altitude);
        double Drag(double airspeed, double altitude);
        double FuelFLow(double thrust, double TAS, double altitude);

        double VClean { get; }
        double Mass { get; }
    }
}
