namespace MakingDotNETApplicationsFaster.Runners.SerializersPerformance.Models
{
    public struct HeartRateSummary
    {
        public string Period { get; set; }

        public string AverageHeartRate { get; set; }

        public string PeakHeartRate { get; set; }

        public string LowestHeartRate { get; set; }
    }
}