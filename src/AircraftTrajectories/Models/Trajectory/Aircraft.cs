using System;

namespace AircraftTrajectories.Models.Trajectory
{
    class Aircraft
    {
        public String EngineId { get; protected set; }
        public String EngineMount { get; protected set; }

        public Aircraft(string engineId, string engineMount)
        {
            EngineId = engineId;
            EngineMount = engineMount;
        }

    }
}
