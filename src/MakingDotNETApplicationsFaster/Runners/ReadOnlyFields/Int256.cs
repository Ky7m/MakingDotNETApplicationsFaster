namespace MakingDotNETApplicationsFaster.Runners.ReadOnlyFields
{
    public struct Int256
    {
        public Int256(long bits0, long bits1, long bits2, long bits3)
        {
            Bits0 = bits0;
            Bits1 = bits1;
            Bits2 = bits2;
            Bits3 = bits3;
        }
        public long Bits0 { get; set; }
        public long Bits1 { get; set; }
        public long Bits2 { get; set; }
        public long Bits3 { get; set; }
    }
}