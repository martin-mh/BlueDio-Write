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

using InTheHand.Net.Bluetooth;
using BluetoothManagerShared;

namespace GraphicalInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ArduinoManager arduino;

        public MainWindow()
        {
            InitializeComponent();
            arduino = new ArduinoManager();
        }

        private void onOnClick(object sender, RoutedEventArgs e)
        {
            ArduinoDigitalPinSetter digitalPin = sender as ArduinoDigitalPinSetter;
            ArduinoManager.TargetCard card = digitalPin.card.Text == "Master" ? ArduinoManager.TargetCard.Master : ArduinoManager.TargetCard.Slave;
            arduino.setDigitalOutput(card, Convert.ToInt32(digitalPin.digitalPin.Text), ArduinoManager.DigitalOutputStatus.High);
        }

        private void onOffClick(object sender, RoutedEventArgs e)
        {
            ArduinoDigitalPinSetter digitalPin = sender as ArduinoDigitalPinSetter;
            ArduinoManager.TargetCard card = digitalPin.card.Text == "Master" ? ArduinoManager.TargetCard.Master : ArduinoManager.TargetCard.Slave;
            arduino.setDigitalOutput(card, Convert.ToInt32(digitalPin.digitalPin.Text), ArduinoManager.DigitalOutputStatus.Low);
        }

        private void addPinControl(object sender, RoutedEventArgs e)
        {
            ArduinoDigitalPinSetter digitalPin = new ArduinoDigitalPinSetter();
            digitalPin.OnClick += this.onOnClick;
            digitalPin.OffClick += this.onOffClick;
            Grid.Children.Add(digitalPin);
        }

        private void viewAvailableDevices(object sender, RoutedEventArgs e)
        {
            DiscoverDevices dd = new DiscoverDevices(arduino.discoverDevices());
            dd.connectionAsked += connectionAsked;
        }

        private void connectionAsked(object sender, RoutedEventArgs e)
        {
            Device device = sender as Device;

            InputPopup pin = new InputPopup();

            if(pin.ShowDialog() == true)
            {
                if (arduino.connect(device.deviceInfo.DeviceAddress.ToString(), pin.ResponseText) == ArduinoManager.Status.Connected)
                {
                    connect();
                }
                else
                {
                    disconnect();
                }
            }
        }

        private void connect()
        {
            StatusIndicator.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            StatusIndicator.ToolTip = "Connected.";
        }

        private void disconnect()
        {
            StatusIndicator.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            StatusIndicator.ToolTip = "Not connected.";
        }

        private void updateGrid()
        {
            Grid.Width = Window.ActualWidth;
            Grid.Height = Window.ActualHeight - (double)45;
            Grid.RowCount = (int)(Grid.Height / Grid.RowHeight.Value);
            Grid.ColumnCount = (int)(Grid.Width / Grid.ColumnWidth.Value);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            updateGrid();
        }
    }
}
