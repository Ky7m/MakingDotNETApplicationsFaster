using System.Numerics;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster
{
    sealed class SIMDRunner : IRunner
    {
        public void Run()
        {
            var customVector1 = new CustomVector(1, 2, 3, 0);
            var customVector2 = new CustomVector(3, 2, 1, 0);

            var bclVector1 = new Vector4(1, 2, 3, 0);
            var bclVector2 = new Vector4(3, 2, 1, 0);
            new PerformanceTests
            {
                {_ => CustomVectorMultiply(customVector1, customVector2), "CustomVectorMultiply"},
                {_ => BclVectorMultiply(bclVector1, bclVector2), "BclVectorMultiply"}
            }.Run(100000000);
        }

        static CustomVector CustomVectorMultiply(CustomVector customVector1, CustomVector customVector2)
        {
            return customVector1 * customVector2;
        }
        static Vector4 BclVectorMultiply(Vector4 bclVector1, Vector4 bclVector2)
        {
            return bclVector1 * bclVector2;
        }
    }
}