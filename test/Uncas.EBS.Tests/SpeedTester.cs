using System;
using System.Diagnostics;

namespace Uncas.EBS.Tests
{
    public delegate void FuncToSpeedTest();

    public class SpeedTester
    {
        public static void RunSpeedTest
            (string testTitle
            , FuncToSpeedTest func
            , int numberOfRepetitions)
        {
            // Runs once in order to eliminate initialization costs:
            func();

            // Runs the repetitive tests:
            long begin = DateTime.Now.Ticks;
            for (int i = 0; i < numberOfRepetitions; i++)
            {
                func();
            }

            long end = DateTime.Now.Ticks;

            // Writes some output:
            string message = string.Format("{0}: {1:N2}"
                , testTitle
                , TimeSpan.FromTicks(end - begin).TotalSeconds);
            Trace.WriteLine(message);
        }
    }
}