using FluentAssertions;
using Garden.Flowerbed;
using Garden.Flowerbed.Model;
using Garden.Genetics;
using NUnit.Framework;

namespace Garden.Tests
{
    [TestFixture]
    public class Flower_Should_Grow
    {
        [Test]
        public void StartFromSeed()
        {
            new Flower().Segments.Should().HaveCount(1).And.EndWith(new PlantNode(PlantSegment.Seed, 0));
        }

        [Test]
        public void FirstlyBySqrout()
        {
            new Flower().Grow().Segments.Should().HaveCount(2).And.EndWith(new PlantNode(PlantSegment.Sqrout, 1));
        }

        [Test]
        public void SecondlyByFlower()
        {
            new Flower().Grow()
                .Grow()
                .Segments.Should()
                .HaveCount(3)
                .And.EndWith(new PlantNode(PlantSegment.Flower, 2));
        }

        [Test]
        public void NotGrowingYet()
        {
            new Flower().Grow()
                .Grow()
                .Grow()
                .IsGrowed.Should()
                .BeTrue();
        }
    }
}