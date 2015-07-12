using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothManagerShared
{
    public class ArduinoManager : BluetoothManager
    {
        public enum TargetCard
        {
            Master,
            Slave
        };

        public enum DigitalOutputStatus
        {
            Low,
            High
        };

        // Summary:
        //     Send to the Arduino the following message : card:digitalPin:status. Replace keywords by their value.
        public bool setDigitalOutput(TargetCard card, int digitalPin, DigitalOutputStatus status)
        {
            if (!connected)
                return false;

            const string colon = ":";

            string message = card == TargetCard.Master ? "master" : "slave";
            message += colon;
            message += digitalPin.ToString();
            message += colon;
            message += status == DigitalOutputStatus.Low ? "0" : "1";

            write(message);

            return true;
        }
    }
}
