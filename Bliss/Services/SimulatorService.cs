using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Bliss.Services
{
    public  class SimulatorService:IDisposable
    {
        private int steercancel = 0; 
        private JoystickService joy;
        //private string _direction = "F";
        //public event EventHandler? OnInterfaceData;
        private System.Timers.Timer PositionUpdateTimer;
        //private System.Timers.Timer SteerCancelTimer;
        public bool IsDisposed { get; private set; }
        public SimulatorService(JoystickService _joy)
        {
            //serial = _serial;
            joy = _joy;

            PositionUpdateTimer = new System.Timers.Timer();
            PositionUpdateTimer.Interval = AppSettings.Default.SpeedUpdateInterval;
            PositionUpdateTimer.Elapsed += OnPositionTimer;
            PositionUpdateTimer.Enabled = true;

            joy.OnJoystickData += OnJoysticData;
        }

        public void OnJoysticData(object? obj, EventArgs cmd)
        {
            if (!State.Notices.Any())
            {
                return;
            }
            string command = ParseCommand(State.Notices.Dequeue());//serial.ports["pilotPort"].ReadLine().AsSpan();
            var parts = command.Split('|');
            switch (parts[0])
            {
                case "Error! ":
                    State.Alarms.Enqueue(command.ToString());
                    break;
                case "MOTOR01":
                    Info.PowerLeftState = (int)(double.Parse(parts[2]) / 40.95);
                    Info.LeftReverseState = parts[1] == "R";
                    break;
                case "MOTOR02":
                    Info.PowerRightState = (int)(double.Parse(parts[2]) / 40.95);
                    Info.RightReverseState = parts[1] == "R";
                    break;
                case "JOYST01":
                    OnJoystickCommand(parts[1]);
                    //Info.JoystickCommands.Enqueue(parts[1]);//UI updates
                    //OnInterfaceData?.Invoke(this, EventArgs.Empty);//Dont need
                    break;
                default:
                    State.Alarms.Enqueue($"Error! Unknown command received ({command})");
                    break;
            }
            
        }
        private string ParseCommand(string command)
        {
            try
            {
                int Pos1 = command.IndexOf('<') + 1;
                int Pos2 = command.IndexOf('>');
                return command.Substring(Pos1, Pos2 - Pos1);
            }
            catch
            {
                return $"Error! parsing command({command}).";
            }
        }
        //private void OnCancelTurnTimer(object? sender, ElapsedEventArgs args)
        //{
        //    OnJoystickCommand("Cancel");
        //}

        #region speed and bearing

        private void OnPositionTimer(object? sender, ElapsedEventArgs args)
        {
            CalculatePosition();
            steercancel++;
            if (steercancel > 10)
            {
                steercancel = 0;
                OnJoystickCommand("Cancel");
            }
        }
        #endregion
        public void OnJoystickCommand(string command)
        {
            int currentLeft = Info.PowerLeft;
            int currentRight = Info.PowerLeft;
            switch (command)
            {
                case "Left":
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("MOTOR01", Info.PowerLeft);
                    break;
                case "Right":
                    Info.PowerRight = GetPower(false, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerRight);
                    break;
                case "Forward":
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("MOTOR01", Info.PowerLeft);
                    Info.PowerRight = GetPower(true, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerRight);
                    break;
                case "Backward":
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("MOTOR01", Info.PowerLeft);
                    Info.PowerRight = GetPower(false, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerRight);
                    break;
                case "Stop":
                    Info.PowerLeft = 0;
                    EnqueueCommand("MOTOR01", Info.PowerLeft);
                    Info.PowerRight = 0;
                    EnqueueCommand("MOTOR02", Info.PowerRight);
                    break;
                case "Cancel":
                    if (Info.PowerRight < Info.PowerLeft)
                    {
                        Info.PowerRight = Info.PowerLeft;
                        EnqueueCommand("MOTOR02", Info.PowerRight);
                    }
                    else if (Info.PowerLeft < Info.PowerRight)
                    {
                        Info.PowerLeft = Info.PowerRight;
                        EnqueueCommand("MOTOR01", Info.PowerLeft);
                    }
                    break;
                case "ForwardLeft":
                    Info.PowerRight = GetPower(true, Info.PowerRight);//twice is not a mistake
                    Info.PowerRight = GetPower(true, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerRight);
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("MOTOR01", Info.PowerLeft);
                    break;
                case "ForwardRight":
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);//twice is not a mistake
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("MOTOR01", Info.PowerLeft);
                    Info.PowerRight = GetPower(true, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerRight);
                    break;
                case "BackwardLeft":
                    Info.PowerRight = GetPower(false, Info.PowerRight);//twice is not a mistake
                    Info.PowerRight = GetPower(false, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerRight);
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("MOTOR01", Info.PowerLeft);
                    break;
                case "BackwardRight":
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);//twice is not a mistake
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("MOTOR01", Info.PowerLeft);
                    Info.PowerRight = GetPower(false, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerRight);
                    break;
            }

        }
        private int GetPower(bool increase, int startPower)
        {


            if (increase)
            {
                if (startPower + AppSettings.Default.PilotSpeedIncrement <= AppSettings.Default.PilotMaxPower)
                {
                    startPower += AppSettings.Default.PilotSpeedIncrement;
                }
            }
            else
            {
                if (startPower - AppSettings.Default.PilotSpeedIncrement >= AppSettings.Default.PilotMinPower)
                {
                    startPower -= AppSettings.Default.PilotSpeedIncrement;
                }
            }
            return startPower;
        }
        /// <summary>
        /// Simulate State
        /// </summary>
        /// <param name="motorId"></param>
        /// <param name="speed"></param>
        private void EnqueueCommand(string motorId, int speed)
        {
            //Simulating set new states

            switch (motorId)
            {
                case "MOTOR02"://RIGHT
                    Info.RightReverse = speed < 0;
                    Info.RightReverseState = speed < 0;
                    //Info.Bearing = Info.Bearing + 5;
                    Info.PowerRightState = speed < 0 ? speed * -1 : speed;
                    break;
                case "MOTOR01"://LEFT
                    Info.LeftReverse = speed < 0;
                    Info.LeftReverseState = speed < 0;
                    //Info.Bearing = Info.Bearing - 5;
                    Info.PowerLeftState = speed < 0 ? speed * -1 : speed;
                    break;
            }
            Info.Speed = ((speed * 32 / 200) + (speed * 32 / 200)) / 2;
            

        }

        double rad;// = Info.Bearing * Math.PI / 180; //to radians
        double lat1;// = Info.CurrentLocation.Lat * Math.PI / 180; //to radians
        double lng1;// = Info.CurrentLocation.Lng * Math.PI / 180; //to radians
        double lat;//= Math.Asin(Math.Sin(lat1) * Math.Cos(distance / 6378137) + Math.Cos(lat1) * Math.Sin(distance / 6378137) * Math.Cos(rad));
        double lng;
        int motorBearing;
        /// <summary>
        /// Use Speed and bearing to update the location on the map.
        /// </summary>
        public void CalculatePosition()
        {
            CalculateBearing();
            double distance = (Info.Speed / 24 / 60) * AppSettings.Default.SpeedUpdateInterval;
            if (distance < 0) distance *= -1;
            motorBearing = Indicators.Backward ? Info.Bearing - 180 : Info.Bearing;
            if (motorBearing < 0) motorBearing = motorBearing + 359;
            //rad = Info.Bearing * Math.PI / 180; //to radians
            rad = motorBearing * Math.PI / 180; //to radians
            lat1 = Info.CurrentLocation.Lat * Math.PI / 180; //to radians
            lng1 = Info.CurrentLocation.Lng * Math.PI / 180; //to radians
            lat = Math.Asin(Math.Sin(lat1) * Math.Cos(distance / 6378137) + Math.Cos(lat1) * Math.Sin(distance / 6378137) * Math.Cos(rad));
            lng = lng1 + Math.Atan2(Math.Sin(rad) * Math.Sin(distance / 6378137) * Math.Cos(lat1), Math.Cos(distance / 6378137) - Math.Sin(lat1) * Math.Sin(lat));

            Info.LastLocation = Info.CurrentLocation;
            Info.CurrentLocation = new PointLatLng(lat * 180 / Math.PI, lng * 180 / Math.PI); // to degrees  
        }

        /// <summary>
        /// Get Bearing from indicators
        /// </summary>
        private void CalculateBearing()
        {

            if (Indicators.Turning && Indicators.Right)
            {
                if (Indicators.Forward)
                {
                    Info.Bearing += 5; 
                }
                if (Indicators.Backward)
                {
                    Info.Bearing -= 5;
                }
            }
            if (Indicators.Turning && Indicators.Left)
            {
                if (Indicators.Forward)
                {
                    Info.Bearing -= 5;
                }
                if (Indicators.Backward)
                {
                    Info.Bearing += 5;
                }
            }

        }
        public void Dispose()
        {
            if (!IsDisposed)
            {
                PositionUpdateTimer.Dispose();
                //SteerCancelTimer.Dispose();
                IsDisposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
}
