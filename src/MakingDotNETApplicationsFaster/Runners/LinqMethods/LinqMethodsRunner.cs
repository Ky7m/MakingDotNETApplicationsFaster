using System.Linq;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster.Runners.LinqMethods
{
    sealed class LinqMethodsRunner : IRunner
    {
        public void Run()
        {
            var array = Enumerable.Range(0, 100000).ToArray();

            new PerformanceTests
            {
                {i => { FirstOrDefault(array, i); }, "FirstOrDefault"},
                {i => { SingleOrDefault(array, i); }, "SingleOrDefault"}
            }.Run(10000);
        }

        int FirstOrDefault(int[] array, int i)
        {
            return array.FirstOrDefault(x => x == i);
        }
        int SingleOrDefault(int[] array, int i)
        {
            return array.SingleOrDefault(x => x == i);
        }
    }
}