using System.Linq;
using BenchmarkDotNet.Attributes;

namespace MakingDotNETApplicationsFaster.Runners.LinqMethods
{
    public class LinqMethodsRunner
    {
        private readonly int[] _array;
        public LinqMethodsRunner()
        {
            _array = Enumerable.Range(0, 100000).ToArray();
        }

        [Benchmark]
        public int FirstOrDefault()
        {
            return _array.FirstOrDefault(x => x == 100);
        }

        [Benchmark]
        public int SingleOrDefault()
        {
            return _array.SingleOrDefault(x => x == 100);
        }
    }
}