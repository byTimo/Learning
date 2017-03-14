using System.Collections.Generic;

namespace LifeGame
{
    public interface IPrinter
    {
        void Print(HashSet<Cell> aliveCells);
    }
}