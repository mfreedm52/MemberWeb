using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace FileMover
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();

            //https://msdn.microsoft.com/en-us/library/system.serviceprocess.serviceprocessinstaller(v=vs.110).aspx
            ServiceInstaller service1 = new ServiceInstaller();
            ServiceProcessInstaller process = new ServiceProcessInstaller();

            process.Account = ServiceAccount.LocalSystem;

            service1.ServiceName = "File Mover";
            service1.StartType = ServiceStartMode.Manual;

            Installers.Add(process);
            Installers.Add(service1);

            

        }
    }
}
