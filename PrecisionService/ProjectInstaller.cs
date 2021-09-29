using Precision.Core;
using PrecisionService.Core;
using System.ComponentModel;
using System.ServiceProcess;

namespace PrecisionService
{
	[RunInstaller(true)]
	public class ProjectInstaller : ProjectInstallerBase
	{
		protected override string ServiceName => Const.String.ServiceName;

		protected override ServiceStartMode ServiceStartMode => ServiceStartMode.Automatic;
	}
}
