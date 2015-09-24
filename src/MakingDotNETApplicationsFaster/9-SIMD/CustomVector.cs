using System.Runtime.CompilerServices;

namespace MakingDotNETApplicationsFaster
{
    struct CustomVector
    {
        public double X;
        public double Y;
        public double Z;

        public CustomVector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CustomVector operator *(CustomVector left, CustomVector right)
        {
            return new CustomVector(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }
    }
}