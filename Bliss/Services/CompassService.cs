using System.Timers;

namespace Bliss.Services
{
    public class CompassService : IDisposable
    {
        private System.Timers.Timer CompassTimer;

        private SerialPortService serial;
        public bool IsDisposed { get; private set; }
        public CompassService(SerialPortService _serial)
        {
            serial = _serial;
            CompassTimer = new System.Timers.Timer(1000);
            CompassTimer.Elapsed += OnCompassTimer;
            CompassTimer.Enabled = true;

            serial.OnCompassData += ReceiveCompassData;
        }

        private void OnCompassTimer(object? sender, ElapsedEventArgs args)
        {
            if (serial.ports.ContainsKey("compassPort"))
            {
                if (serial.ports["compassPort"].IsOpen)
                {
                    serial.ports["compassPort"].WriteLine("GET");
                }
            }
            else
            {
                serial.ScanDevices();
            }
        }

        private void ReceiveCompassData(object? obj, EventArgs e)
        {

            ReadOnlySpan<char> command = serial.ports["compassPort"].ReadLine().AsSpan();
            switch (command.Slice(0, 7).ToString())
            {
                case "COMPASS":
                    Info.CompassBearing = command.Slice(7).ToString().TrimEnd('\n');
                    break;
                default:
                    //Todo: ignore startup..
                    break;
            }

        }
        public void Dispose()
        {
            if (!IsDisposed)
            {
                if (!serial.IsDisposed) serial.Dispose();
                CompassTimer.Dispose();
                IsDisposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
}
