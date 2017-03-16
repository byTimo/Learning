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

    public class BallContainer : ICloneable
    {
        private readonly List<Color> balls;

        public BallContainer() : this(Enumerable.Empty<Color>())
        {
        }

        public BallContainer(IEnumerable<Color> expectedBalls)
        {
            balls = new List<Color>(expectedBalls);
        }

        public BallContainer(BallContainer another)
        {
            balls = new List<Color>(another.Balls);
        }

        public Color[] Balls => balls.ToArray();

        public BallContainer Add(Color boll)
        {
            balls.Add(boll);
            return this;
        }

        public BallContainer Add(IEnumerable<Color> bollCollection)
        {
            balls.AddRange(bollCollection);
            return this;
        }

        public object Clone()
        {
            return new BallContainer(this);
        }

        public static BallContainer operator +(BallContainer first, BallContainer second)
        {
            return new BallContainer(first.Balls.Concat(second.Balls));
        }

        public static BallContainer operator -(BallContainer fist, BallContainer second)
        {
            var secondBolls = second.Balls;
            return new BallContainer(fist.Balls.Where(x => !secondBolls.Contains(x)));
        }

        public bool Contains(BallContainer anotherContainer)
        {
            return anotherContainer.Balls.All(x => balls.Contains(x));
        }

        public BallContainer Clear()
        {
            balls.Clear();
            return this;
        }

        public Color Extract(Color boll)
        {
            if (!balls.Contains(boll))
                throw new ArgumentException("Нельзя изять несуществующий шар");

            balls.Remove(boll);
            return boll;
        }

        public Color[] Extract(int count)
        {
            if(balls.Count < count)
                throw new ArgumentException("Нельзя изять столько шаров");

            var extractedBolls = balls.Take(count).ToArray();
            balls.RemoveRange(0, count);
            return extractedBolls;
        }
    }

    [TestFixture]
    public class BollContainerShoud
    {
        private BallContainer container;

        [SetUp]
        public void Setup()
        {
            container = new BallContainer();
        }

        [Test]
        public void GetEmptyArrayWhenIsEmpty()
        {
            container.Balls.Should().BeEmpty();
        }

        [Test]
        public void GetBollAfterThatIsAdded()
        {
            container.Add(Color.Red);

            container.Balls.Should().BeEquivalentTo(new[] {Color.Red});
        }

        [Test]
        public void CreatedWithGivenBolls()
        {
            var expectedBolls = new[] {Color.Red, Color.Gray, Color.White};
            var anotherContainer = new BallContainer(expectedBolls);

            anotherContainer.Balls.Should().BeEquivalentTo(expectedBolls);
        }

        [Test]
        public void CreatedByAnotherContainer()
        {
            container.Add(Color.Red).Add(Color.Blue);

            new BallContainer(container).Balls.Should().BeEquivalentTo(container.Balls).And.NotBeSameAs(container.Balls);
        }

        [Test]
        public void AddAnotherContainerBolls()
        {
            var expectedBalls = new[] {Color.Red, Color.Gray, Color.Blue, Color.Red};
            container.Add(Color.Red).Add(Color.Gray);
            var anotherContainer = new BallContainer(new[] {Color.Blue, Color.Red});

            (container + anotherContainer).Balls.Should().BeEquivalentTo(expectedBalls);
        }

        [Test]
        public void SubstractAnotherContainerBolls()
        {
            var expectedBalls = new[] {Color.Gray};
            container.Add(Color.Red).Add(Color.Gray);
            var anotherContainer = new BallContainer(new[] {Color.Blue, Color.Red});

            (container - anotherContainer).Balls.Should().BeEquivalentTo(expectedBalls);
        }

        [Test]
        public void ContainsAnotherContainerWhenIsContainAllItsBolls()
        {
            container.Add(new[] {Color.Black, Color.Blue, Color.Gray, Color.White});
            var anotherContainer = new BallContainer(new[] {Color.Blue, Color.White});

            container.Contains(anotherContainer).Should().BeTrue();
        }

        [Test]
        public void NotContainsAnotherContainerWhenIsContainNotAllItsBolls()
        {
            container.Add(new[] {Color.Black, Color.Blue, Color.Gray});
            var anotherContainer = new BallContainer(new[] {Color.Blue, Color.White});

            container.Contains(anotherContainer).Should().BeFalse();
        }

        [Test]
        public void ClearAllBolls()
        {
            container.Add(Color.Blue).Clear().Balls.Should().BeEmpty();
        }

        [Test]
        public void ExtractBollFromContainer()
        {
            var expectedBalls = new[] {Color.Red, Color.White};
            container.Add(expectedBalls).Add(Color.Black);

            container.Extract(Color.Black);

            container.Balls.Should().BeEquivalentTo(expectedBalls);
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

            container.Balls.Should().HaveCount(1);
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