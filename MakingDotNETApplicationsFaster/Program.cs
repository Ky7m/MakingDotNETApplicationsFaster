using BenchmarkDotNet.Running;
using MakingDotNETApplicationsFaster.Runners.CompareStrings;
using MakingDotNETApplicationsFaster.Runners.DictionaryPerformance;
using MakingDotNETApplicationsFaster.Runners.DotNetLoopPerformance;
using MakingDotNETApplicationsFaster.Runners.ExceptionHandlingPerformance;
using MakingDotNETApplicationsFaster.Runners.FastMemberPerformance;
using MakingDotNETApplicationsFaster.Runners.JaggedArraysVersus2DArrays;
using MakingDotNETApplicationsFaster.Runners.LinqMethods;
using MakingDotNETApplicationsFaster.Runners.ReadOnlyFields;
using MakingDotNETApplicationsFaster.Runners.ReplaceOptimization;
using MakingDotNETApplicationsFaster.Runners.SIMD;
using MakingDotNETApplicationsFaster.Runners.StructEquality;

namespace MakingDotNETApplicationsFaster
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[]
            {
                typeof(DotNetLoopPerformanceRunner),
                typeof(JaggedArraysVersus2DArraysRunner),
                typeof(DictionaryPerformanceRunner),
                typeof(ExceptionHandlingPerformanceRunner),
                typeof(ReplaceOptimizationRunner),
                typeof(StructEqualityRunner),
                typeof(ReadOnlyFieldsRunner),
                typeof(CompareStringsRunner),
                typeof(SIMDRunner),
                typeof(LinqMethodsRunner),
                typeof(FastMemberPerformanceRunner)
            });
            switcher.Run(args);
        }
    }
}
