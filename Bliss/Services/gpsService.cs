using Bliss.NMEA;
using GMap.NET;
using System.Globalization;
using System.IO.Ports;

namespace Bliss.Services
{
    public sealed class gpsService :IDisposable
    {

        private NMEA0183 gpsdata;
        public bool IsDisposed { get; private set; }

        private SerialPortService serial;

        public gpsService(SerialPortService _serial)
        {
            gpsdata = new NMEA0183();
            serial = _serial;
            if (!serial.ports.ContainsKey("gpsPort"))
            {
                serial.ScanDevices();
            }
            SwitchState();
        }

        public void SwitchState()
        {
            if (!State.IsSimulating)
            {
                //_read_task = new Task(ReadTask);
                //_read_task.Start();
                serial.OnGpsData += ProcessData;
            }
        }

        private void ProcessData(object? obj, EventArgs e)
        {
            try
            {
                string message = serial.ports["gpsPort"].ReadLine();
                var result = gpsdata.ProcessNMEA0183(message);
                if (result != null && result.GetType() == typeof(NMEA0183Data.GNSSData.GPSFixData))
                {
                        if (((NMEA0183Data.GNSSData.GPSFixData)result).QualityIndicator == GPSQualityIndicator.ValidSPSFix)
                        {
                            AverageLocation(new PointLatLng(((NMEA0183Data.GNSSData.GPSFixData)result).Coordinates.Latitude, ((NMEA0183Data.GNSSData.GPSFixData)result).Coordinates.Longitude));// should go here
                        }
                }
            }
            catch(Exception ex)
            {
                State.Alarms.Enqueue($"gps : {ex.Message}");
            }
            
        }

        private List<PointLatLng> recents = new();
        private double Latitude;
        private double Longtitude;

        public void AverageLocation(PointLatLng reading)
        {

            if (recents.Count < AppSettings.Default.GPSAverageCount)//collect more data
            {
                recents.Add(reading);
                return;
            }
            foreach (PointLatLng rec in recents)
            {
                Latitude += rec.Lat;
                Longtitude += rec.Lng;
            }
            Info.SetCurrentLocation(new PointLatLng(Latitude / recents.Count, Longtitude / recents.Count));
            recents.Clear();
            Latitude = 0;
            Longtitude = 0;
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                serial.Dispose();

                IsDisposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }

}
