using System.Collections.Generic;

namespace LifeGame
{
    public interface IGameInitializer
    {
        HashSet<Cell> Init(int beginingAliveCellCount = 10);
    }
}