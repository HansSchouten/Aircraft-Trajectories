using System;
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
        static void Main() { }
    }
}
