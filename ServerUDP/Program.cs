using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ServerUDP.server;

// {"type":"track", "content": "hola mundo"}
namespace ServerUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Server UDP started");
            Console.WriteLine("-----------------");

            Task.Run(() => startUDPServer());
            //Process process = new Process();
            ////process.StartInfo.FileName = "";
            ////process.StartInfo.Arguments = arguments;
            //process.StartInfo.ErrorDialog = true;
            //process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            //process.Start();
            //process.WaitForExit(1000 * 60 * 5);    // Wait up to five minutes.
            Console.ReadKey();
            //Thread.Sleep(Timeout.Infinite);
            //await Task.Run(() => Thread.Sleep(Timeout.Infinite));

            //do
            //{
            //    Console.WriteLine($"Type: quit<Enter> to end {Process.GetCurrentProcess().ProcessName}");
            //}
            //while (!Console.ReadLine().Trim().Equals("quit", StringComparison.OrdinalIgnoreCase));

            while (true) { }
        }

        public static void startUDPServer()
        {
            UDPSocket s = new UDPSocket();
            //s.Server("127.0.0.1", 27000, "ok\n");
            s.Server("0.0.0.0", 27000, "ok\n");
            //s.Server("192.168.1.55", 27000, "ok\n");
        }

        
    }

}


