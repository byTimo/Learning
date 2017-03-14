using System.Collections.Generic;
using System.Linq;

namespace LifeGame
{
    public class CellNeighbourFactory : ICellNeighbourFactory
    {
        private static readonly int[] NeighbourIndexes = { -1, 0, 1 };

        public IEnumerable<Cell> ExtendCell(Cell cell)
        {
            return NeighbourIndexes.SelectMany(x => NeighbourIndexes.Select(y => Cell.Create(cell.X + x, cell.Y + y)));
        }

        public IEnumerable<Cell> GetNeighbours(Cell cell)
        {
            return ExtendCell(cell).Where(x => !x.Equals(cell));
        }
    }
}