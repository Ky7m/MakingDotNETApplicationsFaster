using System;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster.Runners.JaggedArraysVersus2DArrays
{
    sealed class JaggedArraysVersus2DArraysRunner : IRunner
    {
        const bool ForceFullCollection = true;
        const int Increment = 1309;

        public void Run()
        {
            const int rows = 1000;
            const int cols = 100;

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            int[][] jaggedArray = JaggedArrayMemory(rows, cols);

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            int[,] array = TwoDimensionalArrayMemory(rows, cols);

            new PerformanceTests
            {
                {_ => { GetSumFor2DArray(array,rows,cols); }, "GetSumFor2DArray"},
                {_ => { GetSumForJaggedArray(jaggedArray,rows,cols); }, "GetSumForJaggedArray"},

                {_ => { GetSumForJaggedArrayWithCachingTo1DArray(jaggedArray,rows,cols); }, "GetSumForJaggedArrayWithCachingTo1DArray"},

                {_ => { Traversal2DArray(array,rows,cols); }, "Traversal2DArray"},
                {_ => { TraversalJaggedArray(jaggedArray,rows,cols); }, "TraversalJaggedArray"},

                {_ => { OptimizedTraversal2DArray(array,rows,cols); }, "OptimizedTraversal2DArray"},
                {_ => { OptimizedTraversalJagged(jaggedArray,rows,cols); }, "OptimizedTraversalJagged"},

                {_ => { SemiRandomAccess2DArray(array,rows,cols); }, "SemiRandomAccess2DArray"},
                {_ => { SemiRandomAccessJaggedArray(jaggedArray,rows,cols); }, "SemiRandomAccessJaggedArray"}
            }.Run(10000);
        }

        #region Memory Test

        static int[][] JaggedArrayMemory(int rows, int cols)
        {

            long b1 = GC.GetTotalMemory(ForceFullCollection);
            int[][] jagged = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                jagged[i] = new int[cols];
            }
            long b2 = GC.GetTotalMemory(ForceFullCollection);
            jagged[0][0] = 0;
            Console.WriteLine("{0} bytes (jagged {1} x {2})", b2 - b1, rows, cols);
            return jagged;
        }

        static int[,] TwoDimensionalArrayMemory(int rows, int cols)
        {
            long b1 = GC.GetTotalMemory(ForceFullCollection);
            int[,] array = new int[rows, cols];
            long b2 = GC.GetTotalMemory(ForceFullCollection);
            array[0, 0] = 0;
            Console.WriteLine("{0} bytes (2D {1} x {2})", b2 - b1, rows, cols);
            return array;
        }

        #endregion Memory Test

        #region Performance Test

        static long GetSumFor2DArray(int[,] array, int rows, int cols)
        {
            long sum = 0;
            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < cols; ++j)
                {
                    sum += array[i, j];
                }
            }
            return sum;

        }

        static long GetSumForJaggedArray(int[][] jaggedArray, int rows, int cols)
        {
            long sum = 0;
            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < cols; ++j)
                {
                    sum += jaggedArray[i][j];
                }
            }
            return sum;

        }

        static long GetSumForJaggedArrayWithCachingTo1DArray(int[][] jaggedArray, int rows, int cols)
        {
            long sum = 0;
            for (var i = 0; i < rows; ++i)
            {
                var theRow = jaggedArray[i];
                for (var j = 0; j < cols; ++j)
                {
                    sum += theRow[j];
                }
            }
            return sum;

        }

        static void Traversal2DArray(int[,] array, int rows, int cols)
        {
            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < cols; ++j)
                {
                    array[i, j] = int.MaxValue - array[i, j];
                }
            }
        }

        static void TraversalJaggedArray(int[][] jaggedArray, int rows, int cols)
        {
            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < cols; ++j)
                {
                    jaggedArray[i][j] = int.MaxValue - jaggedArray[i][j];
                }
            }
        }

        static void OptimizedTraversal2DArray(int[,] array, int rows, int cols)
        {
            long count = (((long)rows) * cols) / 3;
            unsafe
            {
                fixed (int* pArray = array)
                {
                    int* p = pArray;
                    while (count-- > 0)
                    {
                        *p++ = int.MaxValue - *p;
                        *p++ = int.MaxValue - *p;
                        *p++ = int.MaxValue - *p;
                    }
                }
            }
        }

        static void OptimizedTraversalJagged(int[][] array, int rows, int cols)
        {
            for (var i = 0; i < rows; ++i)
            {
                unsafe
                {
                    int count = cols / 3;
                    fixed (int* pArray = array[i])
                    {
                        int* p = pArray;
                        while (count-- > 0)
                        {
                            *p++ = int.MaxValue - *p;
                            *p++ = int.MaxValue - *p;
                            *p++ = int.MaxValue - *p;
                        }
                    }
                }
            }
        }

        static void SemiRandomAccess2DArray(int[,] array, int rows, int cols)
        {
            int count = rows * cols;
            int row = 0;
            int col = 0;
            while (count-- > 0)
            {
                row = (row + Increment) % rows;
                col = (col + Increment) % cols;
                array[row, col] = int.MaxValue - array[row, col];
            }
        }

        static void SemiRandomAccessJaggedArray(int[][] array, int rows, int cols)
        {
            int count = rows * cols;
            int row = 0;
            int col = 0;
            while (count-- > 0)
            {
                row = (row + Increment) % rows;
                col = (col + Increment) % cols;
                array[row][col] = int.MaxValue - array[row][col];
            }
        }

        #endregion Performance Test
    }
}
