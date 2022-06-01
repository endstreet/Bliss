using GMap.NET;
using System.IO.Ports;
using System.Timers;

namespace Bliss.Services
{
    public sealed class PilotService : IDisposable
    {
        public bool IsDisposed { get; private set; }

        private SerialPortService serial;
        private Queue<string> SerialCommand = new Queue<string>();


        public GPSSensor gps;
        private readonly Task _read_task;

        private string _direction;

        System.Timers.Timer PositionUpdateTimer;
        System.Timers.Timer SteerCancelTimer;

        public PilotService(GPSSensor _gps,SerialPortService _serial)
        {
            serial = _serial;
            gps = _gps;


            PositionUpdateTimer = new System.Timers.Timer();
            PositionUpdateTimer.Enabled = true;
            if(State.IsSimulating)
            {
                PositionUpdateTimer.Interval = 1000;
            }
            else
            {
                PositionUpdateTimer.Interval = AppSettings.Default.SpeedUpdateInterval * 1000;
            }
            
            PositionUpdateTimer.Elapsed += OnPositionTimer;

            SteerCancelTimer = new System.Timers.Timer();
            SteerCancelTimer.Enabled = true;
            SteerCancelTimer.Interval = AppSettings.Default.PilotCancelTurn;
            SteerCancelTimer.Elapsed += OnCancelTurnTimer;

            _read_task = new Task(SerialTask);
            _read_task.Start();

        }

        /// <summary>
        /// Simulation code ...
        /// </summary>
        /// <param name="command"></param>
        public void OnPilotCommand(PilotCommand command)
        {
            SerialCommand = new Queue<string>();

            if (command.SpeedUp)
            {
                if (Info.PowerRight + AppSettings.Default.PilotSpeedIncrement > AppSettings.Default.PilotMaxPower)
                {
                    Info.PowerRight = AppSettings.Default.PilotMaxPower;
                }
                else
                {
                    Info.PowerRight += AppSettings.Default.PilotSpeedIncrement;
                }
                EnqueueCommand("R", Info.PowerRight);
                if (Info.PowerLeft + AppSettings.Default.PilotSpeedIncrement > AppSettings.Default.PilotMaxPower)
                {
                    Info.PowerLeft = AppSettings.Default.PilotMaxPower;
                }
                else
                {
                    Info.PowerLeft += AppSettings.Default.PilotSpeedIncrement;
                }
                EnqueueCommand("L", Info.PowerLeft);
                if(State.IsSimulating)
                {
                    if ((int)Info.Speed < AppSettings.Default.PilotMaxPower/10)
                    {
                        Info.OnReverse(Info.Speed, Info.Speed + 1, Info.Bearing);
                        Info.Speed += 1;
                    }
                }
            }
            if (command.SpeedDown)
            {
                if (Info.PowerRight - AppSettings.Default.PilotSpeedIncrement < AppSettings.Default.PilotMinPower)
                {
                    Info.PowerRight = AppSettings.Default.PilotMinPower;
                }
                else
                {
                    Info.PowerRight -= AppSettings.Default.PilotSpeedIncrement;
                }
                EnqueueCommand("R", Info.PowerRight);
                if (Info.PowerLeft - AppSettings.Default.PilotSpeedIncrement < AppSettings.Default.PilotMinPower)
                {
                    Info.PowerLeft = AppSettings.Default.PilotMinPower;
                }
                else
                {
                    Info.PowerLeft -= AppSettings.Default.PilotSpeedIncrement;
                }
                EnqueueCommand("L", Info.PowerLeft);

                if (State.IsSimulating)
                {
                    if ((int)Info.Speed > AppSettings.Default.PilotMinPower/10)
                    {
                        Info.OnReverse(Info.Speed, Info.Speed + 1, Info.Bearing);
                        Info.Speed -= 1;
                    }
                }
            }
            if (command.TurnLeft)
            {
                if (Info.PowerLeft - AppSettings.Default.PilotSpeedIncrement < AppSettings.Default.PilotMinPower)
                {
                    Info.PowerLeft = AppSettings.Default.PilotMinPower;
                }
                else
                {
                    Info.PowerLeft -= AppSettings.Default.PilotSpeedIncrement;
                }
                EnqueueCommand("L", Info.PowerLeft);
                if(State.IsSimulating)
                {
                    Info.Bearing = Info.Bearing - (1 * Info.Speed);
                }
            }
            if (command.TurnRight)
            {
                if (Info.PowerRight - AppSettings.Default.PilotSpeedIncrement < AppSettings.Default.PilotMinPower)
                {
                    Info.PowerRight = AppSettings.Default.PilotMinPower;
                }
                else
                {
                    Info.PowerRight -= AppSettings.Default.PilotSpeedIncrement;
                }
                EnqueueCommand("R", Info.PowerRight);
                if (State.IsSimulating)
                {
                    Info.Bearing = Info.Bearing + (1 * Info.Speed);
                }
            }
            if (command.Stop)
            {
                Info.PowerRight = 0;
                EnqueueCommand("R", Info.PowerRight);
                Info.PowerLeft = 0;
                EnqueueCommand("L", Info.PowerLeft);

                if (State.IsSimulating)
                {
                    Info.Speed = 0;
                }
            }
            if (!State.IsSimulating)
            {
                SendCommands();
            }
        }

        private void SendCommands()
        {
            try
            {
                if (serial.ports.ContainsKey("pilotPort"))
                {
                    while (SerialCommand.Count > 0)
                    {
                        serial.ports["pilotPort"].WriteLine(SerialCommand.Dequeue());
                    }
                }
                else
                {
                    State.Alarms.Enqueue("PilotPort not available. Command ignored.");
                }
            }
            catch
            {
                State.Alarms.Enqueue("PilotPort comm error. Command ignored.");
            }
        }
        //Always running
        private async void SerialTask()
        {
            while (!IsDisposed)
            {
                try
                {
                    if (serial.ports.ContainsKey("pilotPort"))
                    {
                        string message = serial.ports["pilotPort"].ReadLine();
                        if(message != "OK")
                        {
                            State.Alarms.Enqueue(message);
                        }
                    }
                    else
                    {
                        serial.ScanDevices();
                        await Task.Delay(500);
                    }
                }
                catch (Exception ex)
                {
                    if (serial.ports.ContainsKey("pilotPort"))
                    {
                        State.Alarms.Enqueue("Pilot dongle Unplugged");
                        serial.Stop(serial.ports["pilotPort"]);
                        serial.ports.Remove("pilotPort");
                    }
                }
            }
            _read_task?.Dispose();
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                //Stop();

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

        private void OnCancelTurnTimer(object? sender, ElapsedEventArgs args)
        {
            if (Info.PowerLeft == Info.PowerRight) return;
            if(Info.PowerLeft > Info.PowerRight)
            {
                Info.PowerRight = Info.PowerLeft;
                EnqueueCommand("R", Info.PowerRight);
            }
            if (Info.PowerRight > Info.PowerLeft)
            {
                Info.PowerLeft = Info.PowerRight;
                EnqueueCommand("L", Info.PowerLeft);
            }
            SendCommands();
        }
        private void EnqueueCommand(string motorId,int speed)
        {
            switch(motorId)
            {
                case "R":
                    Info.RightReverse = speed < 0;
                    break;
                case "L":
                    Info.LeftReverse = speed < 0;
                    break;
            }
            _direction = speed < 0 ? "REVERSE" : "FORWARD";
            speed = speed < 0 ? speed*-1 : speed;
            SerialCommand.Enqueue($"{motorId}|{_direction}|{speed * 4096 / 100}");
        }

        #endregion

        #region Simulation
        double rad;// = Info.Bearing * Math.PI / 180; //to radians
        double lat1;// = Info.CurrentLocation.Lat * Math.PI / 180; //to radians
        double lng1;// = Info.CurrentLocation.Lng * Math.PI / 180; //to radians
        double lat;//= Math.Asin(Math.Sin(lat1) * Math.Cos(distance / 6378137) + Math.Cos(lat1) * Math.Sin(distance / 6378137) * Math.Cos(rad));
        double lng;
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
