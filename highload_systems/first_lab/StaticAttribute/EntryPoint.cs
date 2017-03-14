using System;
using System.Threading;

namespace StaticAttribute
{
    class EntryPoint
    {
        private static bool IsAlive { get; set; } = true;

        static void Main(string[] args)
        {
            new Thread(() => ThreadCallback(0, 500, new ThreadStaticResourceProvider())).Start();
            new Thread(() => ThreadCallback(1, 300, new ThreadStaticResourceProvider())).Start();
            Console.ReadKey();

            Console.Clear();
            new Thread(() => ThreadCallback(0, 500, new TlsResourceProvider())).Start();
            new Thread(() => ThreadCallback(1, 300, new TlsResourceProvider())).Start();
            Console.ReadKey();
            IsAlive = false;
            Console.ReadKey();
        }

        private static void ThreadCallback(int threadNumber, int delay, IResourceProvider resource)
        {
            resource.InitResource();

            while (IsAlive)
            {
                if (resource.IsResourceEmpty())
                    break;

                resource.DicrementResource();
                Console.SetCursorPosition(0, threadNumber);
                Console.Write($"I have eaten resource. Now it's {resource}");
                Thread.Sleep(delay);
            }
            Console.SetCursorPosition(0, threadNumber);
            Console.Write("I'm end to eat!");
        }
    }

    internal class ThreadStaticResourceProvider : IResourceProvider
    {

        [ThreadStatic]
        private static int resource;

        public void InitResource()
        {
            resource = 100;
        }

        public bool IsResourceEmpty()
        {
            return resource == 0;
        }

        public void DicrementResource()
        {
            resource--;
        }

        public override string ToString()
        {
            return resource.ToString();
        }
    }

    internal interface IResourceProvider
    {
        void InitResource();
        bool IsResourceEmpty();
        void DicrementResource();
    }

    internal class TlsResourceProvider : IResourceProvider
    {
        private LocalDataStoreSlot slot;

        public void InitResource()
        {
            slot = Thread.AllocateDataSlot();
            Thread.SetData(slot, 100);
        }

        public bool IsResourceEmpty()
        {
            return (int) Thread.GetData(slot) == 0;
        }

        public void DicrementResource()
        {
            Thread.SetData(slot, ((int) Thread.GetData(slot)) - 1);
        }

        public override string ToString()
        {
            return Thread.GetData(slot).ToString();
        }
    }
}
