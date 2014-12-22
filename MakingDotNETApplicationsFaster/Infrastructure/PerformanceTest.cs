using System;
using System.Diagnostics;

namespace MakingDotNETApplicationsFaster.Infrastructure
{
    internal sealed class PerformanceTest
    {
        public static PerformanceTest Create(Action<int> iteration, string name)
        {
            return new PerformanceTest { Iteration = iteration, Name = name };
        }

        public Action<int> Iteration { get; set; }
        public string Name { get; set; }
        public Stopwatch Watch { get; set; }
    }
}
