using System.Collections.Generic;

namespace LifeGame
{
    public interface ICellNeighbourFactory
    {
        IEnumerable<Cell> ExtendCell(Cell cell);

        IEnumerable<Cell> GetNeighbours(Cell cell);
    }
}