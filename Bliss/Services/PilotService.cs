using Bliss.Models;
using System.Timers;

namespace Bliss.Services
{
    public sealed class PilotService : IDisposable
    {

        //private JoystickService joystickService;
        //private SerialPortService serial;
        private BleInterfaceService interfaceService;
        private SimulatorService simulatorService;
        //private BleService btService;
        //private Queue<string> SerialCommand = new Queue<string>();
        public WayPoint? Target { get; set; }
        //public gpsService gps;

        //public List<string> ActivePorts
        //{
        //    get { return serial.ports.Keys.ToList(); }
        //}


        public event EventHandler? OnInterfaceData;
        //public event EventHandler? OnJoyStickData;
        //public event EventHandler? OnMotorData;

        //private System.Timers.Timer PositionUpdateTimer;

        public bool IsDisposed { get; private set; }
        //public Action<object?, string> Motor_OnInterfaceData { get; }

        public PilotService(BleInterfaceService _ifService, SimulatorService _simulator)//, JoystickService _joystick)
        {
            interfaceService = _ifService;
            simulatorService = _simulator;
            //joystickService = _joystick;

            //PositionUpdateTimer = new System.Timers.Timer();
            //PositionUpdateTimer.Interval = AppSettings.Default.SpeedUpdateInterval;
            //PositionUpdateTimer.Elapsed += OnPositionTimer;
            //PositionUpdateTimer.Enabled = true;

            interfaceService.OnConnection += OnConnection;

        }

        private void OnInterFaceData(object? sender, EventArgs e)
        {
            OnInterfaceData?.Invoke(null, EventArgs.Empty);
        }

        private void OnConnection(object? sender, string state)
        {
            switch (state)
            {
                case "Connected":
                    //simulatorService.OnInterfaceData -= OnInterFaceData;
                    //interfaceService.OnInterfaceData += OnInterFaceData;
                    State.IsSimulating = false;
                    break;
                case "DisConnected":
                    //interfaceService.OnInterfaceData -= OnInterFaceData;
                    //simulatorService.OnInterfaceData += OnInterFaceData;
                    State.IsSimulating = true;
                    break;

            }

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

        private void OnPositionTimer(object? sender, ElapsedEventArgs args)
        {
            //Huh?
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                interfaceService.Dispose();
                // btService.Dispose();
                //PositionUpdateTimer.Dispose();
                //joystick.Dispose();
                //serial.Dispose();
                //motorService.Dispose();
                //gps.Dispose();
                IsDisposed = true;
            }

            GC.SuppressFinalize(this);
        }



    }
}
