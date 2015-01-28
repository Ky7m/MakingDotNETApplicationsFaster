using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MakingDotNETApplicationsFaster.Infrastructure
{
    internal sealed class PerformanceTests : List<PerformanceTest>
    {
        public void Add(Action<int> iteration, string name)
        {
            Add(PerformanceTest.Create(iteration, name));
        }

        public void Run(int iterations)
        {
            // warmup 
            foreach (var test in this)
            {
                test.Iteration(iterations + 1);
                test.Watch = new Stopwatch();
                test.Watch.Reset();
            }

            var rand = new Random();
            for (int i = 1; i <= iterations; i++)
            {
                foreach (var test in this.OrderBy(ignore => rand.Next()))
                {
                    test.Watch.Start();
                    test.Iteration(i);
                    test.Watch.Stop();
                }
            }


            var orderedByElapsedTime = this.OrderBy(t => t.Watch.ElapsedMilliseconds).ToList();
            var bestTest = orderedByElapsedTime.First();
            var bestTimeinMilliseconds = bestTest.Watch.ElapsedMilliseconds;
            var millisecondsCountInOnePercent = bestTimeinMilliseconds / 100.0;
            foreach (var test in orderedByElapsedTime)
            {
                var currentTime = test.Watch.ElapsedMilliseconds;
                var percentage = (currentTime - bestTimeinMilliseconds) / millisecondsCountInOnePercent;

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Write(currentTime + "ms");
                Console.ResetColor();
                Console.Write("\t");


                if (percentage > 0.0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("(+" + percentage.ToString("F") + "%)");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("(best result)");
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t");
                Console.Write(test.Name);

                Console.WriteLine(Environment.NewLine);
                Console.ResetColor();
            }
        }
    }
}