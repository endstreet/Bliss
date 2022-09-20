using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Bliss.Services
{
    public  class BleInterfaceService:IDisposable
    {
        //private SerialPortService serial;
        BleService bleConnection;
        private Queue<string> BleCommands;
        //private string _direction = "F";
        public event EventHandler? OnInterfaceData;
       // public event EventHandler? OnSimulatorData;
        public event EventHandler<string>? OnConnection;
        //private System.Timers.Timer SteerCancelTimer;
        public bool IsDisposed { get; private set; }
        public BleInterfaceService(BleService _ble)
        {
            //serial = _serial;
            bleConnection = _ble;
            BleCommands = new Queue<string>();
            //SteerCancelTimer = new System.Timers.Timer();
            //SteerCancelTimer.Enabled = false;
            //SteerCancelTimer.Interval = AppSettings.Default.PilotCancelTurn;
            //SteerCancelTimer.Elapsed += OnCancelTurnTimer;
            bleConnection.OnBleData += ReceiveBleData;
            bleConnection.OnBleConnection += BleConnection;
            //serial.OnPilotData += ReceiveBleData;
        }
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
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerLeft);
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
                    if (Info.PowerRight > Info.PowerLeft)
                    {
                        Info.PowerRight = Info.PowerLeft;
                        EnqueueCommand("MOTOR02", Info.PowerRight);
                    }
                    else if (Info.PowerLeft > Info.PowerRight)
                    {
                        Info.PowerLeft = Info.PowerRight;
                        EnqueueCommand("MOTOR01", Info.PowerLeft);
                    }
                    break;
                case "ForwardLeft":
                    Info.PowerRight = GetPower(true, Info.PowerRight);
                    Info.PowerRight = GetPower(true, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerRight);
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("MOTOR01", Info.PowerLeft);
                    break;
                case "ForwardRight":
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("MOTOR01", Info.PowerLeft);
                    Info.PowerRight = GetPower(true, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerRight);
                    break;
                case "BackwardLeft":
                    Info.PowerRight = GetPower(false, Info.PowerRight);
                    Info.PowerRight = GetPower(false, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("MOTOR02", Info.PowerRight);
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("MOTOR01", Info.PowerLeft);
                    break;
                case "BackwardRight":
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
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
        private void EnqueueCommand(string motorId, int speed)
        {
            string _direction;
            switch (motorId)
            {
                case "MOTOR02":
                    Info.RightReverse = speed < 0;
                    break;
                case "MOTOR01":
                    Info.LeftReverse = speed < 0;
                    break;
            }
            _direction = speed < 0 ? "R" : "F";
            speed = speed < 0 ? speed * -1 : speed;
            string command = $"{motorId}{_direction}{(int)(speed * 40.95)}";
            //Not Simulating send the commands to the motors
            BleCommands.Enqueue(command);
            SendCommands();
 
        }
        private async void SendCommands()
        {
            string command = "";
            try
            {
                if(BleCommands.Count > 0)
                {
                   command = BleCommands.Dequeue();
                   await bleConnection.SendBTCommand(command);
                }

            }
            catch
            {
                //serial.ports.Remove("pilotPort");
                //serial.ScanDevices();
                State.Alarms.Enqueue($"Error! Sending command ({command}). ");
            }
        }
        public void ReceiveBleData(object? obj, string cmd)
        {
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
                    Info.JoystickCommands.Enqueue(parts[1]);//UI updates
                    OnInterfaceData?.Invoke(this, EventArgs.Empty);
                    break;
                default:
                    State.Alarms.Enqueue($"Error! Unknown command received ({command})");
                    break;
            }
            
        }

        public void BleConnection(object? obj, string state)
        {
            OnConnection?.Invoke(this,state);
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
        public void Dispose()
        {
            if (!IsDisposed)
            {
                //SteerCancelTimer.Dispose();
                IsDisposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
}
