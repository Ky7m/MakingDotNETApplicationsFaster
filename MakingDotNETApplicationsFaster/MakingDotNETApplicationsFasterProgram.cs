using System;
using System.Collections.Generic;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster
{
    static class MakingDotNetApplicationsFasterProgram
    {
        static void Main()
        {
            if (DemoRunner.TryAddRunners(new Dictionary<Int16, IRunner>
            {
                {0, new AggressiveInliningRunner()},
                {1, new DotNetLoopPerformanceRunner()},
                {2, new JaggedArraysVersus2DArraysRunner()},
                {3, new DictionaryPerformanceRunner()},
                {4, new ExceptionHandlingPerformanceRunner()},
                {5, new ReplaceOptimizationRunner()},
                {6, new StructEqualityRunner()},
                {7, new ReadOnlyFieldsRunner()}
            }))
            {
                DemoRunner.Run(0);
            }
        }
    }
}
