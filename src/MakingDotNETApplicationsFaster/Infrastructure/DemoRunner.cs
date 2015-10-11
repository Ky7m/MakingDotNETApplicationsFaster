using System.Collections.Generic;
using System.Linq;

namespace MakingDotNETApplicationsFaster.Infrastructure
{
    static class DemoRunner
    {
        static Dictionary<int, IRunner> Runners { get; set; }

        static DemoRunner()
        {
            Runners = new Dictionary<int, IRunner>();
        }

        public static bool TryAddRunner(int id, IRunner runner)
        {
            if (Runners.ContainsKey(id))
            {
                return false;
            }
            Runners.Add(id, runner);
            return true;
        }

        public static bool TryAddRunners(Dictionary<int, IRunner> runnersMap)
        {
            return runnersMap.Select(runner => TryAddRunner(runner.Key, runner.Value)).All(isSuccess => isSuccess);
        }

        public static bool Run(int runnerId)
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