// See https://aka.ms/new-console-template for more information
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

Console.WriteLine("Hello, World!");

BTS.Test();

static class BTS
{

    public static void Test()
    {
        BluetoothClient client = new BluetoothClient();

        BluetoothDeviceInfo device = null;
        foreach (var dev in client.DiscoverDevices())
        {
            if (dev.DeviceName.Contains("FollowTheSun"))
            {
                device = dev;
                break;
            }
        }

        if (!device.Authenticated)
        {
            BluetoothSecurity.PairRequest(device.DeviceAddress, "1234");
        }

        device.Refresh();
        System.Diagnostics.Debug.WriteLine(device.Authenticated);

        client.Connect(device.DeviceAddress, BluetoothService.SerialPort);

        var stream = client.GetStream();
        StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.ASCII);
        sw.WriteLine("Hello world!\r\n\r\n");
        sw.Close();

        client.Close();
    }
}