using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ServerService
{
    [RunInstaller(true)]
    public partial class ServerServiceSetup : Installer
    {
        public ServerServiceSetup()
        {
            InitializeComponent();
        }

        public override void Uninstall(IDictionary savedState)
        {
            try
            {
                using (var controller = new ServiceController(serviceInstaller1.ServiceName))
                {
                    controller.Stop();
                }

                ManagedInstallerClass.InstallHelper(new[] {"/u", typeof(ServerServiceSetup).Assembly.Location});
            }
            catch (Exception)
            {
                //ignore
            }
            

            base.Uninstall(savedState);
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                using (var controller = new ServiceController(serviceInstaller1.ServiceName))
                {
                    controller.Start();
                }
            }
            catch (Exception)
            {
                //ignore
            }
        }
    }
}
