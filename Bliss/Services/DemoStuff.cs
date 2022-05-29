using GMap.NET;
using GMap.NET.MapProviders;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace Bliss
{
    public struct VehicleData
    {
        public int Id;
        public double Lat;
        public double Lng;
        public string Line;
        public string LastStop;
        public string TrackType;
        public string AreaName;
        public string StreetName;
        public string Time;
        public double? Bearing;
    }

    public enum TransportType
    {
        Bus,
        TrolleyBus,
    }

    public struct FlightRadarData
    {
        public string Name;
        public PointLatLng Point;
        public int Bearing;
        public string Altitude;
        public string Speed;
        public int Id;
    }

    public class Stuff
    {
        ////public const string GoogleMapsApiKey = "AIzaSyCoz0fVRmn6L-zZuLXnIXtRcGLKf2PHI5Q"; // this key is not working
        //public const string GoogleMapsApiKey = "AIzaSyAmO6pIPTz0Lt8lmYZEIAaixitKjq-4WlB"; // from Demo.Geocoding project

        public static bool PingNetwork(string hostNameOrAddress)
        {
            bool pingStatus;

            using (var p = new Ping())
            {
                var buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                int timeout = 4444; // 4s

                try
                {
                    var reply = p.Send(hostNameOrAddress, timeout, buffer);
                    pingStatus = reply.Status == IPStatus.Success;
                }
                catch (Exception)
                {
                    pingStatus = false;
                }
            }

            return pingStatus;
        }

        /// <summary>
        ///     gets routes from gpsd log file
        /// </summary>
        /// <param name="gpsdLogFile"></param>
        /// <param name="start">start time(UTC) of route, null to read from very start</param>
        /// <param name="end">end time(UTC) of route, null to read to the very end</param>
        /// <param name="maxPositionDilutionOfPrecision">max value of PositionDilutionOfPrecision, null to get all</param>
        /// <returns></returns>
        public static IEnumerable<List<GpsLog>> GetRoutesFromMobileLog(string gpsdLogFile, DateTime? start,
            DateTime? end, double? maxPositionDilutionOfPrecision)
        {
            using (var cn = new SQLiteConnection())
            {
                cn.ConnectionString = string.Format("Data Source=\"{0}\";FailIfMissing=True;", gpsdLogFile);


                cn.Open();
                {
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM GPS ";

                        if (start.HasValue)
                        {
                            cmd.CommandText += "WHERE TimeUTC >= @t1 ";
                            var lookupValue = new SQLiteParameter("@t1", start);
                            cmd.Parameters.Add(lookupValue);
                        }

                        if (end.HasValue)
                        {
                            if (!start.HasValue)
                            {
                                cmd.CommandText += "WHERE ";
                            }
                            else
                            {
                                cmd.CommandText += "AND ";
                            }

                            cmd.CommandText += "TimeUTC <= @t2 ";
                            var lookupValue = new SQLiteParameter("@t2", end);
                            cmd.Parameters.Add(lookupValue);
                        }

                        if (maxPositionDilutionOfPrecision.HasValue)
                        {
                            if (!start.HasValue && !end.HasValue)
                            {
                                cmd.CommandText += "WHERE ";
                            }
                            else
                            {
                                cmd.CommandText += "AND ";
                            }

                            cmd.CommandText += "(PositionDilutionOfPrecision <= @p3)";
                            var lookupValue = new SQLiteParameter("@p3", maxPositionDilutionOfPrecision);
                            cmd.Parameters.Add(lookupValue);
                        }

                        using (var rd = cmd.ExecuteReader())
                        {
                            var points = new List<GpsLog>();

                            long lastSessionCounter = -1;

                            while (rd.Read())
                            {
                                var log = new GpsLog();
                                {
                                    log.TimeUTC = (DateTime)rd["TimeUTC"];
                                    log.SessionCounter = (long)rd["SessionCounter"];
                                    log.Delta = rd["Delta"] as double?;
                                    log.Speed = rd["Speed"] as double?;
                                    log.SeaLevelAltitude = rd["SeaLevelAltitude"] as double?;
                                    log.EllipsoidAltitude = rd["EllipsoidAltitude"] as double?;
                                    log.SatellitesInView = rd["SatellitesInView"] as Byte?;
                                    log.SatelliteCount = rd["SatelliteCount"] as Byte?;
                                    log.Position = new PointLatLng((double)rd["Lat"], (double)rd["Lng"]);
                                    log.PositionDilutionOfPrecision = rd["PositionDilutionOfPrecision"] as double?;
                                    log.HorizontalDilutionOfPrecision = rd["HorizontalDilutionOfPrecision"] as double?;
                                    log.VerticalDilutionOfPrecision = rd["VerticalDilutionOfPrecision"] as double?;
                                    log.FixQuality = (FixQuality)((byte)rd["FixQuality"]);
                                    log.FixType = (FixType)((byte)rd["FixType"]);
                                    log.FixSelection = (FixSelection)((byte)rd["FixSelection"]);
                                }

                                if (log.SessionCounter - lastSessionCounter != 1 && points.Count > 0)
                                {
                                    var ret = new List<GpsLog>(points);
                                    points.Clear();
                                    {
                                        yield return ret;
                                    }
                                }

                                points.Add(log);
                                lastSessionCounter = log.SessionCounter;
                            }

                            if (points.Count > 0)
                            {
                                var ret = new List<GpsLog>(points);
                                points.Clear();
                                {
                                    yield return ret;
                                }
                            }

                            points.Clear();

                            rd.Close();
                        }
                    }
                }
                cn.Close();
            }

        }

        static readonly Random R = new Random();


        static string _sessionId = string.Empty;

        public static void GetFlightRadarData(List<FlightRadarData> ret, RectLatLng bounds)
        {
            ret.Clear();

            //if(resetSession || string.IsNullOrEmpty(sessionId))
            //{
            //   sessionId = GetFlightRadarContentUsingHttp("http://www.flightradar24.com/", location, zoom, string.Empty);
            //}

            // get track for one object
            //var tm = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            //var r = GetContentUsingHttp("http://www.flightradar24.com/FlightDataService.php?callsign=WZZ1MF&hex=47340F&date=" + tm, p1, 6, id);
            //Debug.WriteLine(r);

            //if(!string.IsNullOrEmpty(sessionId))
            {
                //var response = GetFlightRadarContentUsingHttp("http://arn.data.fr24.com/zones/fcgi/feed.js?bounds=63.056845879294244,55.95299968262111,5.99853515625,28.54248046875&faa=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=900&gliders=1&stats=1&", location, zoom, sessionId);
                string response = GetFlightRadarContentUsingHttp(string.Format(CultureInfo.InvariantCulture,
                    "http://arn.data.fr24.com/zones/fcgi/feed.js?bounds={0},{1},{2},{3}&faa=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=900&gliders=1&stats=1&",
                    bounds.Top,
                    bounds.Bottom,
                    bounds.Left,
                    bounds.Right));

                var items = response.Split(new[] { "\n," }, StringSplitOptions.RemoveEmptyEntries);

                //int i = 0;
                foreach (string it in items)
                {
                    if (it.Length > 11 && !it.Contains("full_count") && !it.Contains("stats"))
                    {
                        string d = it.TrimEnd(']').Replace(":[", ",").Replace("\"", string.Empty);

                        //Debug.WriteLine(++i + " -> " + d);

                        // BAW576":["400803",48.9923,1.8083,"144","36950","462","0512","LFPO","A319","G-EUPC"
                        var par = d.Split(',');
                        if (par.Length >= 9)
                        {
                            int id = Convert.ToInt32(par[0], 16);
                            string name = par[8] + "|" + par[9] + "|" + par[10];
                            string lat = par[2];
                            string lng = par[3];
                            string bearing = par[4];
                            string altitude = (int)(int.Parse(par[5]) * 0.3048) + "m";
                            string speed = (int)(int.Parse(par[6]) * 1.852) + "km/h";

                            var fd = new FlightRadarData();
                            fd.Name = name;
                            fd.Bearing = int.Parse(bearing);
                            fd.Altitude = altitude;
                            fd.Speed = speed;
                            fd.Point = new PointLatLng(double.Parse(lat, CultureInfo.InvariantCulture),
                                double.Parse(lng, CultureInfo.InvariantCulture));
                            fd.Id = id;

                            ret.Add(fd);

                            //Debug.WriteLine("name: " + name);
                            //Debug.WriteLine("hex: " + hex);
                            //Debug.WriteLine("point: " + fd.point);
                            //Debug.WriteLine("bearing: " + bearing);
                            //Debug.WriteLine("altitude: " + altitude);
                            //Debug.WriteLine("speed: " + speed);
                        }
                        else
                        {
#if DEBUG
                            if (Debugger.IsAttached)
                            {
                                Debugger.Break();
                            }
#endif
                        }

                        //Debug.WriteLine("--------------");
                    }
                }
            }
        }

        static string GetFlightRadarContentUsingHttp(string url)
        {
            string ret;

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UserAgent = GMapProvider.UserAgent;
            request.Timeout = GMapProvider.TimeoutMs;
            request.ReadWriteTimeout = GMapProvider.TimeoutMs * 6;
            request.Accept = "*/*";
            request.Referer = "http://www.flightradar24.com/";
            request.KeepAlive = true;
            //request.Headers.Add("Cookie", string.Format(System.Globalization.CultureInfo.InvariantCulture, "map_lat={0}; map_lon={1}; map_zoom={2}; " + (!string.IsNullOrEmpty(sid) ? "PHPSESSID=" + sid + ";" : string.Empty) + "__utma=109878426.303091014.1316587318.1316587318.1316587318.1; __utmb=109878426.2.10.1316587318; __utmz=109878426.1316587318.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", p.Lat, p.Lng, zoom));

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                //if(string.IsNullOrEmpty(sid))
                //{
                //   var c = response.Headers["Set-Cookie"];
                //   //Debug.WriteLine(c);
                //   if(c.Contains("PHPSESSID"))
                //   {
                //      c = c.Split('=')[1].Split(';')[0];
                //      ret = c;
                //   }
                //}

                using (var responseStream = response.GetResponseStream())
                {
                    using (var read = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string tmp = read.ReadToEnd();
                        //if(!string.IsNullOrEmpty(sid))
                        {
                            ret = tmp;
                        }
                    }
                }

                response.Close();
            }

            return ret;
        }
    }
}
