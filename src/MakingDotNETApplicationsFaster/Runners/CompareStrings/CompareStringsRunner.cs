using System;
using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster.Runners.CompareStrings
{
    sealed class CompareStringsRunner : IRunner
    {

        public void Run()
        {
            // use no string literals, which would be interned, placed in the executable.
            string a1 = $"{1}{1}";
            string a2 = $"{1}{2}";

            string b1 = $"{1}{1}{1}{1}{1}{1}";
            string b2 = $"{1}{1}{1}{2}{2}{2}";

            string c1 = $"{1}{1}{1}{1}{1}{1}{1}{1}{1}{1}{1}{1}";
            string c2 = $"{1}{1}{1}{1}{1}{1}{2}{2}{2}{2}{2}{2}";

            new PerformanceTests
            {
                {_ => { Common(a1, a2, b1, b2, c1, c2); }, "=="},
                {_ => { Equals(a1, a2, b1, b2, c1, c2); }, "Equals"},
                {_ => { StringEquals(a1, a2, b1, b2, c1, c2); }, "String.Equals"},
                {_ => { StringCompare(a1, a2, b1, b2, c1, c2); }, "String.Compare"},
                {_ => { StringCompareOrdinal(a1, a2, b1, b2, c1, c2); }, "String.CompareOrdinal"},
                {_ => { StringCompareIgnoreCase(a1, a2, b1, b2, c1, c2); }, "String.StringCompareIgnoreCase"},

                {_ => { EqualsCurrentCulture(a1, a2, b1, b2, c1, c2); }, "EqualsCurrentCulture"},
                {_ => { StringEqualsCurrentCulture(a1, a2, b1, b2, c1, c2); }, "String.EqualsCurrentCulture"},
                {_ => { EqualsCurrentCultureIgnoreCase(a1, a2, b1, b2, c1, c2); }, "EqualsCurrentCultureIgnoreCase"},
                {
                    _ => { StringEqualsCurrentCultureIgnoreCase(a1, a2, b1, b2, c1, c2); },
                    "String.EqualsCurrentCultureIgnoreCase"
                },

                // dotnet/corefx#108 team has no plans to bring StringComparison.InvariantCulture.

                {_ => { EqualsOrdinalCulture(a1, a2, b1, b2, c1, c2); }, "EqualsOrdinalCulture"},
                {_ => { StringEqualsOrdinalCulture(a1, a2, b1, b2, c1, c2); }, "String.EqualsOrdinalCulture"},
                {_ => { EqualsOrdinalCultureIgnoreCase(a1, a2, b1, b2, c1, c2); }, "EqualsOrdinalCultureIgnoreCase"},
                {
                    _ => { StringEqualsOrdinalCultureIgnoreCase(a1, a2, b1, b2, c1, c2); },
                    "String.EqualsOrdinalCultureIgnoreCase"
                },

                {_ => { CompareTo(a1, a2, b1, b2, c1, c2); }, "CompareTo"}
            }.Run(10000000);
        }

        static bool Common(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return a1 == a2 &&
                b1 == b2 &&
                c1 == c2 &&
                a1 == c1 &&
                b2 == c2;
        }
        static bool Equals(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return a1.Equals(a2) &&
                b1.Equals(b2) &&
                c1.Equals(c2) &&
                a1.Equals(c1) &&
                b2.Equals(c2);
        }
        static bool StringEquals(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return string.Equals(a1, a2) &&
                string.Equals(b1, b2) &&
                string.Equals(c1, c2) &&
                string.Equals(a1, c1) &&
                string.Equals(b2, c2);
        }
        static bool StringCompare(string a1, string a2, string b1, string b2, string c1, string c2)
        {
#pragma warning disable RECS0119 // Warns when a culture-aware 'Compare' call is used by default
            return string.Compare(a1, a2) == 0 &&
                string.Compare(b1, b2) == 0 &&
                string.Compare(c1, c2) == 0 &&
                string.Compare(a1, c1) == 0 &&
                string.Compare(b2, c2) == 0;
#pragma warning restore RECS0119 // Warns when a culture-aware 'Compare' call is used by default
        }
        static bool StringCompareOrdinal(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return string.CompareOrdinal(a1, a2) == 0 &&
                string.CompareOrdinal(b1, b2) == 0 &&
                string.CompareOrdinal(c1, c2) == 0 &&
                string.CompareOrdinal(a1, c1) == 0 &&
                string.CompareOrdinal(b2, c2) == 0;
        }
        static bool StringCompareIgnoreCase(string a1, string a2, string b1, string b2, string c1, string c2)
        {
#pragma warning disable RECS0119
            return string.Compare(a1, a2, true) == 0 &&

                string.Compare(b1, b2, true) == 0 &&
                string.Compare(c1, c2, true) == 0 &&
                string.Compare(a1, c1, true) == 0 &&
                string.Compare(b2, c2, true) == 0;
#pragma warning restore RECS0119
        }
        static bool EqualsCurrentCulture(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return a1.Equals(a2, StringComparison.CurrentCulture) &&
                b1.Equals(b2, StringComparison.CurrentCulture) &&
                c1.Equals(c2, StringComparison.CurrentCulture) &&
                a1.Equals(c1, StringComparison.CurrentCulture) &&
                b2.Equals(c2, StringComparison.CurrentCulture);
        }
        static bool StringEqualsCurrentCulture(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return string.Equals(a1, a2, StringComparison.CurrentCulture) &&
                string.Equals(b1, b2, StringComparison.CurrentCulture) &&
                string.Equals(c1, c2, StringComparison.CurrentCulture) &&
                string.Equals(a1, c1, StringComparison.CurrentCulture) &&
                string.Equals(b2, c2, StringComparison.CurrentCulture);
        }
        static bool EqualsCurrentCultureIgnoreCase(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return a1.Equals(a2, StringComparison.CurrentCultureIgnoreCase) &&
                b1.Equals(b2, StringComparison.CurrentCultureIgnoreCase) &&
                c1.Equals(c2, StringComparison.CurrentCultureIgnoreCase) &&
                a1.Equals(c1, StringComparison.CurrentCultureIgnoreCase) &&
                b2.Equals(c2, StringComparison.CurrentCultureIgnoreCase);
        }
        static bool StringEqualsCurrentCultureIgnoreCase(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return string.Equals(a1, a2, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(b1, b2, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(c1, c2, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(a1, c1, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(b2, c2, StringComparison.CurrentCultureIgnoreCase);
        }

        static bool EqualsOrdinalCulture(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return a1.Equals(a2, StringComparison.Ordinal) &&
                b1.Equals(b2, StringComparison.Ordinal) &&
                c1.Equals(c2, StringComparison.Ordinal) &&
                a1.Equals(c1, StringComparison.Ordinal) &&
                b2.Equals(c2, StringComparison.Ordinal);
        }
        static bool StringEqualsOrdinalCulture(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return string.Equals(a1, a2, StringComparison.Ordinal) &&
                string.Equals(b1, b2, StringComparison.Ordinal) &&
                string.Equals(c1, c2, StringComparison.Ordinal) &&
                string.Equals(a1, c1, StringComparison.Ordinal) &&
                string.Equals(b2, c2, StringComparison.Ordinal);
        }
        static bool EqualsOrdinalCultureIgnoreCase(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return a1.Equals(a2, StringComparison.OrdinalIgnoreCase) &&
                b1.Equals(b2, StringComparison.OrdinalIgnoreCase) &&
                c1.Equals(c2, StringComparison.OrdinalIgnoreCase) &&
                a1.Equals(c1, StringComparison.OrdinalIgnoreCase) &&
                b2.Equals(c2, StringComparison.OrdinalIgnoreCase);
        }
        static bool StringEqualsOrdinalCultureIgnoreCase(string a1, string a2, string b1, string b2, string c1, string c2)
        {
            return string.Equals(a1, a2, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(b1, b2, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(c1, c2, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(a1, c1, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(b2, c2, StringComparison.OrdinalIgnoreCase);
        }

        static bool CompareTo(string a1, string a2, string b1, string b2, string c1, string c2)
        {
#pragma warning disable RECS0064 // Warns when a culture-aware 'string.CompareTo' call is used by default
            return a1.CompareTo(a2) == 0 &&
                b1.CompareTo(b2) == 0 &&
                c1.CompareTo(c2) == 0 &&
                a1.CompareTo(c1) == 0 &&
                b2.CompareTo(c2) == 0;
#pragma warning restore RECS0064 // Warns when a culture-aware 'string.CompareTo' call is used by default
        }
    }
}