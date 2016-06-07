using System;

namespace AircraftTrajectories.Models.Optimisation
{
    public enum FLAP_SETTINGS { UP, FLAPS1, FLAPS5, FLAPS10, FLAPS20 }

    public class Boeing747_400 : ISimulatorModel
    {
        public const int NumberOfEngines = 4;
        public const double ReferenceArea = 541.16;
        public FLAP_SETTINGS FlapSetting = FLAP_SETTINGS.FLAPS10;

        public double Mass {
            get { return 350000; }
        }
        public double VClean
        {
            get { return 280; }
        }
        public double VMax
        {
            get { return 300; }
        }

        public double TakeOffThrust(double TAS, double altitude)
        {
            double ktsToMps = 0.514444444;
            double lbfToN = 4.4482216;
            var isa = new ISA(altitude);
            double machNumber = (TAS * ktsToMps) / isa.VSound;
            //Console.WriteLine(isa.P);
            //Console.WriteLine(machNumber);
            return lbfToN * NumberOfEngines * isa.Delta * (
                (56283 + 1.3231 * altitude - 0.000048825 * altitude * altitude) +
                (-55343 - 0.41746 * altitude + 0.000013332 * altitude * altitude) * machNumber +
                (37825 + 1.1609 * altitude - 0.000031028 * altitude * altitude) * machNumber * machNumber
            );
        }
        
        public double ClimbThrust(double TAS, double altitude)
        {
            double ktsToMps = 0.514444444;
            double lbfToN = 4.4482216;
            var isa = new ISA(altitude);
            double machNumber = (TAS * ktsToMps) / isa.VSound;
            return lbfToN * NumberOfEngines * isa.Delta * (
                (52150 + 0.72668 * altitude - 0.000015837 * altitude * altitude) +
                (-53118 + 1.2828 * altitude - 0.0000084802 * altitude * altitude) * machNumber +
                (26330 - 0.89757 * altitude - 0.00001585 * altitude * altitude) * machNumber * machNumber
            );
        }

        public double FuelFLow(double totalThrust, double TAS, double altitude)
        {
            double ktsToMps = 0.514444444;
            double lbsToKg = 0.45359237;
            var isa = new ISA(altitude);
            double machNumber = (TAS * ktsToMps) / isa.VSound;
            double thrust = totalThrust / NumberOfEngines;
            double thrustNormalized = thrust / isa.Delta;
            double temperatureRatio = isa.Theta;

            return NumberOfEngines  * isa.Delta * Math.Pow(temperatureRatio, 0.62) * (
                (826.15 + 2140.5 * machNumber - 382.94 * machNumber * machNumber) +
                (0.2332 + 0.14859 * machNumber - 0.095481 * machNumber * machNumber) * thrustNormalized +
                (0.000001461 + 0.00000901 * machNumber - 0.000010795 * machNumber * machNumber) * thrustNormalized * thrustNormalized
            ) / 3600.0 * lbsToKg;
        }

        public double Drag(double airspeed, double altitude)
        {
            double ktsToMps = 0.514444444;
            airspeed = (airspeed * ktsToMps);
            var isa = new ISA(altitude);
            double dragPolar = DragPolar(isa.Rho, airspeed);
            return dragPolar * 0.5 * isa.Rho * airspeed * airspeed * ReferenceArea;
        }


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
                    // Flaps up
                    return 0.0233 - 0.0454 * CL + 0.1037 * CL * CL;
            }
        }
        
        protected double LiftCoefficient(double rho, double airspeed)
        {
            return (Mass * 9.81) / (0.5 * rho * airspeed * airspeed * ReferenceArea);
        }

        public double MinimumTurnRadius(double speed)
        {
            double ktsToMps = 0.514444444;
            speed *= ktsToMps;
            return speed * speed / 9.81 * Math.Sin(15 * Math.PI / 180);
        }
    }
}