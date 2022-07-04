using System;
using System.Threading;
using System.Threading.Tasks;
using ServerUDP.server;

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
            while (true)
            {

            }
            //Console.ReadKey();
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


