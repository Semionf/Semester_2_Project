using ProLobby.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLobby.Entities
{
    public class MyQueue
    {
        private Queue<LogItem> myQueue = new Queue<LogItem>();

        public LogItem Pop()
        {
            if (myQueue.Count == 0) return null;
            return myQueue.Dequeue();
        }

        public void Push(LogItem newItem)
        {
            this.myQueue.Enqueue(newItem);
        }
    }
}
