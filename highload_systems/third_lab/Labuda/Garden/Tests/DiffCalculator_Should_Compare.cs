using System;
using FluentAssertions;
using Garden.Drawing;
using Garden.Flowerbed;
using NUnit.Framework;

namespace Garden.Tests
{
    [TestFixture]
    public class DiffCalculator_Should_Compare
    {
        private static readonly IDiffCalculator calculator = new DiffCalculator();

        [Test]
        public void GiveSamePlant_EmptyFirstPlants()
        {
            var plants = new IGrowable[] {new Gress()};

            var differents = calculator.Compare(Array.Empty<IGrowable>(), plants);

            differents.Should()
                .HaveCount(1)
                .And.Contain(new Position(0, 0), PlantSegment.Seed);
        }

        [Test]
        public void GiveNewPlant_AddedNewPlant()
        {
            var first = new IGrowable[] {new Gress()};
            var second = new IGrowable[] {new Gress(), new Gress()};

            calculator.Compare(first, second)
                .Should()
                .HaveCount(1)
                .And.Contain(new Position(1, 0), PlantSegment.Seed);
        }

        [Test]
        public void GiveGrowedSegment_PlantGrowed()
        {
            var first = new IGrowable[] {new Gress()};
            var second = new[] {new Gress().Grow()};

            calculator.Compare(first, second)
                .Should()
                .HaveCount(1)
                .And.Contain(new Position(0, 1), PlantSegment.Sqrout);
        }
    }
}