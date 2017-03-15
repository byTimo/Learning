using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ResetEvents
{
    internal class UserInfo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsMan { get; set; }

        public override string ToString() => $"{Name} - {(IsMan ? "man" : "woman")} - {Age}";

    }

    internal class EntryPoint
    {
        private static UserInfo[] infos =
        {
            new UserInfo {Name = "Andrey", Age = 22, IsMan = true},
            new UserInfo {Name = "Dima", Age = 22, IsMan = true},
            new UserInfo {Name = "Sveta", Age = 21, IsMan = false},
            new UserInfo {Name = "Vadim", Age = 22, IsMan = true},
            new UserInfo {Name = "Zhana", Age = 24, IsMan = false}
        };

        public static void Main(string[] args)
        {
            using (var resetEvent = new ManualResetEvent(false))
            {
                var tasks = infos.Select(x => Task.Run(() =>
                {
                    resetEvent.WaitOne();
                    Console.WriteLine(x.ToString());
                })).ToArray();

                Console.WriteLine("I'm a main thread. After 2 seconds I set reset event.");
                Task.Delay(2000).Wait();
                resetEvent.Set();
                Task.WaitAll(tasks);
            }
        }
    }
}