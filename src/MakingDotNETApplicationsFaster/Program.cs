using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
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
            var config = DefaultConfig.Instance
                .With(new SlowestToFastestOrderProviderWithoutParameters())
                .RemoveBenchmarkFiles();

            BenchmarkRunner.Run<SerializersPerformanceRunner>(config);
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
