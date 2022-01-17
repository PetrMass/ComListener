using System;
using System.IO.Ports;

namespace ComListener
{
    public interface ISettingsProvider
    {
        DataProviderSettings GetSettingsFromCmdArgs(string[] args);
    }
}
