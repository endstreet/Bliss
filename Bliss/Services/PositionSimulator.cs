using GMap.NET;
using System.Timers;

namespace Bliss.Services
{
    internal class SimulationService
    {
        System.Timers.Timer PositionUpdateTimer;
        //Position
        double rad;// = Info.Bearing * Math.PI / 180; //to radians
        double lat1;// = Info.CurrentLocation.Lat * Math.PI / 180; //to radians
        double lng1;// = Info.CurrentLocation.Lng * Math.PI / 180; //to radians
        double lat;//= Math.Asin(Math.Sin(lat1) * Math.Cos(distance / 6378137) + Math.Cos(lat1) * Math.Sin(distance / 6378137) * Math.Cos(rad));
        double lng;
        //
        double distance;
        public SimulationService()
        {
            PositionUpdateTimer = new System.Timers.Timer();
            PositionUpdateTimer.Enabled = true;
            PositionUpdateTimer.Interval = 1000;
            PositionUpdateTimer.Elapsed += OnPositionTimer;
        }

        private void OnPositionTimer(object? sender, ElapsedEventArgs args)
        {
            distance = (Info.Speed / 24 / 60) * 1000;
            if (distance < 0) distance *= -1;
            Resultposition();
        }

        public void OnPilotCommand(PilotCommand command)
        {
            if (command.SpeedUp)
            {
                if ((int)Info.Speed < 10)
                {
                    Info.OnReverse(Info.Speed, Info.Speed + 1, Info.Bearing);
                    Info.Speed += 1;
                }
            }
            if (command.SpeedDown)
            {
                if ((int)Info.Speed > -10)
                {
                    Info.OnReverse(Info.Speed, Info.Speed - 1, Info.Bearing);
                    Info.Speed -= 1;

                }
            }
            if (command.TurnLeft)
            {
                Info.Bearing = Info.Bearing - (1 * Info.Speed);
            }
            if (command.TurnRight)
            {
                Info.Bearing = Info.Bearing + (1 * Info.Speed);
                
            }


        }

        private void Resultposition()
        {
            rad = Info.Bearing * Math.PI / 180; //to radians
            lat1 = Info.CurrentLocation.Lat * Math.PI / 180; //to radians
            lng1 = Info.CurrentLocation.Lng * Math.PI / 180; //to radians
            lat = Math.Asin(Math.Sin(lat1) * Math.Cos(distance / 6378137) + Math.Cos(lat1) * Math.Sin(distance / 6378137) * Math.Cos(rad));
            lng = lng1 + Math.Atan2(Math.Sin(rad) * Math.Sin(distance / 6378137) * Math.Cos(lat1), Math.Cos(distance / 6378137) - Math.Sin(lat1) * Math.Sin(lat));

            Info.LastLocation = Info.CurrentLocation;
            Info.CurrentLocation = new PointLatLng(lat * 180 / Math.PI, lng * 180 / Math.PI); // to degrees  
        }

        //public double GetDistance(PointLatLng fromPoint, PointLatLng toPoint)
        //{
        //    var sCoord = new GeoCoordinate(fromPoint.Lat, fromPoint.Lng);
        //    var eCoord = new GeoCoordinate(toPoint.Lat, toPoint.Lng);
        //    return sCoord.GetDistanceTo(eCoord);
        //}

    }
}
