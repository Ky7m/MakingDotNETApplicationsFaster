using System.Runtime.CompilerServices;

namespace MakingDotNETApplicationsFaster.Runners.SIMD
{
    internal struct CustomVector
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public CustomVector(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CustomVector operator *(CustomVector left, CustomVector right)
        {
            return new CustomVector(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
        }
    }
}