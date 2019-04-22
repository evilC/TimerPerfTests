namespace TestApp
{
    public class Program
    {
        // The period at which to fire the timer
        // Minimum Granularity of system timer is ~16ms, so no point in going below that

        public static void Main(string[] args)
        {
            //var pt = new PeriodicTest();
            var tt = new TimeoutTest();
        }
    }
}
