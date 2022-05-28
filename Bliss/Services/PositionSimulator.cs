using GeoCoordinatePortable;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Bliss.Services;

namespace Bliss.Services
{
    internal class SimulationService
    {
        public double Bearing;
        public double Speed;
        System.Timers.Timer PositionUpdateTimer;

        public SimulationService()
        {
            PositionUpdateTimer = new System.Timers.Timer();
            PositionUpdateTimer.Enabled = true;
            PositionUpdateTimer.Interval = 1000;
            PositionUpdateTimer.Elapsed += OnPositionTimer;
        }

        private void OnPositionTimer(object? sender, ElapsedEventArgs args)
        {

            double distance = (Speed / 24 / 60) * 1000;
            if (distance < 0) distance *= -1;
            Resultposition(distance);
        }

        public void OnPilotCommand(PilotCommand command)
        {
            if (command.SpeedUp)
            { 
                if((int)Speed < 10)
                {
                    Shared.OnReverse(Speed, Speed + 1,Bearing);
                    Speed += 1;
                }
            }
            if (command.SpeedDown)
            {
                if ((int)Speed > -10)
                {
                    Shared.OnReverse(Speed, Speed - 1,Bearing);
                    Speed -= 1;

                }
            }
            if (command.TurnLeft)
            {
                Bearing = Bearing - (1 * Speed);
                if (Bearing < 0)
                {
                    Bearing = Bearing + 359;
                }

            }
            if (command.TurnRight)
            {
                Bearing = Bearing + (1 * Speed);
                if(Bearing > 359)
                {
                    Bearing = Bearing - 359;
                }
            }


        }

        private void Resultposition(double distance)
        {
            double rad = Bearing * Math.PI / 180; //to radians
            double lat1 = Shared.CurrentLocation.Lat * Math.PI / 180; //to radians
            double lng1 = Shared.CurrentLocation.Lng * Math.PI / 180; //to radians
            double lat = Math.Asin(Math.Sin(lat1) * Math.Cos(distance / 6378137) + Math.Cos(lat1) * Math.Sin(distance / 6378137) * Math.Cos(rad));
            double lng = lng1 + Math.Atan2(Math.Sin(rad) * Math.Sin(distance / 6378137) * Math.Cos(lat1), Math.Cos(distance / 6378137) - Math.Sin(lat1) * Math.Sin(lat));

            Shared.LastLocation = Shared.CurrentLocation;
            Shared.CurrentLocation = new PointLatLng(lat * 180 / Math.PI, lng * 180 / Math.PI); // to degrees  
        }

        public double GetDistance(PointLatLng fromPoint, PointLatLng toPoint)
        {
            var sCoord = new GeoCoordinate(fromPoint.Lat, fromPoint.Lng);
            var eCoord = new GeoCoordinate(toPoint.Lat, toPoint.Lng);
            return sCoord.GetDistanceTo(eCoord);
        }

    }
}
