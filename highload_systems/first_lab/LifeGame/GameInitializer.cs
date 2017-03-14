using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeGame
{
    public class GameInitializer : IGameInitializer
    {
        private readonly Cell minCell;
        private readonly Cell maxCell;

        public GameInitializer() : this(20,20) { }

        public GameInitializer(int x, int y) : this(0, x, 0, y) { }

        public GameInitializer(int minX, int maxX, int minY, int maxY)
        {
            minCell = Cell.Create(minX, minY);
            maxCell = Cell.Create(maxX, maxY);
        }

        public HashSet<Cell> Init(int beginingAliveCellCount = 10)
        {
            var rand = new Random();
            return
                Enumerable.Range(0, beginingAliveCellCount)
                    .Select(x => Cell.Create(rand.Next(minCell.X, maxCell.X), rand.Next(minCell.Y, maxCell.Y)))
                    .ToHashSet();
        }
    }
}