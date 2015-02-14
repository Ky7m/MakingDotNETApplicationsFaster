using System.Collections.Generic;
using System.Linq;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster
{
    internal sealed class DotNetLoopPerformanceRunner : IRunner
    {
        private const int ArrayLength = 10000;
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

        private static long BaselineLoop(int[] array)
        {
            long sum = 0;
            for (var i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;

        }

        private static long GetSumWhile(int[] array)
        {
            long sum = 0;
            var i = array.Length;
            while (i --> 0)
            {
                sum += array[i];
            }
            return sum;

        }

        private unsafe static long UnsafeArrayLinearAccessWithPointerIncrement(int[] array)
        {
            long sum = 0;
            fixed (int* pointer = &array[0])
            {
                int* current = pointer;

                for (int i = 0; i < array.Length; ++i)
                {
                    sum += *(current++);
                }
            }

            return sum;
        }

        private unsafe static long UnsafeArrayLinearAccess(int[] array)
        {
            long sum = 0;
            fixed (int* pointer = &array[0])
            {
                int* current = pointer;

                for (int i = 0; i < array.Length; ++i)
                {
                    sum += *(current + i);
                }
            }

            return sum;
        }

        private static long GetSumForeach(int[] array)
        {
            long sum = 0;
            foreach (var val in array)
            {
                sum += val;
            }
            return sum;
        }

        private static long GetSumLinq(int[] array)
        {
            return array.Sum();
        }

        private static long GetSumOfListFor(List<int> list)
        {
            long sum = 0;
            for (var i = 0; i < list.Count; i++)
            {
                sum += list[i];
            }

            return sum;
        }

        private static long GetSumOfListForeach(List<int> list)
        {
            long sum = 0;
            foreach (var val in list)
            {
                sum += val;
            }

            return sum;
        }

        private static long GetSumOfListLinq(List<int> list)
        {
            return list.Sum();
        }

        private static long GetSumOfIEnumerableForeach(IEnumerable<int> collection)
        {
            long sum = 0;
            foreach (var val in collection)
            {
                sum += val;
            }

            return sum;
        }

        private static long GetSumLoopUnrollingArray(int[] array)
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

        private static long GetSumLoopUnrollingList(List<int> list)
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

        private static long GetSumWithPrecalculatedLength(int[] array)
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
