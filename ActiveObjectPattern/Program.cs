
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ActiveObjectPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var activeObject = new ActiveObject();

            Task.Factory.StartNew(()=> { activeObject.AddCommand("1", 1); });
            Task.Factory.StartNew(() => { activeObject.AddCommand("2", 2); });
            Task.Factory.StartNew(() => { activeObject.AddCommand("3", 3); });
            Thread.Sleep(1000);

            activeObject.Run();

            Console.ReadLine();
        }
    }
}
