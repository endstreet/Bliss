using Bliss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bliss.Services
{
    public class ConfigurationService
    {
        public List<ConfigurationSet> configs;
        public ConfigurationService()
        {
            configs = new List<ConfigurationSet>();
            ConfigurationSet configSet = new ConfigurationSet("TestConfig",1, "FollowTheSun","MACADDRESS","BLE to Wireless Gateway");
            Device device = new Device(1, 1, "1234567890", "MOTOR01", "Main Left motor", "20 KW Bldc watercooled motor");
            device.Properties.Add(new Function(1, 1, "Reset", FunctionType.Reset, "Initialize motor controller."));
            device.Properties.Add(new Function(2, 1, "Set", FunctionType.SetValue, "Set motor Speed|Direction."));
            device.Properties.Add(new Function(3, 1, "Get", FunctionType.GetValue, "Get Speed|Direction."));
            configSet.Gateway.Devices.Add(device);
            device = new Device(2, 1, "1234567891", "MOTOR02", "Main Right motor", "20 KW Bldc watercooled motor");
            device.Properties.Add(new Function(1, 2, "Reset", FunctionType.Reset, "Initialize motor controller."));
            device.Properties.Add(new Function(2, 2, "Set", FunctionType.SetValue, "Set motor Speed|Direction."));
            device.Properties.Add(new Function(3, 2, "Get", FunctionType.GetValue, "Get Speed|Direction."));
            configSet.Gateway.Devices.Add(device);
            device = new Device(3, 1, "1234567892", "COMPA01", "Compass", "Electronic Compass");
            device.Properties.Add(new Function(1, 3, "GetBearing", FunctionType.GetValue, "Get compass bearing."));
            configSet.Gateway.Devices.Add(device);
            device = new Device(4, 1, "1234567893", "GPSSE01", "Gps", "Gps sensor");
            device.Properties.Add(new Function(1, 4, "GetLocation", FunctionType.GetValue, "Get Lng|Lat."));
            configSet.Gateway.Devices.Add(device);
            device = new Device(5, 1, "1234567894", "JOYST01", "Joystick", "Joystick control");
            device.Properties.Add(new Function(1, 5, "Auto", FunctionType.AutoUpdate, "Joystick Command."));
            configSet.Gateway.Devices.Add(device);

            configs.Add(configSet);
        }

    }
}
