using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;
using ServerService;

namespace ServerServiceInstaller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var service = ServiceController.GetServices()
                .FirstOrDefault(x => x.ServiceName == new ServerService.ServerService().ServiceName);

            if (service != null)
            {
                if (MessageBox.Show(@"The service is already installed. remove it ?", @"confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ManagedInstallerClass.InstallHelper(new [] {"/u", typeof(ServerServiceSetup).Assembly.Location});
                }

               return; 
            }

            ManagedInstallerClass.InstallHelper(new[] { typeof(ServerServiceSetup).Assembly.Location });
        }
    }
}
