using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace WebServer
{
    public class Listener
    {
        private static IDisposable _listener;

        private static readonly StartOptions Options = new StartOptions
        {
            Urls = {ConfigurationManager.AppSettings["url"]},
            Port = int.Parse(ConfigurationManager.AppSettings["port"])
        };

        public static IDisposable Start()
        {
            _listener = WebApp.Start<Startup>(Options);

            return _listener;
        }

        public static void Stop()
        {
            _listener.Dispose();
        }
    }
}