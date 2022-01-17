using System;
using System.IO.Ports;

namespace ComListener
{
    public class SettingsProvider : ISettingsProvider
    {
        public DataProviderSettings GetSettingsFromCmdArgs(string[] args)
        {
            var dPS = new DataProviderSettings();
            dPS.SetPortName("COM29");
            dPS.SetBaudRate();
            return dPS;
        }
    }
}
