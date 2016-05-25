using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using NUnit.Framework;

namespace AircraftTrajectories.NUnit.Tests.IntegratedNoiseModel
{
    using AircraftTrajectories.Models.IntegratedNoiseModel;
    using AircraftTrajectories.Models.TemporalGrid;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    [TestFixture]
    public class IntegratedNoiseModelTest
    {

        [Test]
        public void INMExecutableFoundTest()
        {
            Assert.DoesNotThrow(() => INMExecutableProcessStart(), "the INM executable cannot be executed");
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
        public void INMGridExists()
        {
            string gridPath = Globals.currentDirectory + "schiphol_grid2D.dat";
            Assert.True(File.Exists(gridPath), gridPath+" does not exist");
        }


        [Test]
        public void INMPositionFileCanBeCreatedTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var aircraft = new Aircraft("GP7270", "wing");
            IntegratedNoiseModel noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(positionFileTestCompleted);
        }
        private void positionFileTestCompleted()
        {
            string positionFile = Globals.currentDirectory + "current_position.dat";
            Assert.True(File.Exists(positionFile), positionFile + " does not exist");
        }


        [Test]
        [RequiresThread]
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


        IntegratedNoiseModel noiseModel;
        [Test]
        [RequiresThread]
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
        public void processCanBeStartedTest()
        {
            Process proc = Process.Start(@"c:\windows\system32\cmd.exe");
            if (null == proc)
                Assert.Fail("Could not start process");
            Thread.Sleep(5000);
            proc.Kill();
        }

    }

}