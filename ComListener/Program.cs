using System;
using System.IO.Ports;


namespace ComListener
{
    class Program
    {
        static void Main(string[] args)
        {
            ISettingsProvider settingsProvider = new SettingsProvider();
            var settings = settingsProvider.GetSettingsFromCmdArgs(args);

            DataProvider provider = new DataProvider(settings);
            provider.MessageReceived += Handler;
            provider.StartPolling();

            Console.ReadLine();
            provider.StopPolling();
        }

        private static void Handler(object o, EventArgs e)
        {
            DataProvider dp = (DataProvider)o;

            for (int i = 0; i < dp._buffer.Length; i = i + 4)
            {
                Console.WriteLine($"{BitConverter.ToSingle(dp._buffer, i)}"); // преобразование в float
            }
        }
    }
}
