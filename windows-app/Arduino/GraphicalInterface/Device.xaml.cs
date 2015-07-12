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
using System.Windows.Navigation;
using System.Windows.Shapes;

using InTheHand.Net.Sockets;

namespace GraphicalInterface
{
    /// <summary>
    /// Interaction logic for Device.xaml
    /// </summary>
    public partial class Device : UserControl
    {
        public event RoutedEventHandler connect;
        public event RoutedEventHandler moreInfos;
        public BluetoothDeviceInfo deviceInfo;

        public Device(BluetoothDeviceInfo infos)
        {
            InitializeComponent();

            deviceInfo = infos;
            SignalStrength.Content = "Signal strength : " + infos.Rssi.ToString();
            Name.Content = infos.DeviceName;
        }

        public void setName(string name)
        {
            Name.Content = name;
        }

        public void setSignalStrength(int strength)
        {
            SignalStrength.Content = "Signal strength : " + strength.ToString();
        }

        private void moreInfosClicked(object sender, RoutedEventArgs e)
        {
            deviceInfo.ShowDialog();
        }

        private void connectClicked(object sender, RoutedEventArgs e)
        {
            if (this.connect != null)
                this.connect(this, e);
        }
    }
}
