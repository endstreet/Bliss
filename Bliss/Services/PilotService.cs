using System.IO.Ports;
using System.Timers;

namespace Bliss.Services
{
    public sealed class PilotService : IDisposable
    {
        public bool IsDisposed { get; private set; }
        public bool IsInUse { get; private set; }

        private readonly object? _mutex;
        private SerialPort? _port;

        private SerialPortService? serial;

        public GPSSensor gps;

        public PilotService()
        {
            gps = new();

            //_mutex = new();
            //_read_task = new Task(ReadTask);
            //_read_task.Start();

            m_Timer = new System.Timers.Timer();
            m_Timer.Enabled = true;
            m_Timer.Interval = AppSettings.Default.SpeedUpdateInterval * 1000;
            m_Timer.Elapsed += OnPositionTimer;


            //serial = new SerialPortService();
            //serial.OnPilotComm += Serial_OnPilotComm;
            //serial.ScanDevices();
        }
        /// <summary>
        /// Simulation code ...
        /// </summary>
        /// <param name="command"></param>
        public void OnPilotCommand(PilotCommand command)
        {
            if (command.SpeedUp)
            {
                if (Info.PowerRight + AppSettings.Default.PilotSpeedIncrement > AppSettings.Default.PilotMaxPower)
                {
                    Info.PowerRight = AppSettings.Default.PilotMaxPower;
                    //Send right max
                }
                else
                {
                    Info.PowerRight += AppSettings.Default.PilotSpeedIncrement;
                    //Send right
                }
                if (Info.PowerLeft + AppSettings.Default.PilotSpeedIncrement > AppSettings.Default.PilotMaxPower)
                {
                    Info.PowerLeft = AppSettings.Default.PilotMaxPower;
                    //Send left max
                }
                else
                {
                    Info.PowerLeft += AppSettings.Default.PilotSpeedIncrement;
                    //Send left 
                }
            }
            if (command.SpeedDown)
            {
                if (Info.PowerRight < AppSettings.Default.PilotSpeedIncrement)
                {
                    Info.PowerRight = 0;
                    //Send right stop
                }
                else
                {
                    Info.PowerRight -= AppSettings.Default.PilotSpeedIncrement;
                    //Send right
                }
                if (Info.PowerLeft < AppSettings.Default.PilotSpeedIncrement)
                {
                    Info.PowerLeft = 0;
                    //Send left stop
                }
                else
                {
                    Info.PowerLeft -= AppSettings.Default.PilotSpeedIncrement;
                    //Send left - 
                }
            }
            if (command.TurnLeft)
            {
                if (Info.PowerLeft < AppSettings.Default.PilotSpeedIncrement)
                {
                    Info.PowerLeft = 0;
                    //Send stop
                }
                else
                {
                    Info.PowerLeft -= AppSettings.Default.PilotSpeedIncrement;
                    //Send left - 
                }

            }
            if (command.TurnRight)
            {
                if (Info.PowerRight < AppSettings.Default.PilotSpeedIncrement)
                {
                    Info.PowerRight = 0;
                    //Send stop
                }
                else
                {
                    Info.PowerRight -= AppSettings.Default.PilotSpeedIncrement;
                    //Send right
                }
            }
            if (command.Stop)
            {
                Info.PowerLeft = 0;
                //Send stop
                Info.PowerRight = 0;
                //Send stop
            }

        }

        //private void Serial_OnPilotComm(string port)
        //{
        //    m_Timer.Stop();
        //    serial.OnPilotComm -= Serial_OnPilotComm;
        //    Stop();
        //    Start(port);
        //    //InitialPosition
        //    Shared.LastLocation = Shared.CurrentLocation;
        //    Shared.CurrentLocation = gps.CurrentLocation;

        //    m_Timer.Start();
        //}

        //Always running
        //private async void ReadTask()
        //{
        //    while (!IsDisposed)
        //        try
        //        {
        //            if (IsInUse && _port is { IsOpen: true } p) //Normal
        //            {
        //                string message = p.ReadLine();
        //                //if valid pilot comman
        //                if (!string.IsNullOrEmpty(message))
        //                {
        //                    //process message then..
        //                    _port.Write("Ok\r\n");
        //                }
        //            }
        //            else
        //            {
        //                await Task.Delay(300);
        //                serial.OnPilotComm += Serial_OnPilotComm;
        //                serial.ScanDevices();
        //            }
        //        }
        //        catch (Exception ex)//Data error
        //        {
        //            IsValid = false;
        //            //OnDataError?.Invoke(this, ex.Message);
        //        }

        //    _read_task?.Dispose();
        //}

        private void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //OnError?.Invoke(this, e.EventType);
            Stop();
            serial.ScanDevices();
        }

        public bool Start(string port)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(PilotService));
            else if (IsInUse)
                return false;

            lock (_mutex)
            {
                _port = new SerialPort(port, 9600)
                {
                    DataBits = 8,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,
                    NewLine = "\r\n",
                };
                _port.ErrorReceived += Port_ErrorReceived;

                _port.Open();
                IsInUse = true;
                //OnStart?.Invoke(this);
                _port.Write("Ok\r\n");//Kickstart the arduino
            }

            return IsInUse;
        }
        public void Stop()
        {
            IsInUse = false;

            lock (_mutex)
                if (_port is { })
                {
                    _port.ErrorReceived -= Port_ErrorReceived;
                    _port.Close();
                    _port.Dispose();
                    _port = null;
                    //OnStop?.Invoke(this);
                }
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                Stop();

                IsDisposed = true;
            }

            GC.SuppressFinalize(this);
        }

        #region speed and bearing
        private readonly System.Timers.Timer m_Timer;

        private void OnPositionTimer(object? sender, ElapsedEventArgs args)
        {
            //Update the speed
            Info.CalculateSpeed(m_Timer.Interval);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>

        //public double GetBearingX(PointLatLng fromPoint, PointLatLng toPoint)
        //{
        //    double x = Math.Cos(DegreesToRadians(fromPoint.Lat)) * Math.Sin(DegreesToRadians(toPoint.Lat)) - Math.Sin(DegreesToRadians(fromPoint.Lat)) * Math.Cos(DegreesToRadians(toPoint.Lat)) * Math.Cos(DegreesToRadians(toPoint.Lng - fromPoint.Lng));
        //    double y = Math.Sin(DegreesToRadians(toPoint.Lng - fromPoint.Lng)) * Math.Cos(DegreesToRadians(toPoint.Lat));

        //    // Math.Atan2 can return negative value, 0 <= output value < 2*PI expected 
        //    return Math.Round((Math.Atan2(y, x) + Math.PI * 2) % (Math.PI * 2), 2);

        //}

        //private double DegreesToRadians(double angle)
        //{
        //    return angle * Math.PI / 180.0d;
        //}


        #endregion
    }
}
