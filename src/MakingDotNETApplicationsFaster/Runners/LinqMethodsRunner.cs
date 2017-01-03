using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace MakingDotNETApplicationsFaster.Runners
{
    [Config(typeof(CoreConfig))]
    public class LinqMethodsRunner
    {
        private readonly int[] _array;
        private readonly IEnumerable<int> _range;

        public LinqMethodsRunner()
        {
            _range = Enumerable.Range(0, 100000);
            _array = _range.ToArray();
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

        [Benchmark]
        public int[] ToArray()
        {
            return _range.ToArray();
        }

        [Benchmark]
        public List<int> ToList()
        {
            return _range.ToList();
        }

        [Benchmark]
        public bool Length()
        {
            return _array.Length > 0;
        }

        [Benchmark]
        public bool Any()
        {
            return _array.Any();
        }

        [Benchmark]
        public bool Count()
        {
            return _array.Count() > 0;
        }
    }
}