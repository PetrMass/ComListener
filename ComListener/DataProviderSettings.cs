using System;
using System.IO.Ports;

namespace ComListener
{
    public class DataProviderSettings
    {
        public int baudRate;
        public string portName;

        public void SetPortName(string defaultPortName)
        {
            string portName;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("{0}", s);
            }

            Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            portName = Console.ReadLine();

            if (portName == "" || !(portName.ToLower()).StartsWith("com"))
            {
                portName = defaultPortName;
            }
            this.portName = portName;
        }
        public void SetBaudRate()
        {
            int baudRate;

            Console.Write("Enter BaudRate value ");
            baudRate = Convert.ToInt32(Console.ReadLine());
        }

    }
}
