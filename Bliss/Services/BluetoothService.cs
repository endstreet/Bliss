using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Timers;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;

namespace Bliss.Services
{
    public class BleService : IDisposable
    {

        public event EventHandler<string>? OnBleData;
        public event EventHandler<string>? OnBleConnection;

        private GattCharacteristic? command;
        private DeviceInformation? device = null;

        private const string BOATSERVICE_ID = "0001"; //1800 1801 0001
        private const string BOAT_NAME = "FollowTheSun";//The Bluetooth device
        //Reconnect Bluetooth if it is disconnected.
        private readonly System.Timers.Timer Keepalive = new System.Timers.Timer(10000);// Keepalive

        public BleService()
        {
            Keepalive.Elapsed += Keepalive_Elapsed;
            Keepalive.Enabled = true;
        }

        private async void Keepalive_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (device == null)
            {
                await ScanBle();
            }
        }

        private async Task ScanBle()
        {
            // Query for extra properties you want returned
            string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };

            DeviceWatcher deviceWatcher = DeviceInformation.CreateWatcher(BluetoothLEDevice.GetDeviceSelectorFromPairingState(true), requestedProperties, DeviceInformationKind.AssociationEndpoint);

            // Register event handlers before starting the watcher. 
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.Removed += DeviceWatcher_Removed;

            // EnumerationCompleted and Stopped are optional to implement.
            deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
            deviceWatcher.Stopped += DeviceWatcher_Stopped;

            // Start the windoews device watcher.
            deviceWatcher.Start();
            while (true)
            {
                if (device == null)
                {
                    Thread.Sleep(200);
                    OnBleConnection?.Invoke(this, "DisConnected");
                }
                else
                {
                    //Try to connect BT device
                    BluetoothLEDevice bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(device.Id);
                    //Attempting to pair with device
                    if (bluetoothLeDevice != null)
                    {
                        GattDeviceServicesResult result = await bluetoothLeDevice.GetGattServicesAsync();

                        if (result.Status == GattCommunicationStatus.Success)
                        {
                            //Pairing succeeded
                            var services = result.Services;
                            foreach (var service in services)
                            {
                                if (service.Uuid.ToString("N").Substring(4, 4) == BOATSERVICE_ID)
                                {
                                    GattCharacteristicsResult charactiristicResult = await service.GetCharacteristicsAsync();//GetCharacteristicsForUuidAsync(new Guid("beb5483e-36e1-4688-b7f5-ea07361b26a8"));//

                                    if (charactiristicResult.Status == GattCommunicationStatus.Success)
                                    {
                                        var characteristics = charactiristicResult.Characteristics;
                                        foreach (var characteristic in characteristics)
                                        {
                                            GattCharacteristicProperties properties = characteristic.CharacteristicProperties;

                                            if (properties.HasFlag(GattCharacteristicProperties.Notify))
                                            {
                                                ////Console.WriteLine("Notify poroperty found");
                                                GattCommunicationStatus status = await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                                                GattClientCharacteristicConfigurationDescriptorValue.Notify);
                                                if (status == GattCommunicationStatus.Success)
                                                {
                                                    characteristic.ValueChanged += Characteristic_ValueChanged;
                                                    // Server has been informed of clients interest.
                                                }
                                            }
                                            if (properties.HasFlag(GattCharacteristicProperties.Write))
                                            {
                                                command = characteristic;
                                            }
                                        }
                                    }
                                }
                            }
                            OnBleConnection?.Invoke(this, "Connected");
                        }
                    }
                    break;  //Stop DeviceWatcher when Connected
                }
            }
            deviceWatcher.Stop();
        }

        internal async Task SendBTCommand(string toSend)
        {
            IBuffer writer = Encoding.ASCII.GetBytes(toSend).AsBuffer();
            try
            {
                // BT_Code: Writes the value from the buffer to the characteristic.         
                var result = await command.WriteValueAsync(writer);
                if (result != GattCommunicationStatus.Success)
                {
                    State.Alarms.Enqueue("Error SendBTCommand");
                }

            }
            catch (Exception ex) when ((uint)ex.HResult == 0x80650003 || (uint)ex.HResult == 0x80070005)
            {
                State.Alarms.Enqueue("Error SendBTCommand exception");
            }
        }

        private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            var reader = DataReader.FromBuffer(args.CharacteristicValue);
            var notice = reader.ReadString(args.CharacteristicValue.Length);
            //State.Notices.Enqueue(notice);
            OnBleData?.Invoke(this, notice);
        }

        private void DeviceWatcher_Stopped(DeviceWatcher sender, object args)
        {
            OnBleConnection?.Invoke(this, "DisConnected");
            //throw new NotImplementedException();
        }

        private void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {
            //throw new NotImplementedException();
        }

        private void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {

            device = null;

        }

        private void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            //throw new NotImplementedException();
        }

        private void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            if (args.Name == BOAT_NAME)
            {
                device = args;
            }
        }

        public void Dispose()
        {
            Keepalive.Stop();
            Keepalive.Dispose();
        }
    }
}
