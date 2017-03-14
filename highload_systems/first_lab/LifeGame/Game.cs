using System.Collections.Generic;

namespace LifeGame
{
    public class Game : IGame
    {
        private readonly IGameInitializer initializer;
        private readonly IGamePlayer player;
        private readonly IPrinter printer;
        private HashSet<Cell> aliveCells;

        public Game(IGameInitializer initializer, IGamePlayer player, IPrinter printer)
        {
            this.initializer = initializer;
            this.player = player;
            this.printer = printer;
        }

        public void Init(int beginingCount = 100)
        {
            aliveCells = initializer.Init(beginingCount);
        }

        public void Start()
        {
            while (player.IsHasNextStep)
            {
                printer.Print(aliveCells);
                aliveCells = player.MakeStep(aliveCells);
            }
        }
    }
}