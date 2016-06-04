
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace Ethereality.FileService
{
    public class GpxFileParser
    {
        #region GpxFileName
        public string GpxFilename { get; set; }
        public string[] GpxFileNames { get; set; }
        public ICollection<Waypoint> RouteCoordinatePoints { get; set; }
     

        private void SelectFile()
        {
            FileHandling filehandling = new FileHandling();
            GpxFilename = filehandling.OpenFile();
        }

        /// <summary>
        /// Load the Xml document for parsing
        /// </summary>
        /// <param name="sFile">Fully qualified file name (local)</param>
        /// <returns>XDocument</returns>
        private XDocument GetGpxDoc(string sFile)
        {
            if (sFile != String.Empty)
            {
                XDocument gpxDoc = XDocument.Load(sFile);
                return gpxDoc;
            }
            else return null;
        }

        /// <summary>
        /// Load the namespace for a standard GPX document
        /// </summary>
        /// <returns></returns>
        private XNamespace GetGpxNameSpace()
        {
            XNamespace gpx = XNamespace.Get("http://www.topografix.com/GPX/1/1");
            return gpx;
        }

        /// <summary>
        /// When passed a file, open it and parse all waypoints from it.
        /// </summary>
        /// <param name="sFile">Fully qualified file name (local)</param>
        /// <returns>string containing line delimited waypoints from
        /// the file (for test)</returns>
        /// <remarks>Normally, this would be used to populate the
        /// appropriate object model</remarks>
        private string LoadGPXWaypoints(string sFile)
        {
            XDocument gpxDoc = GetGpxDoc(sFile);
            XNamespace gpx = GetGpxNameSpace();

            var waypoints = from waypoint in gpxDoc.Descendants(gpx + "wpt")
                            select new
                            {
                                Latitude = waypoint.Attribute("lat").Value,
                                Longitude = waypoint.Attribute("lon").Value,
                                Elevation = waypoint.Element(gpx + "ele") != null ?
                                    waypoint.Element(gpx + "ele").Value : null,
                                Name = waypoint.Element(gpx + "name") != null ?
                                    waypoint.Element(gpx + "name").Value : null,
                                Dt = waypoint.Element(gpx + "cmt") != null ?
                                    waypoint.Element(gpx + "cmt").Value : null
                            };

            StringBuilder sb = new StringBuilder();
            foreach (var wpt in waypoints)
            {
                // This is where we'd instantiate data
                // containers for the information retrieved.
                sb.Append(
                  string.Format("Name:{0} Latitude:{1} Longitude:{2} Elevation:{3} Date:{4}\n",
                  wpt.Name, wpt.Latitude, wpt.Longitude,
                  wpt.Elevation, wpt.Dt));
            }

            return sb.ToString();
        }

        /// <summary>
        /// When passed a file, open it and parse all tracks
        /// and _track segments from it.
        /// </summary>
        /// <param name="sFile">Fully qualified file name (local)</param>
        /// <returns>string containing line delimited waypoints from the
        /// file (for test)</returns>
        private void LoadGPXTracks(string sFile)
        {
            XDocument gpxDoc = GetGpxDoc(sFile);
            XNamespace gpx = GetGpxNameSpace();
            try
            {
                var tracks = from track in gpxDoc.Descendants(gpx + "trk")
                             select new
                             {
                                 Name = track.Element(gpx + "name") != null ?
                                  track.Element(gpx + "name").Value : null,
                                 Segs = (
                                      from trackpoint in track.Descendants(gpx + "trkpt")
                                      select new
                                      {
                                          Latitude = trackpoint.Attribute("lat").Value,
                                          Longitude = trackpoint.Attribute("lon").Value,
                                          Elevation = trackpoint.Element(gpx + "ele") != null ?
                                            trackpoint.Element(gpx + "ele").Value : null,
                                          Time = trackpoint.Element(gpx + "time") != null ?
                                          trackpoint.Element(gpx + "time").Value : null
                                      })
                             };

                foreach (var trk in tracks)
                {
                    // Populate _track data objects.
                    Waypoint RouteCoordinatePoint = new Waypoint();
                    foreach (var trkSeg in trk.Segs)
                    {
                        RouteCoordinatePoint.Latitude = Convert.ToDouble((string)trkSeg.Latitude);
                        RouteCoordinatePoint.Longitude = Convert.ToDouble((string)trkSeg.Longitude);
                        RouteCoordinatePoint.Elevation = Convert.ToDouble((string)trkSeg.Elevation);
                        ++RouteCoordinatePoint.index;
                        RouteCoordinatePoints.Add(RouteCoordinatePoint);

                        // Populate detailed _track segments
                        // in the object model here.
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Gets Route Coordinate Data From Gpx file
        /// </summary>
        /// <returns></returns>
        public ICollection<Waypoint> GetGpxCoordinateData()
        {
            ICollection<Waypoint> RouteCoordinatePoints;
            SelectFile();
            LoadGPXTracks(GpxFilename);
            RouteCoordinatePoints = RouteCoordinatePoints;
            string json = JsonConvert.SerializeObject(RouteCoordinatePoints, Formatting.Indented);
            return RouteCoordinatePoints;
        }
    }
}