using FluentAssertions;
using Garden.Flowerbed;
using Garden.Flowerbed.Model;
using Garden.Genetics;
using NUnit.Framework;

namespace Garden.Tests
{
    [TestFixture]
    public class Shrub_Should_Grow
    {
        private static IGrowable growedShrub;

        [SetUp]
        public void Setup()
        {
            growedShrub = new Shrub().Grow().Grow();
        }

        [Test]
        public void FirstlyBySqrout()
        {
            new Shrub().Grow().Segments.Should().EndWith(new PlantNode(PlantSegment.Sqrout, 1));
        }

        [Test]
        public void SecondlyBySqroutWithChangeBeforeToWood()
        {
            new Shrub().Grow()
                .Grow()
                .Segments.Should()
                .HaveCount(3)
                .And.HaveElementAt(1, new PlantNode(PlantSegment.Wood, 1))
                .And.HaveElementAt(2, new PlantNode(PlantSegment.Sqrout, 2));
        }

        [Test]
        public void FourthlyBySheet()
        {
            growedShrub.Grow().Segments.Should().HaveCount(4).And.EndWith(new PlantNode(PlantSegment.Sheet, 3));
        }

        [Test]
        public void SixthlyBySheetUnderPriveousSheet()
        {
            growedShrub.Grow().Grow().Segments.Should().HaveCount(5).And.EndWith(new PlantNode(PlantSegment.Sheet, 2));
        }
    }
}