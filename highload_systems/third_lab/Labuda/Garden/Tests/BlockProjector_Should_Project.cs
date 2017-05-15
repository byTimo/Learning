using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Garden.Flowerbed.Model;
using Garden.Genetics;
using Garden.Utility.Drawing;
using NUnit.Framework;

namespace Garden.Tests
{
    [TestFixture]
    public class BlockProjector_Should_Project
    {
        private static readonly IBlockProjector projector = new BlockProjector();
        [Test]
        public void EmptyBlocks_EmpltyPairs()
        {
            projector.Project(Enumerable.Empty<KeyValuePair<GrowingPosition, PlantSegment>>(), 0).Should().BeEmpty();
        }

        [Test]
        public void OneSimpleBlockOnZeroBed_OnePairOnZeroBed()
        {
            projector.Project(new[] {new KeyValuePair<GrowingPosition, PlantSegment>(GrowingPosition.Create(0,0), PlantSegment.Seed) }, 0)
                .Should()
                .HaveCount(1)
                .And.Contain(new KeyValuePair<Position, PlantSegment>(new Position(0, 0), PlantSegment.Seed));
        }

        [Test]
        public void OneSimpleBlockOnFifthBed_OnePairOnFifthBed()
        {
            projector.Project(new[] {new KeyValuePair<GrowingPosition, PlantSegment>(GrowingPosition.Create(0,0), PlantSegment.Seed), }, 5)
                .Should()
                .HaveCount(1)
                .And.Contain(new KeyValuePair<Position, PlantSegment>(new Position(5, 0), PlantSegment.Seed));
        }
    }
}