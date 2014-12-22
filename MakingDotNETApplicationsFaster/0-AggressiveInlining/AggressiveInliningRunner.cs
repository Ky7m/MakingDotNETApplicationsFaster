using System.Runtime.CompilerServices;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster
{
    internal sealed class AggressiveInliningRunner : IRunner
    {
        public void Run()
        {
            new PerformanceTests
            {
                {i => { SmallMethod(i, i + 3); }, "SmallMethod"},
                {i => { NoInliningLargeMethod(i, i + 3); }, "NoInliningLargeMethod"},
                {i => { InliningLargeMethod(i, i + 3); }, "InliningLargeMethod"}
            }.Run(10000000);
        }

        private static int SmallMethod(int i, int j)
        {
            if (i > j) return i + j;
            return i + 2*j - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int InliningLargeMethod(int i, int j)
        {
            if (i + 14 > j)
            {
                return i + j;
            }
            if (j*12 < i)
            {
                return 42 + i - j*7;
            }
            return i%14 - j;
        }

        //[MethodImpl(MethodImplOptions.NoInlining)]
        private static int NoInliningLargeMethod(int i, int j)
        {
            if (i + 14 > j)
            {
                return i + j;
            }
            if (j*12 < i)
            {
                return 42 + i - j*7;
            }
            return i%14 - j;
        }
    }
}
