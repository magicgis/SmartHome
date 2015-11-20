using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin {
    /// <summary>
    /// Helps managing function calls in multithreaded environments
    /// </summary>
    public sealed class Semaphore {
        /// <summary>
        /// A queue of waiting operations
        /// </summary>
        private Queue<object> _queue;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Semaphore() {
            _queue = new Queue<object>();
        }

        /// <summary>
        /// Tells the semaphore so wait until its the invokers turn
        /// </summary>
        public void Enqueue() {
            object o = new object();
            _queue.Enqueue(o);
            Wait(o);
        }

        /// <summary>
        /// Waits until its the specified objects turn
        /// </summary>
        /// <param name="obj">The waiting object</param>
        public void Wait(object obj) {
            while (_queue.ElementAt(0) != obj)
                System.Threading.Thread.Sleep(200);
        }

        /// <summary>
        /// Tells the semaphore that the current object is done with its work
        /// </summary>
        public void Dequeue() {
            _queue.Dequeue();
        }
    }
}
