using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Bliss.Services
{
    public  class MotorService:IDisposable
    {
        private SerialPortService serial;
        private Queue<string> SerialCommand;
        private string _direction = "F";
        public event EventHandler? OnMotorData;
        private System.Timers.Timer SteerCancelTimer;
        public bool IsDisposed { get; private set; }
        public MotorService(SerialPortService _serial)
        {
            serial = _serial;
            SerialCommand = new Queue<string>();
            SteerCancelTimer = new System.Timers.Timer();
            SteerCancelTimer.Enabled = true;
            SteerCancelTimer.Interval = AppSettings.Default.PilotCancelTurn;
            SteerCancelTimer.Elapsed += OnCancelTurnTimer;
            serial.OnPilotData += ReceivePilotData;
        }
        public void OnPilotCommand(string command)
        {
            int currentLeft = Info.PowerLeft;
            int currentRight = Info.PowerLeft;
            switch (command)
            {
                case "Left":
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("LEFTM", Info.PowerLeft);
                    break;
                case "Right":
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentRight != Info.PowerRight) EnqueueCommand("RIGHT", Info.PowerLeft);
                    break;
                case "Forward":
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("LEFTM", Info.PowerLeft);
                    Info.PowerRight = GetPower(true, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("RIGHT", Info.PowerRight);
                    break;
                case "Backward":
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("LEFTM", Info.PowerLeft);
                    Info.PowerRight = GetPower(false, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("RIGHT", Info.PowerRight);
                    break;
                case "Stop":
                    Info.PowerLeft = 0;
                    EnqueueCommand("LEFTM", Info.PowerLeft);
                    Info.PowerRight = 0;
                    EnqueueCommand("RIGHT", Info.PowerRight);
                    break;
                case "Cancel":
                    if (Info.PowerRight > Info.PowerLeft)
                    {
                        Info.PowerRight = Info.PowerLeft;
                        EnqueueCommand("RIGHT", Info.PowerRight);
                    }
                    else if (Info.PowerLeft > Info.PowerRight)
                    {
                        Info.PowerLeft = Info.PowerRight;
                        EnqueueCommand("LEFTM", Info.PowerLeft);
                    }
                    break;
                case "ForwardLeft":
                    Info.PowerRight = GetPower(true, Info.PowerRight);
                    Info.PowerRight = GetPower(true, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("RIGHT", Info.PowerRight);
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("LEFTM", Info.PowerLeft);
                    break;
                case "ForwardRight":
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);
                    Info.PowerLeft = GetPower(true, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("LEFTM", Info.PowerLeft);
                    Info.PowerRight = GetPower(true, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("RIGHT", Info.PowerRight);
                    break;
                case "BackwardLeft":
                    Info.PowerRight = GetPower(false, Info.PowerRight);
                    Info.PowerRight = GetPower(false, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("RIGHT", Info.PowerRight);
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("LEFTM", Info.PowerLeft);
                    break;
                case "BackwardRight":
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    Info.PowerLeft = GetPower(false, Info.PowerLeft);
                    if (currentLeft != Info.PowerLeft) EnqueueCommand("LEFTM", Info.PowerLeft);
                    Info.PowerRight = GetPower(false, Info.PowerRight);
                    if (currentRight != Info.PowerRight) EnqueueCommand("RIGHT", Info.PowerRight);
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
            switch (motorId)
            {
                case "RIGHT":
                    Info.RightReverse = speed < 0;
                    break;
                case "LEFTM":
                    Info.LeftReverse = speed < 0;
                    break;
            }
            _direction = speed < 0 ? "R" : "F";
            speed = speed < 0 ? speed * -1 : speed;
            string command = $"{motorId}{_direction}{(int)(speed * 40.95)}";
            if (!State.IsSimulating)
            {
                SerialCommand.Enqueue(command);
                SendCommands();
            }
            else
            {
                switch (motorId)
                {
                    case "RIGHT":
                        Info.RightReverse = speed < 0;
                        Info.RightReverseState = speed < 0;
                        break;
                    case "LEFTM":
                        Info.LeftReverse = speed < 0;
                        Info.LeftReverseState = speed < 0;
                        break;
                }
                _direction = speed < 0 ? "R" : "F";
                speed = speed < 0 ? speed * -1 : speed;
            }
        }
        private void SendCommands()
        {
            try
            {
                //Todo:wait for commands to complete
                if (serial.ports.ContainsKey("pilotPort"))
                {
                    while (SerialCommand.Count > 0)
                    {
                        string buffer = SerialCommand.Dequeue();
                        if (buffer == null)
                        {
                            continue;
                        }
                        serial.ports["pilotPort"].WriteLine(buffer);
                    }
                }
                else
                {
                    serial.ScanDevices();
                    State.Alarms.Enqueue("PilotPort not available. Command ignored.");
                }
            }
            catch(Exception ex)
            {
                serial.ports.Remove("pilotPort");
                serial.ScanDevices();
                State.Alarms.Enqueue("PilotPort comm error. Command ignored.");
            }
        }
        public void ReceivePilotData(object? obj, EventArgs e)
        {
            ReadOnlySpan<char> command = serial.ports["pilotPort"].ReadLine().AsSpan();
            if (command.Length < 7) return;
            switch (command.Slice(0, 5).ToString())
            {
                case "Error":
                    State.Alarms.Enqueue(command.ToString());
                    break;
                case "LEFTM":
                    Info.PowerLeftState = (int)(double.Parse(command.Slice(6).ToString().TrimEnd('\n')) / 40.95);
                    Info.LeftReverseState = command.Slice(5, 1).ToString() == "R";
                    break;
                case "RIGHT":
                    Info.PowerRightState = (int)(double.Parse(command.Slice(6).ToString().TrimEnd('\n')) / 40.95);
                    Info.RightReverseState = command.Slice(5, 1).ToString() == "R";
                    break;
                default:
                    //Todo: ignore startup..
                    break;
            }
            OnMotorData?.Invoke(this, EventArgs.Empty);
        }
        private void OnCancelTurnTimer(object? sender, ElapsedEventArgs args)
        {
            OnPilotCommand("Cancel");
        }
        public void Dispose()
        {
            if (!IsDisposed)
            {
                SteerCancelTimer.Dispose();
                serial.Dispose();
                IsDisposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
}
