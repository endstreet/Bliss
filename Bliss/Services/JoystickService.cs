using SharpDX.DirectInput;
using System.Text.RegularExpressions;

namespace Bliss.Services
{
    internal class JoystickService : IEquatable<JoystickService>
    {
        public string Name { get { return _deviceInstance.InstanceName; } }
        public string Guid { get { return _deviceInstance.InstanceGuid.ToString(); } }
        public string UsbId { get => _usbId; }

        public Joystick Joystick { get => _joystick; set => _joystick = value; }
        public DeviceInstance DeviceInstance { get => _deviceInstance; }
        public bool Supported { get; internal set; }

        private DeviceInstance _deviceInstance;
        private Joystick _joystick;
        private string _usbId;

        public JoystickService(DirectInput di, DeviceInstance deviceInstance)
        {
            _deviceInstance = deviceInstance;
            _usbId = ProductGuidToUSBID(_deviceInstance.ProductGuid);
            _joystick = new Joystick(di, deviceInstance.InstanceGuid);

            Joystick.Properties.BufferSize = 32;
        }

        public static string ProductGuidToUSBID(Guid guid)
        {
            return Regex.Replace(guid.ToString(), @"(^....)(....).*$", "$2:$1");
        }
        public void Update()
        {
            try
            {
                Joystick.Poll();
            }
            catch (Exception)
            {
                return;
            }

            foreach (JoystickUpdate joystickUpdate in Joystick.GetBufferedData())
            {

                if ("PointOfViewControllers0|Buttons0|Buttons1|Buttons3".Contains(joystickUpdate.Offset.ToString()))
                {
                    //updatedStates = new List<PilotCommand>();
                    PilotCommand command = new PilotCommand();
                    switch (joystickUpdate.Offset.ToString())
                    {
                        case "PointOfViewControllers0":
                            switch (joystickUpdate.Value)
                            {
                                case 0:
                                    command.SpeedUp = true;
                                    break;
                                case -1:
                                    command.SpeedUp = false;
                                    command.SpeedDown = false;
                                    command.TurnLeft = false;
                                    command.TurnRight = false;
                                    break;
                                case 9000:
                                    command.TurnRight = true;
                                    break;
                                case 18000:
                                    command.SpeedDown = true;
                                    break;
                                case 27000:
                                    command.TurnLeft = true;
                                    break;
                            }
                            break;
                        case "Buttons0":
                            command.Cancel = joystickUpdate.Value > 0;
                            break;
                        case "Buttons1":
                            command.Stop = joystickUpdate.Value > 0;
                            break;
                        case "Buttons3":
                            command.Stop = joystickUpdate.Value > 0;
                            break;

                    }
                    Info.PilotCommands.Enqueue(command);
                }
            }
        }

        public bool Equals(JoystickService other)
        {
            return _deviceInstance.InstanceGuid == other.DeviceInstance.InstanceGuid;
        }

        internal void Unacquire()
        {
            try { Joystick.Unacquire(); } catch (Exception) { }
        }

        internal void Acquire()
        {
            Joystick.Acquire();
        }
    }

}
