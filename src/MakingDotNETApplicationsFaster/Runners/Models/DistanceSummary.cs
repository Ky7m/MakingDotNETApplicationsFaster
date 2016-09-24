namespace MakingDotNETApplicationsFaster.Runners.Models
{
    public struct DistanceSummary
    {
        public string Period { get; set; }

        public double TotalDistance { get; set; }

        public double TotalDistanceOnFoot { get; set; }

        public double ActualDistance { get; set; }

        public double ElevationGain { get; set; }

        public double ElevationLoss { get; set; }

        public double MaxElevation { get; set; }

        public double MinElevation { get; set; }

        public double WaypointDistance { get; set; }

        public double Speed { get; set; }

        public double Pace { get; set; }

        public double OverallPace { get; set; }
    }
}