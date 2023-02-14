using PromoteIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoteIt.Entities
{
    public class MyQueue
    {
        public MyQueue() {
            Task.Run(() =>
        {
            while (true)
            {
                if (myQueue.Count > 0)
                {
                    LogItem item = myQueue.Dequeue();

                }
            }
        });
        }
        
        public static Queue<LogItem> myQueue = new Queue<LogItem>();

        public LogItem Pop()
        {
            if (myQueue.Count == 0) return null;
            return myQueue.Dequeue();
        }

        public void Push(LogItem newItem)
        {
            myQueue.Enqueue(newItem);
        }
    }
}
