using Precision.Core;
using PrecisionService.Core;
using System.ComponentModel;
using System.ServiceProcess;

namespace PrecisionService64
{
	[RunInstaller(true)]
	public class ProjectInstaller64 : ProjectInstallerBase
	{
		protected override string ServiceName => Const.String.ServiceName64;

		protected override ServiceStartMode ServiceStartMode => ServiceStartMode.Manual;
	}
}
