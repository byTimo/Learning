using FluentAssertions;
using Garden.Flowerbed;
using Garden.Flowerbed.Model;
using Garden.Genetics;
using NUnit.Framework;

namespace Garden.Tests
{
    [TestFixture]
    public class Gress_Should_Grow
    {
        [Test]
        public void StartFromSeed()
        {
            new Gress().Segments.Should().HaveCount(1).And.EndWith(new PlantNode(PlantSegment.Seed, 0));
        }

        [Test]
        public void FirstlyBySqrout()
        {
            new Gress().Grow().Segments.Should().HaveCount(2).And.EndWith(new PlantNode(PlantSegment.Sqrout, 1));
        }

        [Test]
        public void NoGrowYet()
        {
            new Gress().Grow()
                .Grow()
                .Segments.Should()
                .BeEquivalentTo(new PlantNode(PlantSegment.Seed, 0), new PlantNode(PlantSegment.Sqrout, 1));
        }
    }
}