using AircraftTrajectories.Views;
using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace AircraftTrajectories
{
    internal static class Globals
    {
        public static string currentDirectory = Uri.UnescapeDataString(Path.GetDirectoryName(
                (new Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath
            ) + @"\");
        public static string webrootDirectory = Uri.UnescapeDataString(Path.GetDirectoryName(
                (new Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath
            ) + @"\webroot\");
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            new ContourForm().Show();
            Application.Run();
        }
    }
}
