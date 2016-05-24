using AircraftTrajectories.Views;
using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace AircraftTrajectories
{
    internal static class Globals
    {
        public static string webrootDirectory = Path.GetDirectoryName(
                (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath
            ) + @"\webroot\";
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            MessageBox.Show(Globals.webrootDirectory);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var startupForm = new Test();
            Application.Run(startupForm);
        }
    }
}
