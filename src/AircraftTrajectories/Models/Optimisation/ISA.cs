using System;

namespace AircraftTrajectories.Models.Optimisation
{
    /// <summary>
    /// Class that represents the International Standard Atmosphere
    /// </summary>
    public class ISA
    {
        // Atmospheric conditions at Mean Sea Level
        public double t0 = 273.15 + 15;                 // Temperature [degrees Kelvin]
        public double lambda = -6.5 / 1000;             // Temperature with height [degrees Kelvin / m]
        public double p0 = 101325;                      // Pressure [N/m^2]
        public double R_gas = 8.31432 / 0.0289644;      // Gas constant for air [m^2/degrees Kelvin * sec^2]
        public double rho0 = 1.225;                     // Air density [kg/m^3]
        public double g0 = 9.80665;                     // Gravitational acceleration [m/sec^2]
        public double gamma = 1.4;                      // Ratio of specific heats

        // Properties
        public double T { get; protected set; }
        public double dTdZ { get; protected set; }
        public double Theta { get; protected set; }
        public double dThetaDz { get; protected set; }
        public double P { get; protected set; }
        public double dPdZ { get; protected set; }
        public double Delta { get; protected set; }
        public double dDeltaDz { get; protected set; }
        public double Rho { get; protected set; }
        public double dRhoDz { get; protected set; }
        public double VSound { get; protected set; }
        public double VSoundDz { get; protected set; }

        /// <summary>
        /// Create an ISA object
        /// </summary>
        /// <param name="z">Altitude in feet</param>
        public ISA(double z)
        {
            z = z * 0.3048;

            T = t0 + lambda * z;
            dTdZ = lambda;
            Theta = T / t0;
            dThetaDz = dTdZ / t0;
            P = p0 * Math.Pow(T / t0, -g0 / lambda / R_gas);
            dPdZ = -p0 * g0 / lambda / R_gas * Math.Pow(T / t0, -g0 / lambda / R_gas) * dTdZ / T;
            Delta = P / p0;
            dDeltaDz = dPdZ / p0;
            Rho = rho0 * Delta / Theta;
            dRhoDz = (-t0 * lambda * rho0 * (g0 / (lambda * R_gas) + 1) / Math.Pow(T,2)) * Math.Pow(1 / Theta, g0 / (lambda * R_gas));
            VSound = Math.Sqrt(gamma * R_gas * T);
            VSoundDz = 0.5 * gamma * R_gas * lambda * VSound;
        }
    }
}