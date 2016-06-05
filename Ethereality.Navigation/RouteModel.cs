
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ethereality.FileService;
using Ethereality.CustomTypes;
namespace Ethereality.Navigation
{
   
    public class RouteModel 
    {
      

     
        public Waypoint CoordinatePoint { get; set; }
      
      

       
        public List<Waypoint> CoordinatePointsList { get; set; }
    

     
        public RouteSegmentVector PostionSegment { get; set; }
    

   

      
        public List<RouteSegmentVector> RouteSegmentVectors { get; set; }
   

        public RouteModel()
        {
            CoordinatePoint = new Waypoint();
            CoordinatePointsList = new List<Waypoint>();
            PostionSegment= new RouteSegmentVector();
            RouteSegmentVectors= new List<RouteSegmentVector>();
         
        }

        #region PointDifference Method

        /// <summary>
        /// Calculates the difference between 2 values
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>Difference between p1 and p2 </returns>
        private double PointDifference(double p1, double p2)
        {
            double difference;
            difference = p2 - p1;
            return difference;
        }

        #endregion PointDifference Method

        #region SegmentGCD Method

        /// <summary>
        /// Returns the Great Circle Distance between 2 points p1 and p2.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>GCD</returns>
        private double SegmentGCD(Waypoint p1, Waypoint p2)
        {
            double a;
            double c;
            double dLat;
            double dLon;
            double lat1inRad;
            double lat2inRad;
            double GCD;

            dLat = PointDifference(p1.Latitude, p2.Latitude) * Constants.toRad;
            dLon = PointDifference(p1.Longitude, p2.Longitude) * Constants.toRad;
            lat1inRad = p1.Latitude * Constants.toRad;
            lat2inRad = p2.Latitude * Constants.toRad;

            a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1inRad) * Math.Cos(lat2inRad);
            c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            GCD = Constants.EarthRadius * c;
            return GCD;
        }

        #endregion SegmentGCD Method

        #region Segment Slope Method

        /// <summary>
        /// Returns slope of a segment of road
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>slope</returns>
        private double SegmentSlope(Waypoint p1, Waypoint p2)
        {
            double slope;
            double GCD;
            double dz;
            GCD = SegmentGCD(p1, p2);
            dz = p2.Elevation - p1.Elevation;

            if (GCD > 0)
            {
                slope = Math.Atan((dz /
                    GCD)) / Constants.toRad;
            }
            else
            { slope = 0; }
            return slope;
        }

        #endregion Segment Slope Method

        #region Segment Azimuth Method

        /// <summary>
        /// Calculates the Azimuth of the current route segment
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>azimuth</returns>
        private double SegmentAzimuth(Waypoint p1, Waypoint p2)
        {
            double dLat;
            double dLon;
            double lat1inRad;
            double lat2inRad;
            double azimuth;

            dLat = PointDifference(p1.Latitude, p2.Latitude) * Constants.toRad;
            dLon = PointDifference(p1.Longitude, p2.Longitude) * Constants.toRad;
            lat1inRad = p1.Latitude * Constants.toRad;
            lat2inRad = p2.Latitude * Constants.toRad;

            double y = Math.Sin(dLon) * Math.Cos(lat2inRad);
            double x = Math.Cos(lat1inRad) * Math.Sin(lat2inRad) - Math.Sin(lat1inRad) * Math.Cos(lat2inRad) * Math.Cos(dLon);

            azimuth = Math.Atan2(y, x) / Constants.toRad;

            if (azimuth < 0)
            {
                azimuth = azimuth + 360;
            }
            return azimuth;
        }

        #endregion Segment Azimuth Method

        #region Segment Vector Method

        /// <summary>
        /// Calculates the segment characteristics namely GCD, Slope and Azimuth
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns>RouteSegment</returns>
        private RouteSegmentVector SegmentCalculations(Waypoint point1, Waypoint point2)
        {
            RouteSegmentVector routeSegment = new RouteSegmentVector();
            routeSegment.point1 = new Waypoint();
            routeSegment.point2 = new Waypoint();
            routeSegment.GCD = SegmentGCD(point1, point2);
            routeSegment.Elevation = point1.Elevation;
            routeSegment.Slope = SegmentSlope(point1, point2);
            routeSegment.Azimuth = SegmentAzimuth(point1, point2);
            routeSegment.point1 = point1;
            routeSegment.point2 = point2;

            return routeSegment;
        }

        #endregion Segment Vector Method

        public void EntireRouteSegmentCalculation()
        {
            GpxFileParser GpxFileParse = new GpxFileParser();
            CoordinatePointsList= GpxFileParse.GetGpxCoordinateData();

            int i = 0;
            int j = 1;
            double TotalDistance = 0;

            while (j < CoordinatePointsList.Count())
            {
                Waypoint p1 = new Waypoint();
                Waypoint p2 = new Waypoint();

                RouteSegmentVector routevector = new RouteSegmentVector();

                p1 = CoordinatePointsList[i];
                p2 = CoordinatePointsList[j];
                routevector = SegmentCalculations(p1, p2);
                TotalDistance += routevector.GCD;
                routevector.AccumulativeGCD = TotalDistance;
                routevector.Elevation = routevector.point1.Elevation;
                routevector.Index = i;

                RouteSegmentVectors.Add(routevector);
                ++i;
                ++j;
            }

        }
    }
}