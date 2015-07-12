using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Bluetooth.AttributeIds;
using InTheHand.Net;
using System.IO;

namespace BluetoothManagerShared
{
    public class BluetoothManager
    {
        public enum Status
        {
            Connected,
            ConnectionError,
            ConnectionLost,
            Idle
        };

        private BluetoothClient client;
        private BluetoothAddress clientAddress;
        private Guid connectionType = BluetoothService.SerialPort;
        private StreamWriter sw;

        public string pin { get; private set; }
        public Status status { get; private set; }
        public bool connected
        {
            get
            {
                return client.Connected;
            }
        }

        public BluetoothManager()
        {
            client = new BluetoothClient();
            status = Status.Idle;
        }

        public Status connect(string address, string pin)
        {
            client.SetPin(pin);

            clientAddress = BluetoothAddress.Parse(address);
            var ep = new BluetoothEndPoint(clientAddress, connectionType);

            try
            {
                client.Connect(ep);
                client.SetPin(pin);
                sw = new StreamWriter(client.GetStream());
                this.pin = pin;
                status = Status.Connected;
            }
            catch(Exception)
            {
                this.pin = String.Empty;
                status = Status.ConnectionError;
            }

            return status;
        }

        public void disconnect()
        {
            client.Close();
            status = Status.Idle;
        }

        public BluetoothDeviceInfo[] discoverDevices()
        {
            return client.DiscoverDevices();
        }

        protected void write(string message)
        {
            sw.WriteLine(message);
            sw.Flush();
        }
    }
}
