using System;
using System.Threading;

namespace TimerPerfTests.Periodic
{
    /// <summary>
    /// Implements IPeriodicAction using a dirty thread with an infinite loop and Thread.Sleep
    /// </summary>
    public class PeriodicThreadSleep : IPeriodicAction
    {
        private readonly Action _action;
        private readonly int _period;
        private Thread _timer;

        public PeriodicThreadSleep(Action action, int period)
        {
            _action = action;
            _period = period;
        }

        public void SetState(bool state)
        {
            if (_timer != null)
            {
                _timer.Abort();
                _timer.Join();
                _timer = null;
            }

            if (!state) return;
            _timer = new Thread(TimerThread);
            _timer.Start();
        }

        public void Dispose()
        {
            SetState(false);
        }

        private void TimerThread()
        {
            while (true)
            {
                _action();
                Thread.Sleep(_period);
            }
        }

    }
}
