using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Container
{
    public enum Color
    {
        Red,
        Yellow,
        Gray,
        Green,
        Brown,
        White,
        Black,
        Blue
    }

    public class BollContainer : ICloneable
    {
        private readonly List<Color> bolls;

        public BollContainer() : this(Enumerable.Empty<Color>())
        {
        }

        public BollContainer(IEnumerable<Color> expectedBolls)
        {
            bolls = new List<Color>(expectedBolls);
        }

        public BollContainer(BollContainer another)
        {
            bolls = new List<Color>(another.Bolls);
        }

        public Color[] Bolls => bolls.ToArray();

        public BollContainer Add(Color boll)
        {
            bolls.Add(boll);
            return this;
        }

        public BollContainer Add(IEnumerable<Color> bollCollection)
        {
            bolls.AddRange(bollCollection);
            return this;
        }

        public object Clone()
        {
            return new BollContainer(this);
        }

        public static BollContainer operator +(BollContainer first, BollContainer second)
        {
            return new BollContainer(first.Bolls.Concat(second.Bolls));
        }

        public static BollContainer operator -(BollContainer fist, BollContainer second)
        {
            var secondBolls = second.Bolls;
            return new BollContainer(fist.Bolls.Where(x => !secondBolls.Contains(x)));
        }

        public bool Contains(BollContainer anotherContainer)
        {
            return anotherContainer.Bolls.All(x => bolls.Contains(x));
        }

        public BollContainer Clear()
        {
            bolls.Clear();
            return this;
        }

        public Color Extract(Color boll)
        {
            if (!bolls.Contains(boll))
                throw new ArgumentException("Нельзя изять несуществующий шар");

            bolls.Remove(boll);
            return boll;
        }

        public Color[] Extract(int count)
        {
            if(bolls.Count < count)
                throw new ArgumentException("Нельзя изять столько шаров");

            var extractedBolls = bolls.Take(count).ToArray();
            bolls.RemoveRange(0, count);
            return extractedBolls;
        }
    }

    [TestFixture]
    public class BollContainerShoud
    {
        private BollContainer container;

        [SetUp]
        public void Setup()
        {
            container = new BollContainer();
        }

        [Test]
        public void GetEmptyArrayWhenIsEmpty()
        {
            container.Bolls.Should().BeEmpty();
        }

        [Test]
        public void GetBollAfterThatIsAdded()
        {
            container.Add(Color.Red);

            container.Bolls.Should().BeEquivalentTo(new[] {Color.Red});
        }

        [Test]
        public void CreatedWithGivenBolls()
        {
            var expectedBolls = new[] {Color.Red, Color.Gray, Color.White};
            var anotherContainer = new BollContainer(expectedBolls);

            anotherContainer.Bolls.Should().BeEquivalentTo(expectedBolls);
        }

        [Test]
        public void CreatedByAnotherContainer()
        {
            container.Add(Color.Red).Add(Color.Blue);

            new BollContainer(container).Bolls.Should().BeEquivalentTo(container.Bolls).And.NotBeSameAs(container.Bolls);
        }

        [Test]
        public void AddAnotherContainerBolls()
        {
            var expectedBolls = new[] {Color.Red, Color.Gray, Color.Blue, Color.Red};
            container.Add(Color.Red).Add(Color.Gray);
            var anotherContainer = new BollContainer(new[] {Color.Blue, Color.Red});

            (container + anotherContainer).Bolls.Should().BeEquivalentTo(expectedBolls);
        }

        [Test]
        public void SubstractAnotherContainerBolls()
        {
            var expectedBolls = new[] {Color.Gray};
            container.Add(Color.Red).Add(Color.Gray);
            var anotherContainer = new BollContainer(new[] {Color.Blue, Color.Red});

            (container - anotherContainer).Bolls.Should().BeEquivalentTo(expectedBolls);
        }

        [Test]
        public void ContainsAnotherContainerWhenIsContainAllItsBolls()
        {
            container.Add(new[] {Color.Black, Color.Blue, Color.Gray, Color.White});
            var anotherContainer = new BollContainer(new[] {Color.Blue, Color.White});

            container.Contains(anotherContainer).Should().BeTrue();
        }

        [Test]
        public void NotContainsAnotherContainerWhenIsContainNotAllItsBolls()
        {
            container.Add(new[] {Color.Black, Color.Blue, Color.Gray});
            var anotherContainer = new BollContainer(new[] {Color.Blue, Color.White});

            container.Contains(anotherContainer).Should().BeFalse();
        }

        [Test]
        public void ClearAllBolls()
        {
            container.Add(Color.Blue).Clear().Bolls.Should().BeEmpty();
        }

        [Test]
        public void ExtractBollFromContainer()
        {
            var expectedBolls = new[] {Color.Red, Color.White};
            container.Add(expectedBolls).Add(Color.Black);

            container.Extract(Color.Black);

            container.Bolls.Should().BeEquivalentTo(expectedBolls);
        }

        [Test]
        public void ThrowArgumentExceptionWhenOneExtractBadBoll()
        {
            container.Invoking(x => x.Extract(Color.Red))
                .ShouldThrow<ArgumentException>()
                .WithMessage("Нельзя изять несуществующий шар");
        }

        [Test]
        public void ExtractBollsByCount()
        {
            container.Add(new[] {Color.Black, Color.Black, Color.Blue});

            container.Extract(2);

            container.Bolls.Should().HaveCount(1);
        }

        [Test]
        public void ThrowArgaumentExceptionWhenOnExctractBadCount()
        {
            container.Invoking(x => x.Extract(100))
                .ShouldThrow<ArgumentException>()
                .WithMessage("Нельзя изять столько шаров");
        }
    }
}

