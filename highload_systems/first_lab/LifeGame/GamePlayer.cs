using System.Collections.Generic;
using System.Linq;

namespace LifeGame
{
    public class GamePlayer : IGamePlayer
    {
        private readonly ICellNeighbourFactory neighbourFactory;

        public GamePlayer(ICellNeighbourFactory neighbourFactory)
        {
            this.neighbourFactory = neighbourFactory;
        }

        public bool IsHasNextStep { get; private set; } = true;

        public HashSet<Cell> MakeStep(HashSet<Cell> aliveCells)
        {
            var nextStepCell = new HashSet<Cell>();

            var extendCells = aliveCells.SelectMany(c => neighbourFactory.ExtendCell(c));

            foreach (var cell in extendCells)
            {
                if (IsAlive(cell, aliveCells))
                    nextStepCell.Add(cell);
            }

            IsHasNextStep = nextStepCell.Count != 0;
            return nextStepCell;
        }

        private bool IsAlive(Cell cell, HashSet<Cell> checkingCells)
        {
            return checkingCells.Contains(cell) ? IsCellLive(cell, checkingCells) : IsCellBirth(cell, checkingCells);
        }

        private bool IsCellLive(Cell cell, HashSet<Cell> checkingCells)
        {
            var neighboursCount = neighbourFactory.GetNeighbours(cell).Count(checkingCells.Contains);
            return neighboursCount > 1 && neighboursCount < 4;
        }

        private bool IsCellBirth(Cell cell, HashSet<Cell> checkingCells)
        {
            var neighboursCount = neighbourFactory.GetNeighbours(cell).Count(checkingCells.Contains);
            return neighboursCount == 3;
        }
    }
}