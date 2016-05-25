using AircraftTrajectories.Models.Visualisation;

using NUnit.Framework;
using System.IO;

namespace AircraftTrajectories.NUnit.Tests.Models.Visualisation
{
    [TestFixture]
    public class LegendCreatorTest
    {
        [Test]
        public void LegendCreator()
        {
            LegendCreator creator = new LegendCreator();
            Assert.IsNotNull(creator);
            Assert.AreEqual("80", creator.max.ToString());
            Assert.AreEqual("65", creator.min.ToString());
        }

        [Test]
        public void OutputLegendImageTest()
        {
            LegendCreator creator = new LegendCreator();
            creator.OutputLegendImage();

            if (File.Exists("gradientImage.png"))
            {
                Assert.Pass();
            }

        }

        [Test]
        public void OutputLegendTitleTest()
        {
            LegendCreator creator = new LegendCreator();
            creator.OutputLegendTitle();

            if (File.Exists("titleImage.png"))
            {
                Assert.Pass();
            }

        }

    }
}
