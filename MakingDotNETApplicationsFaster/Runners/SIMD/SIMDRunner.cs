using System;
using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace MakingDotNETApplicationsFaster.Runners.SIMD
{
    public class SIMDRunner
    {
        private const int size = 1024;

        private readonly double[] _a = new double[size];
        private readonly double[] _b = new double[size];

        public SIMDRunner()
        {
            var rand = new Random();
            for (var i = 0; i < size; ++i)
            {
                _a[i] = rand.NextDouble();
                _b[i] = rand.NextDouble();
            }
        }

        [Benchmark]
        public double NativeImplementation()
        {
            var dp = 0.0;
            for (var i = 0; i < _a.Length; ++i)
            {
                dp += _a[i] + _b[i];
            }
            return dp;
        }

        [Benchmark]
        public double NumericsVectorImplementation()
        {
            var dp = 0.0;
            var vectorSize = Vector<float>.Count;
            for (var i = 0; i < _a.Length; i += vectorSize)
            {
                var va = new Vector<double>(_a, i);
                var vb = new Vector<double>(_b, i);
                dp += Vector.Dot(va, vb);
            }
            return dp;
        }
    }
}