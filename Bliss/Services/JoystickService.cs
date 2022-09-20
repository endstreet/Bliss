//using SharpDX.DirectInput;
using SharpDX.DirectInput;
using SharpDX.XInput;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Timers;
using DeviceType = SharpDX.DirectInput.DeviceType;
//using System.Diagnostics;
//using System.Text.RegularExpressions;

namespace Bliss.Services
{
    public class JoystickService :IDisposable
    {
        private System.Timers.Timer JoystickInputTimer;
        private XInputController XJoystick;
        
        public event EventHandler? OnJoystickData;
        public JoystickService()
        {
            XJoystick = new XInputController();
            JoystickInputTimer = new System.Timers.Timer();
            if (XJoystick.IsConnected || XJoystick.IsThrustMasterConnected)
            {
                JoystickInputTimer.Interval = 200;
                JoystickInputTimer.Elapsed += JoystickInputTimer_Elapsed;
                JoystickInputTimer.Enabled = true;
            }
        }

        private void JoystickInputTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (XJoystick.IsThrustMasterConnected)
            {
                if (!XJoystick.IsProcessing)
                {
                    XJoystick.TMUpdate();
                    if (XJoystick.CommandReady)
                    {
                        //XJoystick.CommandReady = false;
                        OnJoystickData?.Invoke(null, EventArgs.Empty);
                    }
                }
            }
            else
            {
                if (!XJoystick.IsProcessing)
                {
                    XJoystick.Update();
                    if (XJoystick.CommandReady)
                    {
                        //XJoystick.CommandReady = false;
                        OnJoystickData?.Invoke(null, EventArgs.Empty);
                    }
                }
            }
        }

        public void Dispose()
        {
            JoystickInputTimer.Dispose();
            XJoystick.Dispose();
        }
    }
    
    internal class XInputController : IDisposable
    {
        private Dictionary<string, string> Commands;
        Controller controller;
        Gamepad gamepad;
        public bool IsConnected = false;
        //TM
        public bool IsThrustMasterConnected = false;
        Joystick tmjoystick;
        //----
        public bool IsProcessing = false;
        //private Point leftThumb, rightThumb = new Point(0, 0);
        //private float leftTrigger, rightTrigger;
        public bool CommandReady = false;

        public XInputController()
        {
            controller = new Controller(UserIndex.One);//One
            IsConnected = controller.IsConnected;
            if(!IsConnected)
            {
                ThrustMaster();
                return;
            }
            Commands = new Dictionary<string, string>()
            {
                { "DPadUp", "Forward" },
                { "DPadDown", "Backward" },
                { "DPadLeft", "Left" },
                { "DPadRight", "Right" },
                { "DPadUpDPadLeft", "ForwardLeft" },
                { "DPadDownDPadLeft", "BackwardLeft" },
                { "DPadUpDPadRight", "ForwardRight"  },
                { "DPadDownDPadRight", "BackwardRight" },
                { "DPadLeftDPadUp", "ForwardLeft" },
                { "DPadLeftDPadDown", "BackwardLeft" },
                { "DPadRightDPadUp", "ForwardRight" },
                { "DPadRightDPadDown", "BackwardRight" },
                { "Start", "Stop" },
                { "Back", "Cancel" }
            };
        }

        /// <summary>
        /// Code to try Thrustmaster when gamepad not available
        /// </summary>
        private void ThrustMaster()
        {
            // Initialize DirectInput
            var directInput = new DirectInput();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                        DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {
                Console.WriteLine("No joystick/Gamepad found.");
                Console.ReadKey();
                Environment.Exit(1);
            }
            else
            {
                IsThrustMasterConnected = true;
            }

            // Instantiate the joystick
            tmjoystick = new Joystick(directInput, joystickGuid);

            Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

            // Query all suported ForceFeedback effects
            //var allEffects = joystick.GetEffects();
            //foreach (var effectInfo in allEffects)
            //    Console.WriteLine("Effect available {0}", effectInfo.Name);

            // Set BufferSize in order to use buffered data.
            tmjoystick.Properties.BufferSize = 128;

            // Acquire the joystick
            tmjoystick.Acquire();

            // Poll events from joystick
            //while (true)
            //{
            //    joystick.Poll();
            //    var datas = joystick.GetBufferedData();
            //    foreach (var state in datas)
            //        Console.WriteLine(state);
            //}
        }
        // Call this method to update all class values
        //Todo: Automate by allowing 
        public void Update()
        {
            if (!IsConnected) return;
            if (IsProcessing) return;
            IsProcessing = true;
            CommandReady = false;
            gamepad = controller.GetState().Gamepad;

            var commands = gamepad.Buttons.ToString().Split(',').Where(c => c != "None");
            if (!commands.Any())
            {
                IsProcessing = false;
                return;
            }
            string command;
            if (commands.Count() == 2)
            {
                command = string.Join("", commands);
            }
            else
            {
                command = commands.First();
            }
            if (!Info.JoystickCommands.Contains(command.Replace(" ", "")))
            {
                State.Notices.Enqueue($"<JOYST01|{Commands[command.Replace(" ", "")]}>");
                Info.JoystickCommands.Enqueue(Commands[command.Replace(" ", "")]);
                CommandReady = true;
            }
            IsProcessing = false;

        }

        public void TMUpdate()
        {
            try
            {
                tmjoystick.Poll();
            }
            catch (Exception)
            {
                return;
            }
            //var xx = tmjoystick.GetBufferedData();
            foreach (JoystickUpdate joystickUpdate in tmjoystick.GetBufferedData())
            {

                if ("X|Y|PointOfViewControllers0|Buttons0|Buttons1|Buttons3".Contains(joystickUpdate.Offset.ToString()))
                {
                    //updatedStates = new List<PilotCommand>();
                    string command = "";
                    switch (joystickUpdate.Offset.ToString())
                    {
                        case "X":
                            Debug.Print("X: " + joystickUpdate.Value.ToString());
                            break;
                        case "Y":
                            Debug.Print("Y: " + joystickUpdate.Value.ToString());
                            break;
                        case "PointOfViewControllers0":
                            switch (joystickUpdate.Value)
                            {
                                case 0:
                                    command = "Forward";
                                    break;
                                case -1:
                                    //command.SpeedUp = false;
                                    //command.SpeedDown = false;
                                    //command.Left = false;
                                    //command.Right = false;
                                    return;
                                case 9000:
                                    command = "Right";
                                    break;
                                case 18000:
                                    command = "Backward";
                                    break;
                                case 27000:
                                    command = "Left";
                                    break;
                            }
                            State.Notices.Enqueue($"<JOYST01|{command}>");
                            Info.JoystickCommands.Enqueue(command);
                            CommandReady = true;
                            break;
                        case "Buttons0":
                            command = joystickUpdate.Value > 0 ? "Cancel" : command = "";
                            break;
                        case "Buttons1":
                            command = joystickUpdate.Value > 0 ? "Stop" : command = "";
                            break;
                        case "Buttons3":
                            command = joystickUpdate.Value > 0 ? "Stop" : command = "";
                            break;

                    }
                    if (!string.IsNullOrEmpty(command))
                    {
                        //Info.PilotCommands.Enqueue(command);
                    }
                }
            }
        }

        public void Dispose()
        {
            //Commands.Clear();
        }
    }


    //private void ScanJoysticks()
    //{

    //    DirectInput _directInput = new DirectInput();
    //    var devices = _directInput.GetDevices().Where(d => d.ProductName.ToUpper().Contains("JOYSTICK") || d.ProductName == "USB Game Controllers");
    //    if (devices.Any())
    //    {
    //        DeviceInstance firstJoystickInstance = _directInput.GetDevices().First();
    //        //DeviceInstance firstJoystickInstance = _directInput.GetDevices().Where(d => d.ProductName == "USB Game Controllers").First();
    //        Joystick = new USBInputController(_directInput, firstJoystickInstance);
    //        Joystick.Acquire();
    //        Joystick.IsConnected = true;
    //        JoystickInputTimer.Enabled = true;
    //        //joystickActive.BackColor = ColorScheme.Active;
    //    }
    //    else
    //    {
    //        XJoystick = new XInputController();
    //        if (XJoystick.IsConnected)
    //        {
    //            JoystickInputTimer.Enabled = true;
    //            //joystickActive.BackColor = ColorScheme.Active;
    //        }
    //    }
    //}

    //internal class USBInputController : IEquatable<USBInputController>
    //{
    //    public bool IsConnected { get; set; }
    //    public string? Name { get { return _deviceInstance?.InstanceName; } }
    //    public string? Guid { get { return _deviceInstance?.InstanceGuid.ToString(); } }
    //    public string? UsbId { get => _usbId; }
    //    public Joystick? Joystick { get => _joystick; set => _joystick = value; }
    //    public DeviceInstance? DeviceInstance { get => _deviceInstance; }
    //    public bool Supported { get; internal set; }
    //    private DeviceInstance? _deviceInstance;
    //    private Joystick? _joystick;
    //    private string? _usbId;
    //    private System.Timers.Timer JoystickRead;

    //    public USBInputController(DirectInput di, DeviceInstance deviceInstance)
    //    {
    //        _deviceInstance = deviceInstance;
    //        _usbId = ProductGuidToUSBID(_deviceInstance.ProductGuid);
    //        _joystick = new Joystick(di, deviceInstance.InstanceGuid);

    //        Joystick.Properties.BufferSize = 32;
    //    }


    //    public static string ProductGuidToUSBID(Guid guid)
    //    {
    //        return Regex.Replace(guid.ToString(), @"(^....)(....).*$", "$2:$1");
    //    }
    //    public void Update()
    //    {
    //        try
    //        {
    //            Joystick.Poll();
    //        }
    //        catch (Exception)
    //        {
    //            return;
    //        }
    //        var xx = Joystick.GetBufferedData();
    //        foreach (JoystickUpdate joystickUpdate in Joystick.GetBufferedData())
    //        {

    //            if ("X|Y|PointOfViewControllers0|Buttons0|Buttons1|Buttons3".Contains(joystickUpdate.Offset.ToString()))
    //            {
    //                //updatedStates = new List<PilotCommand>();
    //                string command = "";
    //                switch (joystickUpdate.Offset.ToString())
    //                {
    //                    case "X":
    //                        Debug.Print("X: " + joystickUpdate.Value.ToString());
    //                        break;
    //                    case "Y":
    //                        Debug.Print("Y: " + joystickUpdate.Value.ToString());
    //                        break;
    //                    case "PointOfViewControllers0":
    //                        switch (joystickUpdate.Value)
    //                        {
    //                            case 0:
    //                                command = "Forward";
    //                                break;
    //                            case -1:
    //                                //command.SpeedUp = false;
    //                                //command.SpeedDown = false;
    //                                //command.Left = false;
    //                                //command.Right = false;
    //                                break;
    //                            case 9000:
    //                                command = "Right";
    //                                break;
    //                            case 18000:
    //                                command = "Backward";
    //                                break;
    //                            case 27000:
    //                                command = "Left";
    //                                break;
    //                        }
    //                        break;
    //                    case "Buttons0":
    //                        command = joystickUpdate.Value > 0 ? "Cancel" : command = "";
    //                        break;
    //                    case "Buttons1":
    //                        command = joystickUpdate.Value > 0 ? "Stop" : command = "";
    //                        break;
    //                    case "Buttons3":
    //                        command = joystickUpdate.Value > 0 ? "Stop" : command = "";
    //                        break;

    //                }
    //                if (!string.IsNullOrEmpty(command))
    //                {
    //                    //Info.PilotCommands.Enqueue(command);
    //                }
    //            }
    //        }
    //    }

    //    public bool Equals(JoystickService other)
    //    {
    //        return true;// _deviceInstance.InstanceGuid == other.DeviceInstance.InstanceGuid;
    //    }

    //    internal void Unacquire()
    //    {
    //        try { Joystick.Unacquire(); } catch (Exception) { }
    //    }

    //    internal void Acquire()
    //    {
    //        Joystick.Acquire();
    //    }

    //    public bool Equals(USBInputController? other)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}


