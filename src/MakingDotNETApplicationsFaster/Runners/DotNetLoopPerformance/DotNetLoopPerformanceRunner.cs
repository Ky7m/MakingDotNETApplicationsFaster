using System.Collections.Generic;
using System.Linq;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster.Runners.DotNetLoopPerformance
{
    sealed class DotNetLoopPerformanceRunner : IRunner
    {
        const int ArrayLength = 10000;
        public void Run()
        {
            var array = Enumerable.Range(0, ArrayLength).ToArray();
            //array[ArrayLength] = 1; // throws error on runtime: that means that the CLR has to inject bounds checking into array access
            var list = array.ToList();

            new PerformanceTests
            {
                {_ => { BaselineLoop(array); }, "BaselineLoop"},

                {_ => { GetSumWhile(array); }, "GetSumWhile"},

                {_ => { UnsafeArrayLinearAccessWithPointerIncrement(array); }, "UnsafeArrayLinearAccessWithPointerIncrement"},
                {_ => { UnsafeArrayLinearAccess(array); }, "UnsafeArrayLinearAccess"},

                {_ => { GetSumForeach(array); }, "GetSumForeach"},
                {_ => { GetSumLinq(array); }, "GetSumLinq"},

                {_ => { GetSumOfListFor(list); }, "GetSumOfListFor"},
                {_ => { GetSumOfListForeach(list); }, "GetSumOfListForeach"},
                {_ => { GetSumOfListLinq(list); }, "GetSumOfListLinq"},
                {_ => { GetSumOfIEnumerableForeach(list); }, "GetSumOfIEnumerableForeach"},

                {_ => { GetSumLoopUnrollingArray(array); }, "GetSumLoopUnrollingArray"},
                {_ => { GetSumLoopUnrollingList(list); }, "GetSumLoopUnrollingList"},

                {_ => { GetSumWithPrecalculatedLength(array); }, "GetSumWithPrecalculatedLength"}
            }.Run(100000);
        }

        static long BaselineLoop(int[] array)
        {
            long sum = 0;
            for (var i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;

        }

        static long GetSumWhile(int[] array)
        {
            long sum = 0;
            var i = array.Length;
            while (i-- > 0)
            {
                sum += array[i];
            }
            return sum;

        }

        unsafe static long UnsafeArrayLinearAccessWithPointerIncrement(int[] array)
        {
            long sum = 0;
            fixed (int* pointer = &array[0])
            {
                var current = pointer;

                for (var i = 0; i < array.Length; ++i)
                {
                    sum += *(current++);
                }
            }

            return sum;
        }

        unsafe static long UnsafeArrayLinearAccess(int[] array)
        {
            long sum = 0;
            fixed (int* pointer = &array[0])
            {
                var current = pointer;

                for (var i = 0; i < array.Length; ++i)
                {
                    sum += *(current + i);
                }
            }

            return sum;
        }

        static long GetSumForeach(int[] array)
        {
            long sum = 0;
            foreach (var val in array)
            {
                sum += val;
            }
            return sum;
        }

        static long GetSumLinq(int[] array)
        {
            return array.Sum();
        }

        static long GetSumOfListFor(List<int> list)
        {
            long sum = 0;
            for (var i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return sum;
        }

        static long GetSumOfListForeach(List<int> list)
        {
            long sum = 0;
            foreach (var val in list)
            {
                sum += val;
            }

            return sum;
        }

        static long GetSumOfListLinq(List<int> list)
        {
            return list.Sum();
        }

        static long GetSumOfIEnumerableForeach(IEnumerable<int> collection)
        {
            long sum = 0;
            foreach (var val in collection)
            {
                sum += val;
            }

            return sum;
        }

        static long GetSumLoopUnrollingArray(int[] array)
        {
            long sum = 0;
            for (var i = 0; i < array.Length - 4; i += 4)
            {
                sum += array[i];
                sum += array[i + 1];
                sum += array[i + 2];
                sum += array[i + 3];
            }

            return sum;
        }

        static long GetSumLoopUnrollingList(List<int> list)
        {
            long sum = 0;
            for (var i = 0; i < list.Count - 4; i += 4)
            {
                sum += list[i];
                sum += list[i + 1];
                sum += list[i + 2];
                sum += list[i + 3];
            }

            return sum;
        }

        static long GetSumWithPrecalculatedLength(int[] array)
        {
            long sum = 0;
            for (var i = 0; i < ArrayLength; i++)
            {
                sum += array[i];
            }

            return sum;
        }
    }
}
