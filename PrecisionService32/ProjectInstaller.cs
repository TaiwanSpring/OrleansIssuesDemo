using Precision.Core;
using PrecisionService.Core;
using System.ComponentModel;
using System.ServiceProcess;

namespace PrecisionService32
{
	[RunInstaller(true)]
	public class ProjectInstaller32 : ProjectInstallerBase
	{
		protected override string ServiceName => Const.String.ServiceName32;

		protected override ServiceStartMode ServiceStartMode => ServiceStartMode.Manual;
	}
}
