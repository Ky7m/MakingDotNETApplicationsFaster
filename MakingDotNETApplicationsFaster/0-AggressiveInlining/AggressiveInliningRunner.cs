using System;
using System.Runtime.CompilerServices;
using System.Security;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster
{
    sealed class AggressiveInliningRunner : IRunner
    {
        public void Run()
        {
            new PerformanceTests
            {
                {i => { SmallMethod(i, i + 3); }, "SmallMethod"},
                {i => { NoInliningLargeMethod(i, i + 3); }, "NoInliningLargeMethod"},
                {i => { InliningLargeMethod(i, i + 3); }, "InliningLargeMethod"},
                {i => { LargeMethodWithAdditionalAttributes(i, i + 3); }, "LargeMethodWithAdditionalAttributes"}
            }.Run(10000000);
        }

        static int SmallMethod(int i, int j)
        {
            if (i > j) return i + j;
            return i + 2 * j - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int InliningLargeMethod(int i, int j)
        {
            if (i + 14 > j)
            {
                return i + j;
            }
            if (j * 12 < i)
            {
                return 42 + i - j * 7;
            }
            return i % 14 - j;
        }

        //[MethodImpl(MethodImplOptions.NoInlining)]
        static int NoInliningLargeMethod(int i, int j)
        {
            if (i + 14 > j)
            {
                return i + j;
            }
            if (j * 12 < i)
            {
                return 42 + i - j * 7;
            }
            return i % 14 - j;
        }

        // The attributes on this method are chosen for the best JIT performance. 
        // Based on a following finding https://github.com/dotnet/coreclr/blob/cbf46fb0b6a0b209ed1caf4a680910b383e68cba/src/mscorlib/src/System/Buffer.cs#L536
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int LargeMethodWithAdditionalAttributes(int i, int j)
        {
            if (i + 14 > j)
            {
                return i + j;
            }
            if (j * 12 < i)
            {
                return 42 + i - j * 7;
            }
            return i % 14 - j;
        }
    }
}
