using Lively.IPC.Client;
using Sharer;
using System;
using System.Text;

namespace RotaryEncoder.Demo
{
    static class Program
    {
        static void Main(string[] args)
        {
            var hue = 0;
            var wallpaperClient = new CommandsClient();
            var connection = new SharerConnection("COM3", 115200);
            connection.Connect();

            connection.UserDataReceived += async(s, e) =>
            {
                string msg = Encoding.ASCII.GetString(e.Data);
                if (int.TryParse(msg, out int val))
                {
                    hue = val == 1 ? (hue + 10) : (hue - 10);
                    // OR use livelycu commandline utility.
                    await wallpaperClient.AutomationCommandAsync(["setprop", "--property", "hue=" + Clamp(hue, -100, 100)]);
                }
            };
            Console.ReadKey();
        }

        private static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            if (value.CompareTo(max) > 0)
                return max;

            return value;
        }
    }
}
