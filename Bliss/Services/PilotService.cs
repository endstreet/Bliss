using Bliss.Models;
using GMap.NET;
using System.Timers;

namespace Bliss.Services
{
    public sealed class PilotService : IDisposable
    {

        //private JoystickService joystick;
        //private SerialPortService serial;
        private BleInterfaceService interfaceService;
        //private BleService btService;
        //private Queue<string> SerialCommand = new Queue<string>();
        public WayPoint? Target { get; set; }
        //public gpsService gps;
        
        //public List<string> ActivePorts
        //{
        //    get { return serial.ports.Keys.ToList(); }
        //}
        

        public event EventHandler? OnInterfaceData;
        public event EventHandler? OnJoyStickData;
        //public event EventHandler? OnMotorData;

        private System.Timers.Timer PositionUpdateTimer;

        public bool IsDisposed { get; private set; }
        //public Action<object?, string> Motor_OnInterfaceData { get; }

        public PilotService(BleInterfaceService _ifService)
        {
            interfaceService = _ifService;

            PositionUpdateTimer = new System.Timers.Timer();
            PositionUpdateTimer.Interval = AppSettings.Default.SpeedUpdateInterval * 1000;
            PositionUpdateTimer.Elapsed += OnPositionTimer;
            PositionUpdateTimer.Enabled = true;

            interfaceService.OnInterfaceData += OnInterFaceData;


        }

        private void OnInterFaceData(object? sender, EventArgs e)
        {
            OnInterfaceData?.Invoke(null, EventArgs.Empty);
        }

        //private void Joystick_OnJoystickData(object? sender, EventArgs e)
        //{

        //    //while(Info.PilotCommands.Count >0)
        //    //{
        //    //    //OnJoystickData?.Invoke(null, EventArgs.Empty);
        //    //    //motorService.OnPilotCommand(Info.PilotCommands.Dequeue());
        //    //    string command = Info.PilotCommands.Peek();
        //    //    btService.SendBTCommand("LEFTMF100");// Info.PilotCommands.Dequeue());
        //    //}
        //    btService.SendBTCommand("LEFTMF100");
        //}
        //private void Bluetooth_OnBtData(object? sender, String e)
        //{
        //    //string command = Info.PilotCommands.Peek();
        //    //btService.SendBTCommand(Info.PilotCommands.Dequeue());
        //    if (!State.Notices.Any()) return;
        //        ReadOnlySpan<char> command = State.Notices.Dequeue();//serial.ports["pilotPort"].ReadLine().AsSpan();
        //        if (command.Length < 7) return;
        //        switch (command.Slice(0, 5).ToString())
        //        {
        //            case "Error":
        //                State.Alarms.Enqueue(command.ToString());
        //                break;
        //            case "LEFTM":
        //                Info.PowerLeftState = (int)(double.Parse(command.Slice(6).ToString().TrimEnd('\n')) / 40.95);
        //                Info.LeftReverseState = command.Slice(5, 1).ToString() == "R";
        //                break;
        //            case "RIGHT":
        //                Info.PowerRightState = (int)(double.Parse(command.Slice(6).ToString().TrimEnd('\n')) / 40.95);
        //                Info.RightReverseState = command.Slice(5, 1).ToString() == "R";
        //                break;
        //            default:
        //                //Todo: ignore startup..
        //                break;
        //        }
        //        OnMotorData?.Invoke(this, EventArgs.Empty);
        //}
        //public void OnJoysticCommand(string command)
        //{
        //    motorService.OnJoystickCommand(command);
        //}

        public void Dispose()
        {
            if (!IsDisposed)
            {
                interfaceService.Dispose();
               // btService.Dispose();
                PositionUpdateTimer.Dispose();
                //joystick.Dispose();
                //serial.Dispose();
                //motorService.Dispose();
                //gps.Dispose();
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
