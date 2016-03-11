using BenchmarkDotNet.Attributes;

namespace MakingDotNETApplicationsFaster.Runners.ReadOnlyFields
{
    public class ReadOnlyFieldsRunner
    {
        private Int256 _value = new Int256(1L, 5L, 10L, 100L);
        private readonly Int256 _readOnlyValue = new Int256(1L, 5L, 10L, 100L);

        [Benchmark]
        private long GetValue()
        {
            return _value.Bits0 + _value.Bits1 + _value.Bits2 + _value.Bits3;
        }

        [Benchmark]
        private long GetReadOnlyValue()
        {
            return _readOnlyValue.Bits0 + _readOnlyValue.Bits1 + _readOnlyValue.Bits2 + _readOnlyValue.Bits3;
        }
    }
}