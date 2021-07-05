using Sharer;
using Sharer.UserData;
using System;

namespace ArduinoSharp
{
    class Program
    {
        private static int hue = 0;
        private static readonly string uniqueAppName = "LIVELY:DESKTOPWALLPAPERSYSTEM";
        private static readonly string pipeServerName = uniqueAppName + Environment.UserName;

        static void Main(string[] args)
        {
            var connection = new SharerConnection("COM3", 115200);
            connection.Connect();
            connection.UserDataReceived += UserDataReceived;
            Console.ReadKey();
        }

        private static void UserDataReceived(object sender, UserDataReceivedEventArgs e)
        {
            string msg = System.Text.Encoding.ASCII.GetString(e.Data);
            if (int.TryParse(msg, out int val))
            {
                hue = val == 1 ? (hue+10) : (hue-10);
                //or execute livelycu commandline commands..
                livelywpf.Helpers.PipeClient.SendMessage(
                    pipeServerName, 
                    new string[] { "setprop", "--property", "hue=" + Clamp(hue, -100, 100)});
            }
        }

        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            if (value.CompareTo(max) > 0)
                return max;

            return value;
        }
    }
}
