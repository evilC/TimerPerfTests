using System;
using System.Timers;

namespace TimerPerfTests.Timeout
{
    /// <summary>
    /// Implements ITimeoutAction using a System.Timers.Timer
    /// Reset involves stopping and starting timer
    /// </summary>
    public class TimeoutTimer : ITimeoutAction
    {
        private readonly Timer _timer;
        private int _period;
        private readonly Action _action;

        public TimeoutTimer(Action action, int timeout = 10)
        {
            _action = action;
            _period = timeout;
            _timer = new Timer { Interval = _period, AutoReset = false};
            _timer.Elapsed += OnElapsed;
        }

        public void SetState(bool state)
        {
            if (state)
            {
                if (_timer.Enabled)
                {
                    _timer.Stop();
                }
                _timer.Start();
            }
            else if (_timer.Enabled)
            {
                _timer.Stop();
            }
        }

        public void SetTime(int timeout)
        {
            if (timeout == _period) return;
            var timerWasRunning = _timer.Enabled;
            if (timerWasRunning)
                SetState(false);
            _period = timeout;
            _timer.Interval = _period;
            if (timerWasRunning)
                SetState(true);
        }

        public void Dispose()
        {
            SetState(false);
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            _action();
        }
    }

}
