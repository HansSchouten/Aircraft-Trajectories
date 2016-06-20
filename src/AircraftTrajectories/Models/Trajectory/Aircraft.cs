
namespace AircraftTrajectories.Models.Trajectory
{
    public class Aircraft
    {
        public string EngineId { get; protected set; }
        public string EngineMount { get; protected set; }
        public string Model { get; protected set; }

        public double LandingWeight { get; set; }
        public double TakeoffWeight { get; set; }
        public double WingArea { get; set; }
        public double DragCoefficient { get; set; }

        /// <summary>
        /// Constructs an aircraft model with given engine and engine mounting type
        /// </summary>
        /// <param name="engineId"></param>
        /// <param name="engineMount"></param>
        public Aircraft(string engineId, string engineMount)
        {
            EngineId = engineId;
            EngineMount = engineMount;
            WingArea = 40;
            DragCoefficient = 0.03;
            TakeoffWeight = 20000;
            LandingWeight = 10000;
            Model = "B738.dae";
        }
    }
}