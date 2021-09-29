using Precision.Core;
using PrecisionService.Core;

namespace PrecisionService32
{
	class WindowService32 : SiloHostServiceBase
	{
		protected override int SiloPort => Const.Integer.SiloPort_x86;
		protected override int SiloGateway => Const.Integer.GatewayPort_x86;
		protected override string[] GrainFolders => new string[] { Const.String.FolderGrains };

		public WindowService32()
		{
			ServiceName = Const.String.ServiceName32;
		}
	}
}
