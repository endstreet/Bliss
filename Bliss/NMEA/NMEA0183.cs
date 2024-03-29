﻿using System.Globalization;

namespace Bliss.NMEA
{
    internal class NMEA0183
    {
        internal unsafe NMEA0183Data? ProcessNMEA0183(string message)
        {
            try
            {

                message = message.Trim();

                fixed (char* cptr = message)
                {
                    if (message.Length < 9)
                        throw new System.Exception("MessageTooShort");
                    else if (cptr[0] != '$')
                        throw new System.Exception("InvalidMessageStart");

                    string talker = message[1..3];
                    string type = message[3..6];
                    int eot_index = message.IndexOf('*');

                    if (eot_index != message.Length - 3)
                        throw new System.Exception("InvalidMessageEnd");

                    byte checksum = byte.Parse(message[(eot_index + 1)..], NumberStyles.HexNumber);

                    for (int i = 1; i < eot_index; ++i)
                        checksum ^= (byte)cptr[i];

                    if (checksum != 0)
                        throw new System.Exception("InvalidChecksum");

                    string raw_data = message[6..eot_index];
                    string[] data = Array.Empty<string>();

                    if (raw_data.Length > 0)
                    {
                        if (raw_data[0] != ',')
                        {
                            throw new System.Exception("InvalidDataStart");
                        }
                        else
                        {
                            data = raw_data[1..].Split(',');
                        }
                    }

                    return ProcessNMEA0183(talker.ToUpperInvariant(), type.ToUpperInvariant(), data);

                }
            }
            catch
            {

                throw;
            }
        }
        private unsafe NMEA0183Data? ProcessNMEA0183(string talker, string type, string[] data)
        {

            switch (type)
            {
                case "GGA":
                    {
                        DateTime utc = ParseTime(data[0]);
                        double lat = ParseLatLon(data[1].TrimStart('0'));
                        double lon = ParseLatLon(data[3].TrimStart('0'));
                        return new NMEA0183Data.GNSSData.GPSFixData(
                            talker,
                            utc,
                            new WGS84Coordinates(
                                lat * (data[2] == "S" ? -1 : 1),
                                lon * (data[4] == "W" ? -1 : 1)
                            ),
                            (GPSQualityIndicator)byte.Parse(data[5]),
                            int.Parse(data[6]),
                            double.Parse(data[7]),
                            double.Parse(data[8]),
                            double.Parse(data[10]),
                            int.TryParse(data[13], out int sid) ? sid : null
                        );
                    }
                //case "GLL":
                //    {
                //        DateTime utc = ParseTime(data[4]);
                //        double lat = ParseLatLon(data[0].TrimStart('0'));
                //        double lon = ParseLatLon(data[2].TrimStart('0'));
                //        return new NMEA0183Data.GNSSData.GeographicPosition(
                //            talker,
                //            utc,
                //            new WGS84Coordinates(
                //                lat * (data[1] == "S" ? -1 : 1),
                //                lon * (data[3] == "W" ? -1 : 1)
                //            ),
                //            data[5] == "A"
                //        );
                //    }
                //case "GSA":
                //    break;
                //case "GSV":
                //    break;
                //case "RMC":
                //    {
                //        DateTime utc = ParseTime(data[8] + data[0]);
                //        double lat = ParseLatLon(data[2].TrimStart('0'));
                //        double lon = ParseLatLon(data[4].TrimStart('0'));
                //        if (string.IsNullOrEmpty(data[7])) data[7] = "0";
                //        return new NMEA0183Data.GNSSData.RecommendedMinimumSpecificGNSSData(
                //            talker,
                //            utc,
                //            new WGS84Coordinates(
                //                lat * (data[3] == "S" ? -1 : 1),
                //                lon * (data[5] == "W" ? -1 : 1)
                //            ),
                //            data[1] == "A",
                //            double.Parse(data[6]) * 1.852,
                //            double.Parse(data[7]),
                //            (RMCModeIndicator)data[11][0]
                //        );
                //    }
                //case "VTG":
                //    if (string.IsNullOrEmpty(data[0])) data[0] = "0"; //set true coarse to 0 for null
                //    if (string.IsNullOrEmpty(data[2])) data[2] = "0"; //set magnetic coarse to 0 for null
                //    return new NMEA0183Data.CourseOverGround(
                //        talker,
                //        double.Parse(data[0]),
                //        double.Parse(data[2]),
                //        double.Parse(data[4]),
                //        double.Parse(data[4])
                //    );
                //case "ZDA":

                //    break;
                //case "TXT":
                //    return new NMEA0183Data.Text(
                //                                    talker,
                //        data[0].ToString(),
                //        data[1].ToString(),
                //        data[2].ToString(),
                //        data[3].ToString()
                //        );
                default:
                    return null;
            }
        }
        private DateTime ParseTime(string data, DateTimeStyles style = DateTimeStyles.AssumeUniversal)
        {
            if (string.IsNullOrEmpty(data)) return DateTime.MinValue;
            int idx = data.IndexOf('.');
            //050422094919.00

            string format = $"{(idx > 6 ? "ddMMyy" : "")}HHmmss.{new string('f', data.Length - 1 - idx)}";
            if (format == "ddMMyyHHmmss.ff")
            {
                return DateTime.ParseExact(data, format, CultureInfo.InvariantCulture);
            }
            else
            {
                return DateTime.ParseExact(data, format, null, style);
            }

        }
        private double ParseLatLon(string data)
        {
            if (string.IsNullOrEmpty(data)) throw new Exception("Invalid location data.");
            return double.Parse(data[..2]) + double.Parse(data[2..]) / 60d;
        }


    }
    public record WGS84Coordinates(double Latitude, double Longitude)
    {
        public bool IsNorth => Latitude > 0;
        public bool IsSouth => Latitude < 0;
        public bool IsWest => Longitude < 0;
        public bool IsEast => Longitude > 0;

        public (bool North, int Degrees, int Minutes, double Seconds) Lat
        {
            get
            {
                double abs = Math.Abs(Latitude);
                int deg = (int)abs;
                int minutes = (int)((abs - deg) * 60);
                double seconds = (abs - deg - minutes / 60d) * 3600;

                return (IsNorth, deg, minutes, seconds);
            }
        }

        public (bool East, int Degrees, int Minutes, double Seconds) Lon
        {
            get
            {
                double abs = Math.Abs(Longitude);
                int deg = (int)abs;
                int minutes = (int)((abs - deg) * 60);
                double seconds = (abs - deg - minutes / 60d) * 3600;

                return (IsEast, deg, minutes, seconds);
            }
        }


        public override string ToString()
        {
            var lat = Lat;
            var lon = Lon;

            return $"{lat.Degrees}°{lat.Minutes}'{lat.Seconds}\"{(IsSouth ? 'S' : 'N')}, {lon.Degrees}°{lon.Minutes}'{lon.Seconds}\"{(IsWest ? 'W' : 'E')}  ({Latitude},{Longitude})";
        }
    }

    public abstract record NMEA0183Data(string Talker)
    {
        public abstract record GNSSData(string Talker, DateTime TimeUTC, WGS84Coordinates Coordinates)
            : NMEA0183Data(Talker)
        {
            public record GPSFixData(string Talker, DateTime TimeUTC, WGS84Coordinates Coordinates, GPSQualityIndicator QualityIndicator, int SatelliteCount, double HDOP, double Altitude, double Resolution, int? GPSStationID)
                : GNSSData(Talker, TimeUTC, Coordinates);

            public record GeographicPosition(string Talker, DateTime TimeUTC, WGS84Coordinates Coordinates, bool IsValid) : GNSSData(Talker, TimeUTC, Coordinates);

            public record RecommendedMinimumSpecificGNSSData(string Talker, DateTime TimeUTC, WGS84Coordinates Coordinates, bool IsValid, double SpeedInKPH, double Course, RMCModeIndicator ModeIndicator) : GNSSData(Talker, TimeUTC, Coordinates);
        }

        //public record CourseOverGround(string Talker, double TrueCourse, double MagneticCourse, double GroundSpeedKPH, RMCModeIndicator ModeIndicator) : NMEA0183Data(Talker);
        public record CourseOverGround(string Talker, double TrueCourse, double MagneticCourse, double GroundSpeedKnots, double GroundSpeedKPH) : NMEA0183Data(Talker);
        public record Text(string Talker, string Byte1, string Byte2, string Byte3, string Text1) : NMEA0183Data(Talker);
    }

    public enum NMEA0183Error
        : byte
    {
        MessageTooShort,
        InvalidMessageStart,
        InvalidMessageEnd,
        InvalidDataStart,
        InvalidChecksum,
        UnknownMessageType,
        MessageParsingError,

        __NOT_YET_IMPLEMENTED__
    }

    public enum GPSQualityIndicator
        : byte
    {
        NoFix = 0,
        ValidSPSFix = 1,
        ValidDifferentialGPSFix = 2
    }

    public enum RMCModeIndicator
    {
        InvalidData = 'N',
        AutonomousMode = 'A',
        DifferentialMode = 'D',
        Estimated = 'E',
    }
}
