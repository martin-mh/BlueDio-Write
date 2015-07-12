using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BluetoothManagerShared;

namespace ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            ArduinoManager arduino = new ArduinoManager();
            Console.WriteLine("Connecting...");
            if(arduino.connect("301410292603", "0012") == BluetoothManager.Status.Connected)
            {
                Console.WriteLine("Connected!");
            }
            else
            {
                Console.WriteLine("Connection error.");
            }
            
            

            //arduino.write("master:13:0");
            
            while(true)
            {
                string message = Console.ReadLine();
                
            }
        }
    }
}
