using FluentAssertions;
using Garden.Flowerbed;
using Garden.Genetics;
using NUnit.Framework;

namespace Garden.Tests
{
    [TestFixture]
    public class Gress_Should
    {
        private readonly Dna dna = new Dna(1);
        [Test]
        public void Grow_When_No_Max_Length_By_Dna()
        {
            new Gress(dna).Grow().Segments.Should().HaveCount(2);
        }

        [Test]
        public void Not_Grow_When_Max_Length_Achived()
        {
            new Gress(dna).Grow().Grow().Segments.Should().HaveCount(2);
        }
    }
}