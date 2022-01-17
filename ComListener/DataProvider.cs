using System;
using System.IO.Ports;

namespace ComListener
{
    class DataProvider
    {
        private readonly SerialPort _serialPort;
        public EventHandler MessageReceived;

        public byte[] _buffer;
        byte[] bArray = new byte[] { 65, 79, 83 }; // массив содержит 3 начальных байта пакета 0x41,0x4f,0x53 

        public DataProvider(DataProviderSettings settings)
        {
            // по умолчанию COM29 19200 8N1
            _serialPort = new SerialPort(settings.portName, settings.baudRate, Parity.None, 8, StopBits.One);
            _buffer = new byte[12]; // 12 байт - 4 float
        }

        public void StartPolling()
        {
            _serialPort.Open();
            _serialPort.DataReceived += SerialPortDataReceived;
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytesToRead = _serialPort.BytesToRead;
            byte b;
            int indexCount = 0;
            bool exit = false;

            if (bytesToRead >= _buffer.Length + 5)  // проверка на достаточное количество байт в буфере приема
            {
                for (int i = 0; i < bytesToRead - 14; i++) // поиск совпадения трех байтов подряд, сигнализирующих о начале пакета
                {
                    b = (byte)_serialPort.ReadByte();

                    if (b == bArray[indexCount]) indexCount += 1;
                    else indexCount *= 0;

                    if (indexCount == 3 && _serialPort.BytesToRead >= 14) // true - байты совпали  
                    {
                        for (int y = 0; y < 12; y++)
                        {
                            _buffer[y] = (byte)_serialPort.ReadByte();
                        }
                        MessageReceived(this, e);
                        exit = true;
                    }
                    if (exit) break;
                }
            }
        }

        public void StopPolling()
        {
            _serialPort.Close();
        }
    }
}
