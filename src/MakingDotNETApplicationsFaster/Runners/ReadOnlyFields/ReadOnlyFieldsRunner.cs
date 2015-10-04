using MakingDotNETApplicationsFaster.Infrastructure;

namespace MakingDotNETApplicationsFaster.Runners.ReadOnlyFields
{
    sealed class ReadOnlyFieldsRunner : IRunner
    {
        Int256 _value = new Int256(1L, 5L, 10L, 100L);
        readonly Int256 _readOnlyValue = new Int256(1L, 5L, 10L, 100L);

        public void Run()
        {
            new PerformanceTests
            {
                {_ => { GetValue(); }, "GetValue"},
                {_ => { GetReadOnlyValue(); }, "GetReadOnlyValue"}
            }.Run(1000000);
        }

        long GetValue()
        {
            return _value.Bits0 + _value.Bits1 + _value.Bits2 + _value.Bits3;
        }

        long GetReadOnlyValue()
        {
            return _readOnlyValue.Bits0 + _readOnlyValue.Bits1 + _readOnlyValue.Bits2 + _readOnlyValue.Bits3;
        }
    }
}