using System;
using System.Threading.Tasks;

namespace ThreadLocking
{
    internal interface IDialogWithUser
    {
        string AskUserName(int threadNumber);
        bool AskIsMan(int threadNumber);
        int AskAge(int threadNumber);
    }

    internal class DialogWithUser : IDialogWithUser
    {
        private static readonly object sync = new object();

        public string AskUserName(int threadNumber)
        {
            lock (sync)
            {
                Console.Write($"I'm thread #{threadNumber} ");
                Console.WriteLine("What is you name?");
                return Console.ReadLine();
            }
        }

        public bool AskIsMan(int threadNumber)
        {
            lock (sync)
            {
                Console.Write($"I'm thread #{threadNumber} ");
                Console.WriteLine("Write 'true' if you are a man.");
                return bool.Parse(Console.ReadLine());
            }
        }

        public int AskAge(int threadNumber)
        {
            lock (sync)
            {
                Console.Write($"I'm thread #{threadNumber} ");
                Console.WriteLine("What is you age?");
                return int.Parse(Console.ReadLine());
            }
        }
    }

    internal class EntryPoint
    {
        private static readonly IDialogWithUser dialog = new DialogWithUser();

        public static void Main(string[] args)
        {
            var tasks = new[]
            {
                Task.Run(() =>
                {
                    dialog.AskUserName(1);
                }),
                Task.Run(() =>
                {
                    dialog.AskIsMan(2);
                }),

                Task.Run(() =>
                {
                    dialog.AskAge(3);
                })
            };

            Task.WaitAll(tasks);
        }
    }
}