using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster
{
    sealed class StructEqualityRunner : IRunner
    {
        public void Run()
        {
            var a = new StructWithNoRefType();
            var b = new StructWithNoRefType();

            var c = new StructWithRefType();
            var d = new StructWithRefType();

            var x = new StructWithRefTypeAndOverridenEquals();
            var y = new StructWithRefTypeAndOverridenEquals();

            var m = new StructWithRefTypeAndEquatableImplementation();
            var n = new StructWithRefTypeAndEquatableImplementation();

            new PerformanceTests
            {
                {_ => { CompareStructsWithNoRefTypes(a, b); }, "CompareStructsWithNoRefTypes"},
                {_ => { CompareStructsWithRefTypes(c, d); }, "CompareStructsWithRefTypes"},
                {_ => { CompareStructsWithRefTypesAndOverridenEquals(x, y); }, "CompareStructsWithRefTypesAndOverridenEquals"},
                {_ => { CompareStructsWithRefTypesAndEquatableImplementation(m, n); }, "CompareStructsWithRefTypesAndEquatableImplementation"}
            }.Run(10000000);
        }

        static bool CompareStructsWithNoRefTypes(StructWithNoRefType a, StructWithNoRefType b)
        {
            return a.Equals(b);
        }

        static bool CompareStructsWithRefTypes(StructWithRefType c, StructWithRefType d)
        {
            return c.Equals(d);
        }

        static bool CompareStructsWithRefTypesAndOverridenEquals(StructWithRefTypeAndOverridenEquals x, StructWithRefTypeAndOverridenEquals y)
        {
            return x.Equals(y);
        }
        static bool CompareStructsWithRefTypesAndEquatableImplementation(StructWithRefTypeAndEquatableImplementation x, StructWithRefTypeAndEquatableImplementation y)
        {
            return x.Equals(y);
        }
    }
}