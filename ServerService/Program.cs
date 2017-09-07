using System.ServiceProcess;

namespace ServerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceBase[] servicesToRun = {
                new ServerService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
