using System.ServiceProcess;
using WebServer;

namespace ServerService
{
    public partial class ServerService : ServiceBase
    {
        public ServerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Listener.Start();
        }

        protected override void OnStop()
        {
            Listener.Stop();
        }
    }
}
