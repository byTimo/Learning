using FluentAssertions;
using Garden.Flowerbed;
using NUnit.Framework;

namespace Garden.Tests
{
    [TestFixture]
    public class Plant_Should
    {
        [Test]
        public void Have_Only_Seed_If_Never_Grow()
        {
            new Plant().Parts.Should().BeEquivalentTo(new[] {PlantPart.Seed});
        }

        [Test]
        public void Grow_In_Sqrout_After_First_Grow()
        {
            new Plant().Grow().Parts.Should().BeEquivalentTo(new[] {PlantPart.Seed, PlantPart.Sqrout});
        }

        [Test]
        public void Grow_In_Flower_With_Explicit_Probability()
        {
            new Plant(new[]{PlantPart.Sqrout}).Grow(1d).Parts.Should().BeEquivalentTo(new[] {PlantPart.Seed, PlantPart.Flower});
        }

        [Test]
        public void Grow_In_Flower_After_Grow_Without_Flower_With_Explicit_Probability()
        {
            new Plant().Grow(0).Grow(1).Parts.Should().BeEquivalentTo(new[] {PlantPart.Seed, PlantPart.Flower});
        }
    }
}