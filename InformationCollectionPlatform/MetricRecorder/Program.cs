using MetricRecorder01;
using System;

namespace MetricRecorder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("---------------Metric Recorder: logistic information---------------");
            Console.WriteLine($"UtilityHelper.GetLibraryName(): {UtilityHelper.GetLibraryName()}");
            Console.ReadKey();
        }
    }
}
