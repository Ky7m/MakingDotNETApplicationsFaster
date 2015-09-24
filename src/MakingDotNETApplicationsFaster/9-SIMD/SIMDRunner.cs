using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster
{
    sealed class SIMDRunner : IRunner
    {
        public void Run()
        {
            var customVector1 = new CustomVector(1,2,3);
            var customVector2 = new CustomVector(3,2,1);
            new PerformanceTests
            {
                {_ => CustomVectorMultiply(customVector1,customVector2), "CustomVectorMultiply"}
            }.Run(10000000);
        }

        static void CustomVectorMultiply(CustomVector customVector1, CustomVector customVector2)
        {
           var result = customVector1 * customVector2;
        }
    }
}