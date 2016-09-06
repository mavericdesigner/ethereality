namespace Ethereality.CustomTypes
{
    internal enum Rating { Poor, Average, Okay, Good, Excellent }

    public enum PointControl
    {
        Start,
        ControlStop,
        End,
        LoopStart,
        LoopEnd
    }

    /// <summary>
    /// Route Coordinate Information
    /// </summary>
    ///

    public struct Waypoint
    {
        public int Index { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Elevation { get; set; }

        public PointControl PointType { get; set; }
    }

    /// <summary>
    /// Route Information between two points.
    /// </summary>
    public struct RouteSegmentVector
    {
        public int Index { get; set; }

        public Waypoint Point1 { get; set; }

        public Waypoint Point2 { get; set; }

        public double Azimuth { get; set; }

        public double Slope { get; set; }

        public double Gcd { get; set; }

        public double Elevation { get; set; }

        public double AccumulativeGcd { get; set; }
    }

    /// <summary>
    /// Summary of Route Quality
    /// </summary>
    //struct RouteSurface
    //{
    //    bool Potholes;
    //    Rating RouteQuality;

    //}
}