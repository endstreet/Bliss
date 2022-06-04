using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bliss.Services
{
    internal class StaticData
    {
        private static List<string>? _deviceTypes;
        public static List<string> DeviceTypes
        {
            get 
            {
                if (_deviceTypes != null) return _deviceTypes;

                _deviceTypes = new List<string>();
                _deviceTypes.Add("GPS NMEA1083");
                _deviceTypes.Add("AutopilotGV");
                _deviceTypes.Add("Compass NMEA1083");

                return _deviceTypes;
            }
        }
    }
}
