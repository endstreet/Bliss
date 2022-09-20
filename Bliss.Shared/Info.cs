using GeoCoordinatePortable;
using GMap.NET;
using System.Text;

namespace Bliss.Services
{
    public static class Info
    {
        /// <summary>
        /// Speed in KM/H
        /// </summary>
        public static double Speed;
        public static Queue<string> JoystickCommands = new Queue<string>();//Console joystic commands go in here
        public static double Depth = 0;
        public static string CompassBearing = "0";
        public static bool LeftReverse = false;
        public static bool RightReverse = false;
        public static int PowerLeft = 0;//%
        public static int PowerRight = 0;
        public static int PowerLeftState = 0;//%
        public static int PowerRightState = 0;
        public static bool LeftReverseState = false;
        public static bool RightReverseState = false;
        public static bool Autopilot = false;

        private static bool Reverse = false;

        public static void OnReverse(double fromSpeed, double toSpeed,double bearing)
        {
            bool forwardAfterReverse = fromSpeed < 0 && (toSpeed > 0 || toSpeed == 0);
            Reverse = toSpeed < 0 && (fromSpeed > 0 || fromSpeed == 0);

            if (forwardAfterReverse || Reverse)
            {
                //Todo:Switch reverse on
                if (bearing >= 180)
                {
                    bearing -= 180;
                }
                else
                {
                    bearing += 180;
                }
            }
            Bearing = (int)bearing;
        }

        //System Status
        //Input Control Status
        //Motor Control Status
        //Motor Temperature
        //Motor RPM

        
        public static PointLatLng LastLocation; //new PointLatLng(-28.804256, 32.043904);
        public static PointLatLng CurrentLocation;//new PointLatLng(-28.804256, 32.043904);

        public static bool MapShowBearing = false;

        private static int _bearing = 0;
        public static int Bearing
        {
            get
            {
                return _bearing;
            }
            set
            {
                if (value > 359)
                {
                    if (value == 360)
                    {
                        _bearing = 0;
                    }
                    else
                    {
                        _bearing = value - 359;
                        
                    }
                    
                }
                else if(value < 0)
                {
                    if (value == -1)
                    {
                        _bearing = 359;
                    }
                    else 
                    {
                        _bearing = value + 359;
                    }
                }
                else
                {
                    _bearing = value;
                }
            }
        }

        public static bool HasPosition
        {
            get 
            { 
                return LastLocation.Lng > 0 && CurrentLocation.Lng > 0;
            }
        }
        //Navigation
        //Destination
        //ETA
        //Cruise Time
        //Remaining Cruise Time
        //Total Distance
        //Remaining Distance

        //Waypoint
        //ETA
        //Cruise Time
        //Remaining Cruise Time
        //Total Distance
        //Remaining Distance

        /// <summary>
        /// Calculate speed from time and distance travelled
        /// </summary>
        /// <param name="duration"></param>
        public static void CalculateSpeed(double duration)
        {
            if (!HasPosition) return;
            var sCoord = new GeoCoordinate(LastLocation.Lat, LastLocation.Lng);
            var eCoord = new GeoCoordinate(CurrentLocation.Lat, CurrentLocation.Lng);
            var distance = sCoord.GetDistanceTo(eCoord);//Returns meters
            if (distance < 8)//Gps deviation
            {
                Speed = 0;
                //Bearing = 0;
                return;
            }
            //16.6667 meter per minute = 1 km/h
            Speed = distance / (1 / duration); ///16.6667;
            CalculateBearing();
        }

        public static void SetCurrentLocation(PointLatLng CurrentLocation)
        {
            LastLocation = Info.CurrentLocation;
            Info.CurrentLocation = CurrentLocation;
        }

        /// <summary>
        /// Todo: this must me implimented to calculate Bearing while moving
        /// </summary>
        static void CalculateBearing()
        {
            if (State.IsSimulating)
            {
                //Bearing set by joystic01
            }
            else
            {
                //Bearing by angle between current and previous position
                if (!HasPosition) return;
                var dLon = ToRad(CurrentLocation.Lng - LastLocation.Lng);
                var dPhi = Math.Log(Math.Tan(ToRad(CurrentLocation.Lat) / 2 + Math.PI / 4) / Math.Tan(ToRad(LastLocation.Lat) / 2 + Math.PI / 4));
                if (Math.Abs(dLon) > Math.PI)
                {
                    dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
                }
                Bearing = ToBearing(Math.Atan2(dLon, dPhi));
            }
        }

        private static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        private static double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        private static int ToBearing(double radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (int)(ToDegrees(radians) + 360) % 360;
        }
    }

    //public class PilotCommand
    //{
    //    public bool SpeedUp { get; set; }
    //    public bool SpeedDown { get; set; }
    //    public bool LeftUp { get; set; }
    //    public bool LeftDown { get; set; }
    //    public bool RightUp { get; set; }
    //    public bool RightDown { get; set; }
    //    public bool Left { get; set; }
    //    public bool Right { get; set; }
    //    public bool Stop { get; set; }
    //    public bool Cancel { get; set; }
    //    public bool Alarm { get; set; }

    //    //public PilotCommand()
    //    //{
    //    //}

    //    //public PilotCommand(string[] xboxCommand)
    //    //{
    //    //    if (xboxCommand.Length == 1)
    //    //    {
    //    //        SpeedUp = xboxCommand[0] == "DPadUp";
    //    //        SpeedDown = xboxCommand[0] == "DPadDown";
    //    //        Left = xboxCommand[0] == "DPadLeft";
    //    //        Right = xboxCommand[0] == "DPadRight";
    //    //    }
    //    //    else 
    //    //    { 
    //    //        LeftUp = xboxCommand.Contains("DPadLeft") && xboxCommand.Contains("DPadUp");
    //    //        LeftDown = xboxCommand.Contains("DPadLeft") && xboxCommand.Contains("DPadDown");
    //    //        RightUp = xboxCommand.Contains("DPadRight") && xboxCommand.Contains("DPadUp");
    //    //        RightDown = xboxCommand.Contains("DPadRight") && xboxCommand.Contains("DPadDown");
    //    //    }
    //    //}

    //}

    public static class State
    {
        public static bool IsSimulating = true;
        public static bool AutoPilot;
        public static bool AutopilotCommand;
        public static bool EnableRightMotor;
        public static bool EnableLeftMotor;

        public static bool Alarm
        {
            get
            {
                return Alarms.Count > 0;
            }
        }
        public static Queue<string> Alarms = new Queue<string>();
        public static Queue<string> Notices = new Queue<string>();

        public static new string ToString() 
        {
            StringBuilder sb = new StringBuilder();
            List<string> snap = Alarms.ToList();
            foreach(string alarm in snap)
            {
                sb.Append( alarm + " |");
            }
            return sb.ToString();
        }
    }

    public static class Indicators
    {
        public static bool Forward
        {
            get
            {
                if (Info.RightReverse || Info.LeftReverse)
                {
                    return false;
                }
                else
                {
                    return Info.PowerRightState > 0 || Info.PowerLeftState > 0;
                }
            }
        }
        public static bool Backward
        {
            get
            {
                if (!Info.RightReverse || !Info.LeftReverse)
                {
                    return false;
                }
                else
                {
                    return Info.PowerRightState > 0 || Info.PowerLeftState > 0;
                }
            }
        }
        public static bool Turning
        {
            get
            {
                if (Info.RightReverse == Info.LeftReverse)
                {
                    return Info.PowerRightState != Info.PowerLeftState;
                }
                else
                {
                    return Info.PowerRightState != Info.PowerLeftState || Info.PowerRightState == Info.PowerLeftState;
                }
            }
        }
        public static bool Right
        {
            get
            {
                if (Info.RightReverse == Info.LeftReverse)
                {
                    if (Info.RightReverse)
                    {
                        return Info.PowerRightState > Info.PowerLeftState;
                    }
                    else
                    {
                        return Info.PowerLeftState > Info.PowerRightState;
                    }
                }
                else
                {
                    return Info.RightReverse;
                }
            }
        }
        public static bool Left
        {
            get
            {
                if (Info.RightReverse == Info.LeftReverse)
                {
                    if (Info.RightReverse)
                    {
                        return Info.PowerLeftState > Info.PowerRightState;
                    }
                    else
                    {
                        return Info.PowerRightState > Info.PowerLeftState;
                    }

                }
                else
                {
                    return Info.LeftReverse;
                }
            }
        }
    }
}
