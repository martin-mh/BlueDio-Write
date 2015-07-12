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

namespace GraphicalInterface
{
    /// <summary>
    /// Interaction logic for ArduinoDigitalPinSetter.xaml
    /// </summary>
    public partial class ArduinoDigitalPinSetter : UserControl
    {
        public event RoutedEventHandler OnClick;
        public event RoutedEventHandler OffClick;
        
        public ArduinoDigitalPinSetter()
        {
            InitializeComponent();
        }

        private void onOnClick(object sender, RoutedEventArgs e)
        {
            if (this.OnClick != null)
                this.OnClick(this, e);
        }

        private void onOffClick(object sender, RoutedEventArgs e)
        {
            if (this.OffClick != null)
                this.OffClick(this, e);
        }
    }
}
