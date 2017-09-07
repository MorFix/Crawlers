using System;
using WebServer;

namespace ServerSelfHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (Listener.Start())
            {
                Console.WriteLine("Listening...");
                Console.ReadLine();
            } 
        }
    }
}
