using System;

namespace TimerPerfTests.Timeout
{
    /// <summary>
    /// Interface for a class to handle a one-time delayed action
    /// This class should support resetting of the timeout at a very fast rate (eg 1khz) with as low CPU usage as possible
    /// Example use-case: Detecting mouse at rest
    /// Start a 100ms timer, and each time the mouse sends a RawInput update (could be up to 1khz updates), reset the timeout.
    /// When timeout is hit, mouse is considered "stopped"
    /// CPU usage for construction is irrelevant
    /// </summary>
    public interface ITimeoutAction : IDisposable
    {
        /// <summary>
        /// Called to Start, Stop, or ReStart the timer
        /// CPU hit for all is important, should be as low as possible
        /// CPU hit for reset (passing true while timer already running) is most important
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        void SetState(bool state);

        /// <summary>
        /// Called to update timeout time, eg when user changes timeout setting in UI
        /// CPU hit not important
        /// </summary>
        /// <param name="timeout"></param>
        void SetTime(int timeout);
    }
}