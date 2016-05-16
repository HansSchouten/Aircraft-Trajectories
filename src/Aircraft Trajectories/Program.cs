using AircraftTrajectories.Presenters;
using AircraftTrajectories.Views;
using AircraftTrajectories.Models;
using System;
using System.Windows.Forms;

namespace AircraftTrajectories
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var googleEarthForm = new Test();
            var googleEarthPresenter = new GoogleEarthPresenter(googleEarthForm);

            Application.Run(googleEarthForm);
        }
    }
}
