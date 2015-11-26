using System.Collections.Generic;
using MakingDotNETApplicationsFaster.Infrastructure;
using MakingDotNETApplicationsFaster.Runners.AggressiveInlining;
using MakingDotNETApplicationsFaster.Runners.CompareStrings;
using MakingDotNETApplicationsFaster.Runners.DictionaryPerformance;
using MakingDotNETApplicationsFaster.Runners.DotNetLoopPerformance;
using MakingDotNETApplicationsFaster.Runners.ExceptionHandlingPerformance;
using MakingDotNETApplicationsFaster.Runners.JaggedArraysVersus2DArrays;
using MakingDotNETApplicationsFaster.Runners.LinqMethods;
using MakingDotNETApplicationsFaster.Runners.ReadOnlyFields;
using MakingDotNETApplicationsFaster.Runners.ReplaceOptimization;
using MakingDotNETApplicationsFaster.Runners.SIMD;
using MakingDotNETApplicationsFaster.Runners.StructEquality;
using Microsoft.Framework.ConfigurationModel;
using static System.Console;

namespace MakingDotNETApplicationsFaster
{
    public static  class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new Configuration();
            configuration.AddEnvironmentVariables();
            configuration.AddCommandLine(args);

            var runnersMap = new Dictionary<int, IRunner>
            {
                [0] = new AggressiveInliningRunner(),
                [1] = new DotNetLoopPerformanceRunner(),
                [2] = new JaggedArraysVersus2DArraysRunner(),
                [3] = new DictionaryPerformanceRunner(),
                [4] = new ExceptionHandlingPerformanceRunner(),
                [5] = new ReplaceOptimizationRunner(),
                [6] = new StructEqualityRunner(),
                [7] = new ReadOnlyFieldsRunner(),
                [8] = new CompareStringsRunner(),
                [9] = new SIMDRunner(),
                [10] = new LinqMethodsRunner()
            };

            if (!DemoRunner.TryAddRunners(runnersMap))
            {
                WriteLine("Cannot initialize tests.");
                return;
            }

            var maxRegisteredTestId = runnersMap.Count - 1;

            string testIdValue;
            if (configuration.TryGet("TestId", out testIdValue))
            {
                short testId;
               
                if (short.TryParse(testIdValue, out testId))
                {
                    if (runnersMap.ContainsKey(testId))
                    {
                        DemoRunner.Run(testId);
                    }
                    else
                    {
                        WriteLine($"{testIdValue} is not registered. Please specify correct number from 0 to {maxRegisteredTestId}.");
                    }
                }
                else
                {
                    WriteLine($"{testIdValue} is not correct value. Please specify correct number from 0 to {maxRegisteredTestId}.");
                }
            }
            else
            {
                DemoRunner.Run(maxRegisteredTestId);
            }
        }
    }
}
