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

    [TestFixture]
    public class IntegratedNoiseModelTest
    {
        protected bool completed = false;

        [Test]
        public void t0_processCanBeStartedTest()
        {
            Process proc = Process.Start(@"c:\windows\system32\cmd.exe");
            if (null == proc)
                Assert.Fail("Could not start process");
            Thread.Sleep(500);
            proc.Kill();
        }

        [Test]
        public void t1_INMExecutableFoundTest()
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
        public void t2_INMGridExists()
        {
            string gridPath = Globals.currentDirectory + "schiphol_grid2D.dat";
            Assert.True(File.Exists(gridPath), gridPath+" does not exist");
        }
        
        [Test]
        public void t3_INMPositionFileCanBeCreatedTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var aircraft = new Aircraft("GP7270", "wing");
            IntegratedNoiseModel noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while(!completed) { }

            string positionFile = Globals.currentDirectory + "current_position.dat";
            Assert.True(File.Exists(positionFile), positionFile + " does not exist");

            completed = false;
        }

        [Test]
        public void t4_INMExecutableTest()
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
        
        [Test]
        public void t5_INMFullTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var aircraft = new Aircraft("GP7270", "wing");
            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(INMCompleted);

            while(!completed) {}

            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            Assert.AreEqual(2, temporalGrid.GetNumberOfGrids());

            completed = false;
        }

        [Test]
        public void t6_INMFullTrajectoryTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var aircraft = new Aircraft("GP7270", "wing");
            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft, true);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            Assert.AreEqual(1, temporalGrid.GetNumberOfGrids());

            completed = false;
        }

        [Test]
        public void t7_INMTrajectoryFileCanBeCreatedTest()
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile(Globals.testdataDirectory + "test_track.dat");

            var aircraft = new Aircraft("GP7270", "wing");
            IntegratedNoiseModel noiseModel = new IntegratedNoiseModel(trajectory, aircraft, true);
            noiseModel.StartCalculation(INMCompleted);

            while (!completed) { }

            string positionFile = Globals.currentDirectory + "current_position.dat";
            Assert.True(File.Exists(positionFile), positionFile + " does not exist");

            completed = false;
        }

        private void INMCompleted()
        {
            completed = true;
        }
    }
}