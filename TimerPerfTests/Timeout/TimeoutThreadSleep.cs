using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimerPerfTests.Timeout
{
    /// <summary>
    /// Implements ITimeoutAction using a crude thread with sleep loop, checking Environment.Tickcount
    /// Probvably very cheap to reset timeout
    /// </summary>
    public class TimeoutThreadSleep : ITimeoutAction
    {
        private readonly Action _action;
        private int _period;
        private Thread _thread;
        private int _timeoutTime = int.MaxValue;

        public TimeoutThreadSleep(Action action, int period)
        {
            _action = action;
            _period = period;
        }

        public void SetState(bool state)
        {
            if (state && (_thread != null && _thread.IsAlive))
            {
                SetTimeout();
                return;
            }

            if (state)
            {
                _thread = new Thread(cb => _action());
                _thread.Start();
            }
            else
            {
                _thread.Abort();
                _thread.Join();
            }
        }

        public void SetTime(int timeout)
        {
            _period = timeout;
            SetTimeout();
        }

        private void SetTimeout()
        {
            _timeoutTime = Environment.TickCount + _period;
        }

        private void WatchThread()
        {
            while (Environment.TickCount < _timeoutTime)
            {
                Thread.Sleep(10);
            }

            _action();
        }

        public void Dispose()
        {
            SetState(false);
        }
    }
}
