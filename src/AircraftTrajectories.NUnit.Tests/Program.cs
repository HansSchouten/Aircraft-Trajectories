using System.IO;
using System.Reflection;

namespace AircraftTrajectories.NUnit.Tests
{
    internal static class Globals
    {
        public static string testdataDirectory = Path.GetDirectoryName(
                (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath
            ) + "/../../Testdata/";
    }

    static class Program { }
}
