using AircraftTrajectories.Views;
using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace AircraftTrajectories
{
    internal static class Globals
    {
        public static string currentDirectory = Path.GetDirectoryName(
                (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath
            ) + @"\";
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var startupForm = new Optimize();
            Application.Run(startupForm);
        }
    }
}
