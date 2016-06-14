using System;

namespace AircraftTrajectories.Models.Trajectory
{
    public class Aircraft
    {
        public string EngineId { get; protected set; }
        public string EngineMount { get; protected set; }
        public string Model { get; protected set; }

        /// <summary>
        /// Constructs an aircraft model with given engine and engine mounting type
        /// </summary>
        /// <param name="engineId"></param>
        /// <param name="engineMount"></param>
        public Aircraft(string engineId, string engineMount)
        {
            EngineId = engineId;
            EngineMount = engineMount;
            Model = "B738.dae";
        }
    }
}