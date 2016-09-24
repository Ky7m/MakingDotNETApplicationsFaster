using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using MakingDotNETApplicationsFaster.Runners;

namespace MakingDotNETApplicationsFaster
{
    public class Program
    {
        public static void Main()
        {
            var config = ManualConfig.CreateEmpty()
                .With(
                    Job.Dry.With(Runtime.Core)
                        .With(Platform.X64)
                        .With(Jit.RyuJit)
                        .With(Mode.Throughput)
                        .WithWarmupCount(1)
                        .WithTargetCount(10))
                .With(DefaultConfig.Instance.GetLoggers().ToArray())
                .With(PropertyColumn.Method, PropertyColumn.Runtime, PropertyColumn.Platform, PropertyColumn.Jit,
                    StatisticColumn.Median, StatisticColumn.StdDev, StatisticColumn.Max, StatisticColumn.Min)
                .With(new SlowestToFastestOrderProviderWithoutParameters())
                .RemoveBenchmarkFiles();

            BenchmarkRunner.Run<CompareStringsRunner>(config);
        }

        private class SlowestToFastestOrderProviderWithoutParameters : IOrderProvider
        {
            public IEnumerable<Benchmark> GetExecutionOrder(Benchmark[] benchmarks) => benchmarks;

            public IEnumerable<Benchmark> GetSummaryOrder(Benchmark[] benchmarks, Summary summary) =>
                from benchmark in benchmarks
                orderby summary[benchmark]?.ResultStatistics?.Median ascending
                select benchmark;

            public string GetGroupKey(Benchmark benchmark, Summary summary) => null;
        }
    }
}
