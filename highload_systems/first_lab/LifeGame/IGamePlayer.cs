using System.Collections.Generic;

namespace LifeGame
{
    public interface IGamePlayer
    {
        bool IsHasNextStep { get; }
        HashSet<Cell> MakeStep(HashSet<Cell> aliveCells);
    }
}