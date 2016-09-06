
using System;
using System.Collections.Generic;
using System.Linq;
using Ethereality.FileService;
using Ethereality.CustomTypes;
using Etheriality.Constants;

namespace Ethereality.Navigation
{
    using System.Threading.Tasks;
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
        private double SegmentGcd(Waypoint p1, Waypoint p2)
        {
            double a;
            double c;
            double dLat;
            double dLon;
            double lat1InRad;
            double lat2InRad;
            double gcd;

            dLat = PointDifference(p1.Latitude, p2.Latitude) * ConstantValue.ToRad;
            dLon = PointDifference(p1.Longitude, p2.Longitude) * ConstantValue.ToRad;
            lat1InRad = p1.Latitude * ConstantValue.ToRad;
            lat2InRad = p2.Latitude * ConstantValue.ToRad;

            a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1InRad) * Math.Cos(lat2InRad);
            c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            gcd = ConstantValue.EarthRadius * c;
            return gcd;
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
            double gcd;
            double dz;
            gcd = SegmentGcd(p1, p2);
            dz = p2.Elevation - p1.Elevation;

            if (gcd > 0)
            {
                slope = Math.Atan((dz /
                    gcd)) / ConstantValue.ToRad;
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
            double lat1InRad;
            double lat2InRad;
            double azimuth;

            dLat = PointDifference(p1.Latitude, p2.Latitude) * ConstantValue.ToRad;
            dLon = PointDifference(p1.Longitude, p2.Longitude) * ConstantValue.ToRad;
            lat1InRad = p1.Latitude * ConstantValue.ToRad;
            lat2InRad = p2.Latitude * ConstantValue.ToRad;

            double y = Math.Sin(dLon) * Math.Cos(lat2InRad);
            double x = Math.Cos(lat1InRad) * Math.Sin(lat2InRad) - Math.Sin(lat1InRad) * Math.Cos(lat2InRad) * Math.Cos(dLon);

            azimuth = Math.Atan2(y, x) / ConstantValue.ToRad;

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
            routeSegment.Point1 = new Waypoint();
            routeSegment.Point2 = new Waypoint();
            routeSegment.Gcd = SegmentGcd(point1, point2);
            routeSegment.Elevation = point1.Elevation;
            routeSegment.Slope = SegmentSlope(point1, point2);
            routeSegment.Azimuth = SegmentAzimuth(point1, point2);
            routeSegment.Point1 = point1;
            routeSegment.Point2 = point2;

            return routeSegment;
        }

        #endregion Segment Vector Method

        public async Task EntireRouteSegmentCalculation()
        {
            GpxFileParser gpxFileParse = new GpxFileParser();
            CoordinatePointsList= await gpxFileParse.GetGpxCoordinateData();

            int i = 0;
            int j = 1;
            double totalDistance = 0;
            Task routeSegmentTask = new Task(() =>
             {
                 while (j < CoordinatePointsList.Count())
                 {
                     Waypoint p1 = new Waypoint();
                     Waypoint p2 = new Waypoint();

                     RouteSegmentVector routevector = new RouteSegmentVector();

                     p1 = CoordinatePointsList[i];
                     p2 = CoordinatePointsList[j];
                     routevector = SegmentCalculations(p1, p2);
                     totalDistance += routevector.Gcd;
                     routevector.AccumulativeGcd = totalDistance;
                     routevector.Elevation = routevector.Point1.Elevation;
                     routevector.Index = i;

                     RouteSegmentVectors.Add(routevector);
                     ++i;
                     ++j;
                 }
             });
            routeSegmentTask.GetAwaiter();

        }
    }
}