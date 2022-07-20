using System.IO.Ports;
using System.Runtime.InteropServices;



namespace Bliss.Services
{
    public class SerialPortService:IDisposable
    {
        public Dictionary<string, string> attachedports;
        public Dictionary<string, SerialPort> ports;

        private DeviceService device;
        public bool IsDisposed { get; private set; }

        public event EventHandler? OnGpsData;
        public event EventHandler? OnPilotData;
        public event EventHandler? OnCompassData;

        public SerialPortService(DeviceService _device)
        {
            device = _device;
            attachedports = new Dictionary<string, string>();
            ports = new Dictionary<string, SerialPort>();
        }

        public void ScanDevices()
        {
            if (State.IsSimulating)
            {
                return;
            }
            device.GetAttachedPorts();
            attachedports = device.ports;
            //Connect the Ports
            foreach (var port in attachedports)
            {
                switch(port.Key)
                {
                    case "gpsPort":
                        if (!ports.ContainsKey("gpsPort"))
                        {
                            Start(port.Value, port.Key, 4800);
                        }
                        break;
                    case "pilotPort":
                        if (!ports.ContainsKey("pilotPort"))
                        {
                            Start(port.Value, port.Key, 115200);
                        }
                        break;
                    case "pilotPortO":
                        if (!ports.ContainsKey("pilotPortO"))
                        {
                            Start(port.Value, port.Key, 115200);
                        }
                        break;
                    case "compassPort":
                        if (!ports.ContainsKey("compassPort"))
                        {
                            Start(port.Value, port.Key, 115200);
                        }
                        break;
                }
            }
        }
        
        //public bool IsInUse;
        private readonly object _mutex = new();

        public void Start(string port,string PortName,int speed)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(PortName);
            }

            try
            {
                lock (_mutex)
                {
                    SerialPort _port = new SerialPort(port, speed)
                    {
                        DataBits = 8,
                        Parity = Parity.None,
                        StopBits = StopBits.One,
                        Handshake = Handshake.None,
                        NewLine = "\r\n",
                        ReadTimeout = 5000
                    };

                    _port.Open();
                    ports.Add(PortName,_port);
                    _port.ErrorReceived += Port_ErrorReceived;
                    // Attach a method to be called when there
                    // is data waiting in the port's buffer
                    if (PortName == "gpsPort")
                    {
                        _port.DataReceived += gpsPortDataReceived;
                        //OnGpsData?.Invoke(null, EventArgs.Empty);
                    }
                    if (PortName == "pilotPort")
                    {
                        _port.DataReceived += pilotPortDataReceived;
                        //OnPilotData?.Invoke(null, EventArgs.Empty);
                    }
                    if (PortName == "compassPort")
                    {
                        _port.DataReceived += compassPortDataReceived;
                        //OnPilotData?.Invoke(null, EventArgs.Empty);
                    }
                }

            }
            catch(Exception ex)
            {
                State.Alarms.Enqueue($"Error starting {PortName} |");;
                ports.Remove(PortName);
                Task.Delay(1000);
            }
        }

        private void gpsPortDataReceived(object sender,SerialDataReceivedEventArgs e)
        {

                OnGpsData?.Invoke(null,EventArgs.Empty);
        }

        private void pilotPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            OnPilotData?.Invoke(null, EventArgs.Empty);
        }

        private void compassPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //OnCompassData?.Invoke(null, EventArgs.Empty);
        }

        public void Stop(SerialPort? _port)
        {
            lock (_mutex)
            {
                if (_port is not null)
                {
                    _port.ErrorReceived -= Port_ErrorReceived;
                    _port.Close();
                    _port.Dispose();
                    _port = null;
                    Thread.Sleep(5000);
                }
            }
        }
        private void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            State.Alarms.Enqueue($"{((SerialPort)sender).PortName} error port was Stopped ! |");
            ports.Remove(((SerialPort)sender).PortName);
            Stop((SerialPort)sender);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    foreach (SerialPort port in ports.Values)
                    {
                        Stop(port);
                    }
                    ports.Clear();
                }
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                IsDisposed = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}


