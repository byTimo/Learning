using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Simulation.Lib;

namespace Simulation.Tests
{
    [TestFixture]
    public class RandomValueGeneratorTests
    {
        private const int TestSeed = 1;
        private RandomValueGenerator generator;
        private static readonly double[] ExpectedValue = {25.0D, 97.0D};

        [SetUp]
        public void SetUp()
        {
            generator = new RandomValueGenerator(TestSeed);
        }

        [Test]
        public void Generate_DimensionZero_EmptyRandomValue()
        {
            generator.Generate(0).Dimension.Should().Be(0);
        }

        [Test]
        public void Generate_DimensionOne_RandomValueWithOneRandomNumber()
        {
            generator.Generate(1)
                .Values.Select(x => Math.Round(x * 100))
                .Should()
                .HaveCount(1)
                .And.BeEquivalentTo(ExpectedValue.Take(1));
        }

        [Test]
        public void Generate_DimensionTwo_RandomValueWithSecondNumberReferFromFirst()
        {
            generator.Generate(2)
                .Values.Select(x => Math.Round(x * 100))
                .Should()
                .HaveCount(2)
                .And.BeEquivalentTo(ExpectedValue);
        }

        [Test]
        public void Generate_DimensionThree_ThirdNumberDiffSecond()
        {
            generator.Generate(3)
                .Values.Select(x => Math.Round(x * 100))
                .Should()
                .HaveCount(3)
                .And.BeEquivalentTo(ExpectedValue);
        }

        [Test]
        public void Generate_RandomValueLengthLessOrEqualToOne()
        {
            var values = generator.Generate(100).Values;
            Math.Sqrt(values.Sum(x => Math.Pow(x, 2.0))).Should().BeLessOrEqualTo(1.0);
        }
    }
}
