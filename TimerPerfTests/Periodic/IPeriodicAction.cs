using System;

namespace TimerPerfTests.Periodic
{
    /// <summary>
    /// Interface for a class to handle a rapidly repeating action...
    /// ... in the order of 10ms
    /// CPU hit for constructing the class is irrelevant
    /// CPU overhead for handling the timing / firing the action is IMPORTANT
    /// </summary>
    public interface IPeriodicAction : IDisposable
    {
        /// <summary>
        /// Start or stop the timer
        /// CPU usage for this call is irrelevant
        /// </summary>
        /// <param name="state">true = start, false = stop</param>
        void SetState(bool state);
    }
}