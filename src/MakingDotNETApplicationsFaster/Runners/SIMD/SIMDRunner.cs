using System;
using System.Numerics;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster.Runners.SIMD
{
    sealed class SIMDRunner : IRunner
    {
        public void Run()
        {
            var rand = new Random();
            const int size = 1024;

            var a = new double[size];
            var b = new double[size];
            for (var i = 0; i < size; ++i)
            {
                a[i] = rand.NextDouble();
                b[i] = rand.NextDouble();
            }

            new PerformanceTests
            {
                {_ => NativeImplementation(a, b), "NativeImplementation"},
                {_ => NumericsVectorImplementation(a, b), "NumericsVectorImplementation"}
            }.Run(10000000);
        }

        static double NativeImplementation(double[] a, double[] b)
        {
            var dp = 0.0;
            for (var i = 0; i < a.Length; ++i)
            {
                dp += a[i] + b[i];
            }
            return dp;
        }

        static double NumericsVectorImplementation(double[] a, double[] b)
        {
            var dp = 0.0;
            var vectorSize = Vector<float>.Count;
            for (var i = 0; i < a.Length; i += vectorSize)
            {
                var va = new Vector<double>(a, i);
                var vb = new Vector<double>(b, i);
                dp += Vector.Dot(va, vb);
            }
            return dp;
        }
    }
}