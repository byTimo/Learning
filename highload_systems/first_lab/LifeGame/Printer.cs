using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeGame
{
    public class Printer : IPrinter
    {
        private const char EmptyCell = ' ';
        private const char AliveCell = 'X';

        public void Print(HashSet<Cell> aliveCells)
        {
            Console.Clear();
            var minX = aliveCells.Min(x => x.X);
            var minY = aliveCells.Min(x => x.Y);
            var maxX = aliveCells.Max(x => x.X);
            var maxY = aliveCells.Max(x => x.Y);

            for (var y = minY; y < maxY; y++)
            {
                for(var x = minX; x < maxX; x++)
                {
                    Console.Write(aliveCells.Contains(Cell.Create(x, y)) ? AliveCell : EmptyCell);
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}