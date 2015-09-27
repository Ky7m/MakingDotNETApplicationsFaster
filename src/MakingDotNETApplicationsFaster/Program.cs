using System.Collections.Generic;
using MakingDotNETApplicationsFaster.Infrastructure;
using Microsoft.Framework.ConfigurationModel;

namespace MakingDotNETApplicationsFaster
{
    public class Program
    {
        public void Main(string[] args)
        {
            // load configuration
            var configuration = new Configuration();
            configuration.AddEnvironmentVariables();
            configuration.AddCommandLine(args);
      
            if (DemoRunner.TryAddRunners(new Dictionary<short, IRunner>
            {
                {0, new AggressiveInliningRunner()},
                {1, new DotNetLoopPerformanceRunner()},
                {2, new JaggedArraysVersus2DArraysRunner()},
                {3, new DictionaryPerformanceRunner()},
                {4, new ExceptionHandlingPerformanceRunner()},
                {5, new ReplaceOptimizationRunner()},
                {6, new StructEqualityRunner()},
                {7, new ReadOnlyFieldsRunner()},
                {8, new CompareStringsRunner()},
                {9, new SIMDRunner()}
            }))
            {
                DemoRunner.Run(0);
            }
        }
    }
}
