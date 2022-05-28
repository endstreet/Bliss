using GeoCoordinatePortable;
using GMap.NET;


namespace Bliss.Services
{
    public static class Shared
    {
        //Pilot controls
        public static double Speed = 0;
        public static double Bearing = 0;
        public static bool Reverse = false;
        public static int PowerLeft = 0;
        public static int PowerRight = 0;
        public static bool Autopilot = false;
        public static string PilotCommand = "Initialising";
        public static Queue<PilotCommand> PilotCommands = new Queue<PilotCommand>();

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
            Bearing = bearing;
        }

        //System Status
        //Input Control Status
        //Motor Control Status
        //Motor Temperature
        //Motor RPM

        //Navigation
        public static PointLatLng LastLocation = new PointLatLng(-28.804256, 32.043904);
        public static PointLatLng CurrentLocation = new PointLatLng(-28.804256, 32.043904);
        //Cruise (Name)
        //Remaining Cruise Time
        //Remaining Cruise Distance
        //Remaining Time to Waypoint
        //Remaining Distance to Waypoint
        //Waypoint Deviation

        public static void CalculateSpeed(double duration)
        {
            var sCoord = new GeoCoordinate(LastLocation.Lat, LastLocation.Lng);
            var eCoord = new GeoCoordinate(CurrentLocation.Lat, CurrentLocation.Lng);
            var distance = sCoord.GetDistanceTo(eCoord);//Returns meters
            if (distance < 8)//Gps deviation
            {
                Speed = 0;
                Bearing = 0;
                return;
            }
            //16.6667 meter per minute = 1 km/h
            Speed = distance / (1 / duration); ///16.6667;
            CalculateBearing();
        }

        public static void SetCurrentLocation(PointLatLng CurrentLocation)
        {
            LastLocation = Shared.CurrentLocation;
            Shared.CurrentLocation = CurrentLocation;
        }
        static void CalculateBearing()
        {
            var dLon = ToRad(CurrentLocation.Lng - LastLocation.Lng);
            var dPhi = Math.Log(Math.Tan(ToRad(CurrentLocation.Lat) / 2 + Math.PI / 4) / Math.Tan(ToRad(LastLocation.Lat) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
            {
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            }
            Bearing = ToBearing(Math.Atan2(dLon, dPhi));
        }

        private static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        private static double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        private static double ToBearing(double radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ToDegrees(radians) + 360) % 360;
        }
    }

    public class PilotCommand
    {
        public bool SpeedUp { get; set; }
        public bool SpeedDown { get; set; }
        public bool TurnLeft { get; set; }
        public bool TurnRight { get; set; }
        public bool Stop { get; set; }
        public bool Reverse { get; set; }
        public bool Aux { get; set; }

    }
}
