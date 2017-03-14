using System;

namespace LifeGame
{
    public class EntryPoint
    {
        public static IGame Game = CreateGame();

        public static void Main()
        {
            Game.Init(300);
            Game.Start();
            Console.ReadKey();
        }

        private static IGame CreateGame()
        {
            return new Game(new GameInitializer(50,50), new GamePlayer(new CellNeighbourFactory()), new Printer());
        }
    }
}
