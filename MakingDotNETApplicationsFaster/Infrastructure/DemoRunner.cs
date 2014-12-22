using System;
using System.Collections.Generic;
using System.Linq;

namespace MakingDotNETApplicationsFaster.Infrastructure
{
    internal static class DemoRunner
    {
        private static Dictionary<Int16, IRunner> Runners { get; set; }

        static DemoRunner()
        {
            Runners = new Dictionary<Int16, IRunner>();
        }

        public static bool TryAddRunner(Int16 id, IRunner runner)
        {
            if (Runners.ContainsKey(id))
            {
                return false;
            }
            Runners.Add(id, runner);
            return true;
        }

        public static bool TryAddRunners(Dictionary<Int16, IRunner> runnersMap)
        {
            return runnersMap.Select(runner => TryAddRunner(runner.Key, runner.Value)).All(isSuccess => isSuccess);
        }

        public static bool Run(Int16 runnerId)
        {
            IRunner runner;
            if (Runners.TryGetValue(runnerId, out runner))
            {
                runner.Run();
                return true;
            }
            return false;
        }
    }
}