using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace GraphicalInterface
{
    /// <summary>
    /// Interaction logic for DiscoverDevices.xaml
    /// </summary>
    public partial class DiscoverDevices : Window
    {
        public event RoutedEventHandler connectionAsked;

        public DiscoverDevices(BluetoothDeviceInfo[] BluetoothDevices)
        {
            InitializeComponent();

            foreach(BluetoothDeviceInfo deviceInfo in BluetoothDevices)
            {
                Device device = new Device(deviceInfo);
                device.connect += connectionAskedEvent;
                devicesList.Items.Add(device);
            }

            this.Show();
        }

        public void connectionAskedEvent(object sender, RoutedEventArgs e)
        {
            if (connectionAsked != null)
                connectionAsked(sender, e);
        }
    }
}
