using Bliss.Models;
using GMap.NET;
using System.Timers;

namespace Bliss.Services
{
    public sealed class PilotService : IDisposable
    {

        private JoystickService joystick;
        private SerialPortService serial;
        private MotorService motorService;
        //private Queue<string> SerialCommand = new Queue<string>();
        public WayPoint? Target { get; set; }
        public gpsService gps;
        
        public List<string> ActivePorts
        {
            get { return serial.ports.Keys.ToList(); }
        }

        public event EventHandler? OnJoystickData;
        public event EventHandler? OnMotorData;

        private System.Timers.Timer PositionUpdateTimer;
        

        public bool IsDisposed { get; private set; }

        public PilotService(JoystickService _joystick, gpsService _gps,MotorService _motorService, SerialPortService _serial)
        {
            joystick = _joystick;
            serial = _serial;
            motorService = _motorService;
            gps = _gps;

            PositionUpdateTimer = new System.Timers.Timer();
            PositionUpdateTimer.Interval = AppSettings.Default.SpeedUpdateInterval * 1000;
            PositionUpdateTimer.Elapsed += OnPositionTimer;
            PositionUpdateTimer.Enabled = true;

            
            joystick.OnJoystickData += Joystick_OnJoystickData;
            motorService.OnMotorData += Motor_OnMotorData;

        }

        private void Motor_OnMotorData(object? sender, EventArgs e)
        {
            OnMotorData?.Invoke(null, EventArgs.Empty);
        }

        private void Joystick_OnJoystickData(object? sender, EventArgs e)
        {
            
            while(Info.PilotCommands.Count >0)
            {
                OnJoystickData?.Invoke(null, EventArgs.Empty);
                motorService.OnPilotCommand(Info.PilotCommands.Dequeue());
            }
        }
        
        public void OnPilotCommand(string command)
        {
            motorService.OnPilotCommand(command);
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                PositionUpdateTimer.Dispose();
                joystick.Dispose();
                serial.Dispose();
                motorService.Dispose();
                gps.Dispose();
                IsDisposed = true;
            }

            GC.SuppressFinalize(this);
        }

        #region speed and bearing

        private void OnPositionTimer(object? sender, ElapsedEventArgs args)
        {

            if(State.IsSimulating)
            {
                Resultposition();
            }
            else
            {
                if (!serial.ports.ContainsKey("pilotPort"))
                {
                    serial.ScanDevices();
                }
               
                Info.CalculateSpeed(PositionUpdateTimer.Interval);
            }
            
        }
        #endregion

        #region Simulation
        double rad;// = Info.Bearing * Math.PI / 180; //to radians
        double lat1;// = Info.CurrentLocation.Lat * Math.PI / 180; //to radians
        double lng1;// = Info.CurrentLocation.Lng * Math.PI / 180; //to radians
        double lat;//= Math.Asin(Math.Sin(lat1) * Math.Cos(distance / 6378137) + Math.Cos(lat1) * Math.Sin(distance / 6378137) * Math.Cos(rad));
        double lng;
        //private string result;

        private void Resultposition()
        {
            double distance = (Info.Speed / 24 / 60) * 1000;
            if (distance < 0) distance *= -1;

            rad = Info.Bearing * Math.PI / 180; //to radians
            lat1 = Info.CurrentLocation.Lat * Math.PI / 180; //to radians
            lng1 = Info.CurrentLocation.Lng * Math.PI / 180; //to radians
            lat = Math.Asin(Math.Sin(lat1) * Math.Cos(distance / 6378137) + Math.Cos(lat1) * Math.Sin(distance / 6378137) * Math.Cos(rad));
            lng = lng1 + Math.Atan2(Math.Sin(rad) * Math.Sin(distance / 6378137) * Math.Cos(lat1), Math.Cos(distance / 6378137) - Math.Sin(lat1) * Math.Sin(lat));

            Info.LastLocation = Info.CurrentLocation;
            Info.CurrentLocation = new PointLatLng(lat * 180 / Math.PI, lng * 180 / Math.PI); // to degrees  
        }
        #endregion

    }
}
