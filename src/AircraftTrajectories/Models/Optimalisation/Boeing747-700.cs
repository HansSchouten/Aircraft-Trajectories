
namespace AircraftTrajectories.Models.Optimalisation
{
    public class Boeing747_700
    {
        public double TakeOffThrust(double TAS, double altitude)
        {
            var isa = new ISA(altitude);
            double machNumber = isa.VSound / (TAS * 0.514444444);
            return isa.Delta * (
                (56283 + 1.3231 * altitude - 0.000048825 * altitude * altitude) +
                (-55343 - 0.41746 * altitude + 0.000013332 * altitude * altitude) * machNumber +
                (37825 + 1.1609 * altitude - 0.000031028 * altitude * altitude) * machNumber * machNumber
            );
        }
    }
}
