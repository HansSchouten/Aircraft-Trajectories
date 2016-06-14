using System;
using System.IO;
using System.Reflection;

namespace AircraftTrajectories.NUnit.Tests
{
    internal static class Globals
    {
        public static string testdataDirectory = Uri.UnescapeDataString(Path.GetDirectoryName(
                (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath
            ) + "/../../Testdata/");

        public static string currentDirectory = Uri.UnescapeDataString(Path.GetDirectoryName(
                (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath
            ) + @"\");
    }

    static class Program { }
}
