using System;
using System.Diagnostics;
using System.Threading;
using TimerPerfTests.Timeout;

namespace TestApp
{
    public class TimeoutTest
    {
        private readonly int _period = 100;
        private readonly ITimeoutAction _testClass;
        private int _fireCount;

        public TimeoutTest()
        {
            // Tests Timer-based solution
            //_testClass = new TimeoutTimer(OnElapsed, _period);

            // Tests thread and sleep combo
            _testClass = new TimeoutThreadSleep(OnElapsed, _period);

            DoTest();
        }

        public void DoTest()
        {   // Start perf monitoring (of all threads) here
            _fireCount = 0;
            _testClass.SetState(true);
            // Rapidly reset the timeout
            for (var i = 0; i < 1000; i++)
            {
                _testClass.SetState(true);
                //Thread.Sleep(10);
            }
            Thread.Sleep(1000);
            if (_fireCount != 1)
            {
                throw new Exception($"Expecting 1 for fire count, but got {_fireCount}");
            }
        }   // End perf monitoring here

        private void OnElapsed()
        {
            _fireCount++;
            Debug.WriteLine($"Timeout fired (#{_fireCount}) @ {Environment.TickCount}");
        }
    }
}
