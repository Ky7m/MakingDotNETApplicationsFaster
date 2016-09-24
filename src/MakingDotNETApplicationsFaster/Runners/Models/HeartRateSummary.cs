namespace MakingDotNETApplicationsFaster.Runners.Models
{
    public struct HeartRateSummary
    {
        public string Period { get; set; }

        public string AverageHeartRate { get; set; }

        public string PeakHeartRate { get; set; }

        public string LowestHeartRate { get; set; }
    }
}