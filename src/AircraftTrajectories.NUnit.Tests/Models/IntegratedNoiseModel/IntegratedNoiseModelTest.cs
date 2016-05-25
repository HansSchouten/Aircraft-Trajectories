using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.IntegratedNoiseModel
{
    using AircraftTrajectories.Models.IntegratedNoiseModel;
    using AircraftTrajectories.Models.TemporalGrid;
    using System.Diagnostics;
    using System.IO;
    [TestFixture]
    public class IntegratedNoiseModelTest
    {
        IntegratedNoiseModel noiseModel;

        [Test]
        public void INMFullTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(fullTestCompleted);
        }
        private void fullTestCompleted()
        {
            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            Assert.AreNotEqual(0, temporalGrid.GetNumberOfGrids());
        }


        [Test]
        public void INMPositionFileTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(positionFileTestCompleted);
        }
        private void positionFileTestCompleted()
        {
            Assert.True(File.Exists(Globals.testdataDirectory + "test_track.dat"));
        }


        [Test]
        public void INMExecutableExceptionTest()
        {
            Assert.DoesNotThrow(() => INMExecutableProcessStart());
        }
        protected void INMExecutableProcessStart()
        {
            Process process = new Process();
            process.StartInfo.FileName = Globals.currentDirectory + "INMTM_v3.exe";
            process.StartInfo.Arguments = "current_position.dat schiphol_grid2D.dat";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }


        [Test]
        public void INMExecutableTest()
        {
            Process process = new Process();
            process.StartInfo.FileName = Globals.currentDirectory + "INMTM_v3.exe";
            process.StartInfo.Arguments = "current_position.dat schiphol_grid2D.dat";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            output = output.Trim().ToLower();

            Assert.AreEqual("", output);
        }

    }

}