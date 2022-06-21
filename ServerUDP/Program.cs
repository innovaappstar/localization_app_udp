using System;
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

            UDPSocket s = new UDPSocket();
            s.Server("127.0.0.1", 27000, "ok\n");

            Console.ReadKey();
        }
    }
}


