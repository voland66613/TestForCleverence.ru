using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Test2
{
    internal class AsyncCaller
    {
        private EventHandler h;

        private bool isCompleted = false; 

        public AsyncCaller(EventHandler h) => this.h = h;

        internal bool Invoke(int v, object p, EventArgs empty)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            h.BeginInvoke(p, empty, new AsyncCallback(AsyncCompleted), p);

            while (stopwatch.ElapsedMilliseconds <= v)
            {
                Thread.SpinWait(100);
            }

            return isCompleted;
        }

        private void AsyncCompleted(IAsyncResult resObj) => isCompleted = true;
    }
}