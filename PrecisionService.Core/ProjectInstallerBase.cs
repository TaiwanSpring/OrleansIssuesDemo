using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace PrecisionService.Core
{
	[RunInstaller(true)]
	public abstract class ProjectInstallerBase : Installer
	{
		protected abstract string ServiceName { get; }
		protected abstract ServiceStartMode ServiceStartMode { get; }
		protected virtual ServiceAccount ServiceAccount { get; } = ServiceAccount.LocalSystem;
		private ServiceProcessInstaller ServiceProcessInstaller;
		private ServiceInstaller ServiceInstaller;
		public ProjectInstallerBase()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			ServiceProcessInstaller = new ServiceProcessInstaller();
			ServiceInstaller = new ServiceInstaller();
			// 
			// ServiceProcessInstaller
			// 
			ServiceProcessInstaller.Account = ServiceAccount.LocalSystem;
			ServiceProcessInstaller.Password = null;
			ServiceProcessInstaller.Username = null;
			// 
			// ServiceInstaller
			// 
			ServiceInstaller.ServiceName = ServiceName;
			ServiceInstaller.StartType = ServiceStartMode;
			// 
			// Installer
			// 
			Installers.AddRange(new Installer[] {
			ServiceProcessInstaller,
			ServiceInstaller});
		}
	}
}
