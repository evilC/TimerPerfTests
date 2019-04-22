using System;
using System.Threading;
using System.Threading.Tasks;

namespace TimerPerfTests.Periodic
{
    /// <summary>
    /// Implements IPeriodicAction using a Task-based approach
    /// </summary>
    public class PeriodicTaskDelay : IPeriodicAction
    {
        private readonly Action _action;
        private readonly TimeSpan _period;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _task;

        public PeriodicTaskDelay(Action action, int period)
        {
            _action = action;
            _period = TimeSpan.FromMilliseconds(period);
        }

        public void SetState(bool state)
        {
            if (_task != null)
            {
                _cancellationTokenSource.Cancel();
                _task = null;
            }
            if (state)
            {
                _task = AsyncStart();
            }
        }

        public void Dispose()
        {
            SetState(false);
        }

        private async Task AsyncStart()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                await Task.Delay(_period, _cancellationTokenSource.Token);

                if (!_cancellationTokenSource.Token.IsCancellationRequested)
                    _action();
            }
        }
    }
}
