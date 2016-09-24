using System;

namespace MakingDotNETApplicationsFaster.Runners.Models
{
    public sealed class Summary
    {
        public string UserId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Period { get; set; }

        public string Duration { get; set; }

        public int StepsTaken { get; set; }

        public CaloriesBurnedSummary CaloriesBurnedSummary { get; set; }

        public HeartRateSummary HeartRateSummary { get; set; }

        public DistanceSummary DistanceSummary { get; set; }

        public int ActiveHours { get; set; }
    }
}