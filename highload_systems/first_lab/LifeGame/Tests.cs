using NUnit.Framework;

namespace LifeGame
{
    [TestFixture]
    public class Tests
    {
        private GamePlayer gamePlayer;

        [SetUp]
        public void Setup()
        {
            gamePlayer = new GamePlayer(new CellNeighbourFactory());
        }

        [Test]
        public void When_One_Cell_Alive_Should_Kill_It()
        {
            var cells = new[] {Cell.Create(0, 0)}.ToHashSet();
            var nextStepCells = gamePlayer.MakeStep(cells);

            Assert.False(gamePlayer.IsHasNextStep);
            Assert.That(nextStepCells, Is.Empty);
        }

        [Test]
        public void When_Two_Cells_Alive_Should_Kill_Its()
        {
            var cells = new[] {Cell.Create(0, 0), Cell.Create(0, 1)}.ToHashSet();
            var nextStepCells = gamePlayer.MakeStep(cells);

            Assert.False(gamePlayer.IsHasNextStep);
            Assert.That(nextStepCells, Is.Empty);
        }

        [Test]
        public void When_Three_Cells_Alive_Should_Keep_Its()
        {
            var cells = new[] {Cell.Create(0, 0), Cell.Create(0, 1), Cell.Create(1, 1)}.ToHashSet();
            var expectedCells = new[] { Cell.Create(0, 0), Cell.Create(0, 1), Cell.Create(1, 1), Cell.Create(1,0)}.ToHashSet();

            var nextStepCells = gamePlayer.MakeStep(cells);

            Assert.That(gamePlayer.IsHasNextStep);
            Assert.That(nextStepCells, Is.EquivalentTo(expectedCells));
        }
    }
}
