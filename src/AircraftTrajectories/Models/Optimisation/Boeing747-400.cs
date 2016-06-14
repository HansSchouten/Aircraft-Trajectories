using System;

namespace AircraftTrajectories.Models.Optimisation
{
    /// <summary>
    /// A class representing a Boeing 747-400 which can be used in flight simulations
    /// </summary>
    public class Boeing747_400 : ISimulatorModel
    {
        public const int NumberOfEngines = 4;
        public const double ReferenceArea = 541.16;
        protected enum FLAP_SETTINGS { UP, FLAPS1, FLAPS5, FLAPS10, FLAPS20 }
        protected FLAP_SETTINGS FlapSetting = FLAP_SETTINGS.FLAPS10;

        /// <summary>
        /// The mass of the aircraft
        /// </summary>
        public double Mass {
            get { return 350000; }
        }
        /// <summary>
        /// The speed at which the landing gear and flaps are retracted
        /// </summary>
        public double VClean {
            get { return 280; }
        }
        /// <summary>
        /// The maximum allowed speed of the aircraft
        /// </summary>
        public double VMax {
            get { return 300; }
        }

        /// <summary>
        /// Calculate the takeoff thrust
        /// </summary>
        /// <param name="TAS">The current True AirSpeed</param>
        /// <param name="altitude">The current altitude</param>
        /// <returns></returns>
        public double TakeOffThrust(double TAS, double altitude)
        {
            ISA isa = new ISA(altitude);
            double machNumber = (TAS * 0.514444444) / isa.VSound;

            return 4.4482216 * NumberOfEngines * isa.Delta * (
                (56283 + 1.3231 * altitude - 0.000048825 * altitude * altitude) +
                (-55343 - 0.41746 * altitude + 0.000013332 * altitude * altitude) * machNumber +
                (37825 + 1.1609 * altitude - 0.000031028 * altitude * altitude) * machNumber * machNumber
            );
        }

        /// <summary>
        /// Calculate the climb thrust
        /// </summary>
        /// <param name="TAS">The current True AirSpeed</param>
        /// <param name="altitude">The current altitude</param>
        /// <returns></returns>
        public double ClimbThrust(double TAS, double altitude)
        {
            ISA isa = new ISA(altitude);
            double machNumber = (TAS * 0.514444444) / isa.VSound;

            return 4.4482216 * NumberOfEngines * isa.Delta * (
                (52150 + 0.72668 * altitude - 0.000015837 * altitude * altitude) +
                (-53118 + 1.2828 * altitude - 0.0000084802 * altitude * altitude) * machNumber +
                (26330 - 0.89757 * altitude - 0.00001585 * altitude * altitude) * machNumber * machNumber
            );
        }

        /// <summary>
        /// Calculate the amound of fuel is consumed in kilograms per second
        /// </summary>
        /// <param name="totalThrust">The total amound of thrust delivered by all engines</param>
        /// <param name="TAS">The current True AirSpeed</param>
        /// <param name="altitude">The current altitude</param>
        /// <returns></returns>
        public double FuelFLow(double totalThrust, double TAS, double altitude)
        {
            ISA isa = new ISA(altitude);
            double machNumber = (TAS * 0.514444444) / isa.VSound;
            double thrust = totalThrust / NumberOfEngines;
            double thrustNormalized = thrust / isa.Delta;
            double temperatureRatio = isa.Theta;

            return NumberOfEngines  * isa.Delta * Math.Pow(temperatureRatio, 0.62) * (
                (826.15 + 2140.5 * machNumber - 382.94 * machNumber * machNumber) +
                (0.2332 + 0.14859 * machNumber - 0.095481 * machNumber * machNumber) * thrustNormalized +
                (0.000001461 + 0.00000901 * machNumber - 0.000010795 * machNumber * machNumber) * thrustNormalized * thrustNormalized
            ) / 3600.0 * 0.45359237;
        }

        /// <summary>
        /// Calculate the drag for the given speed and altitude
        /// </summary>
        /// <param name="airspeed">The current airspeed</param>
        /// <param name="altitude">The current altitude</param>
        /// <returns></returns>
        public double Drag(double airspeed, double altitude)
        {
            airspeed = (airspeed * 0.514444444);
            ISA isa = new ISA(altitude);
            double dragPolar = DragPolar(isa.Rho, airspeed);

            return dragPolar * 0.5 * isa.Rho * airspeed * airspeed * ReferenceArea;
        }

        /// <summary>
        /// Calculate the drag polar
        /// </summary>
        /// <param name="rho">The rho value received from ISA</param>
        /// <param name="airspeed">The current airspeed</param>
        /// <returns></returns>
        protected double DragPolar(double rho, double airspeed)
        {
            double CL = LiftCoefficient(rho, airspeed);
            switch (FlapSetting)
            {
                case FLAP_SETTINGS.FLAPS1:
                    return 0.0233 - 0.0454 * CL + 0.1037 * CL * CL;
                case FLAP_SETTINGS.FLAPS5:
                    return 0.0344 - 0.0129 * CL + 0.0571 * CL * CL;
                case FLAP_SETTINGS.FLAPS10:
                    return 0.0463 - 0.0424 * CL + 0.0726 * CL * CL;
                case FLAP_SETTINGS.FLAPS20:
                    return 0.0387 + 0.0085 * CL + 0.0402 * CL * CL;
                default:
                    return 0.0233 - 0.0454 * CL + 0.1037 * CL * CL;
            }
        }
        
        /// <summary>
        /// Calculate the lift coeficient
        /// </summary>
        /// <param name="rho">The rho value received from ISA</param>
        /// <param name="airspeed">The current airspeed</param>
        /// <returns></returns>
        protected double LiftCoefficient(double rho, double airspeed)
        {
            return (Mass * 9.81) / (0.5 * rho * airspeed * airspeed * ReferenceArea);
        }

        /// <summary>
        /// Calculate the radius of the smallest turn the aircraft can execute at the given speed
        /// </summary>
        /// <param name="speed">The current airspeed</param>
        /// <returns></returns>
        public double MinimumTurnRadius(double speed)
        {
            speed *= 0.514444444;
            return speed * speed / 9.81 * Math.Sin(15 * Math.PI / 180);
        }
    }
}