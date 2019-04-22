using System;
using System.Diagnostics;
using System.Threading;
using TimerPerfTests.Periodic;

namespace TestApp
{
    public class PeriodicTest
    {
        private readonly int _period = 16;
        private int _count = 0;
        private int _lastTime = int.MaxValue;
        private readonly IPeriodicAction _testClass;

        // Fire off one of the tests in here
        public PeriodicTest()
        {
            // Tests Task.Delay-based solution
            _testClass = new PeriodicTaskDelay(OnElapsed, _period);

            // Tests thread and sleep combo
            //_testClass = new PeriodicThreadSleep(OnElapsed, _period);

            DoTest();
        }

        // Log out iteration count and timing
        private void OnElapsed()
        {
            var t = Environment.TickCount;
            var elapsed = t - _lastTime;
            Debug.WriteLine($"Fire {_count++} - {elapsed}");
            _lastTime = Environment.TickCount;
        }

        public void DoTest()
        {   // Start perf monitoring (of all threads) here
            _testClass.SetState(true);
            Thread.Sleep(1000);
            _testClass.SetState(false);
        }   // End perf monitoring here
    }
}