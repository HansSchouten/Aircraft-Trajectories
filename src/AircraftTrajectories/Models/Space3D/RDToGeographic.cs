using System;
using System.Drawing;

namespace AircraftTrajectories.Models.Space3D
{
    public class RDToGeographic
    {
        /// <summary>
        /// Converts the coordinates of 2D Point to Geographic units
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public PointF convertToLatLong(double x, double y)
        {
            int referenceRdX = 155000;
            int referenceRdY = 463000;

            double dX = (double)(x - referenceRdX) * (double)(Math.Pow(10, -5));
            double dY = (double)(y - referenceRdY) * (double)(Math.Pow(10, -5));

            double sumN =
                (3235.65389 * dY) +
                (-32.58297 * Math.Pow(dX, 2)) +
                (-0.2475 * Math.Pow(dY, 2)) +
                (-0.84978 * Math.Pow(dX, 2) * dY) +
                (-0.0655 * Math.Pow(dY, 3)) +
                (-0.01709 * Math.Pow(dX, 2) * Math.Pow(dY, 2)) +
                (-0.00738 * dX) +
                (0.0053 * Math.Pow(dX, 4)) +
                (-0.00039 * Math.Pow(dX, 2) * Math.Pow(dY, 3)) +
                (0.00033 * Math.Pow(dX, 4) * dY) +
                (-0.00012 * dX * dY);
            double sumE =
                (5260.52916 * dX) +
                (105.94684 * dX * dY) +
                (2.45656 * dX * Math.Pow(dY, 2)) +
                (-0.81885 * Math.Pow(dX, 3)) +
                (0.05594 * dX * Math.Pow(dY, 3)) +
                (-0.05607 * Math.Pow(dX, 3) * dY) +
                (0.01199 * dY) +
                (-0.00256 * Math.Pow(dX, 3) * Math.Pow(dY, 2)) +
                (0.00128 * dX * Math.Pow(dY, 4)) +
                (0.00022 * Math.Pow(dY, 2)) +
                (-0.00022 * Math.Pow(dX, 2)) +
                (0.00026 * Math.Pow(dX, 5));
            
            double referenceWgs84X = 52.15517;
            double referenceWgs84Y = 5.387206;

            float latitude = (float) (referenceWgs84X + (sumN / 3600));
            float longitude = (float) (referenceWgs84Y + (sumE / 3600));

            return new PointF(longitude, latitude);
        }
    }
}
