using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ActiveObjectPattern
{
    /// <summary>
    /// Active Object is a concurrency pattern in which we try to separate the invocation of a method from its execution.
    /// Typically, an active object provides synchronous methods and executes the method calls in an asynchronous way. 
    /// An active object usually has its own thread of control.
    /// 
    /// The key elements in active object pattern are:
    /// Proxy(or Client Interface) - A public method provided by active object to clients.
    /// Dispatch Queue - A list of pending requests from clients.
    /// Scheduler - Determines the order to execute the requests.
    /// Result Handle(or Callback) - This allows the result to be obtained by proxy after a request is executed.
    /// </summary>
    class ActiveObject
    {
        public class MyTask
        {
            public int priority;
            public string name;
            public MyTask(string name, int priority)
            {
                this.name = name;
                this.priority = priority;
            }
        }

        //Thread safe first in first out collection
        ConcurrentQueue<MyTask> dispatchQueue = new ConcurrentQueue<MyTask>();

        public void AddCommand(string name, int priority)
        {
            dispatchQueue.Enqueue(new MyTask(name, priority));
        }
        public void Run()
        {
            while(!dispatchQueue.IsEmpty)
            {
                new Thread(() =>
                {
                    var isSuccess = dispatchQueue.TryDequeue(out var task);
                    Console.WriteLine("Executing: " + task.name);
                }).Start();
                Thread.Sleep(1000);
            }
        }
    }

   
}
