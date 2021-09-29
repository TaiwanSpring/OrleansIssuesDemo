using Precision.Core;
using PrecisionService.Core;

namespace PrecisionService64
{
	class WindowService64 : SiloHostServiceBase
	{
		protected override int SiloPort => Const.Integer.SiloPort_x64;
		protected override int SiloGateway => Const.Integer.GatewayPort_x64;
		protected override string[] GrainFolders => new string[] { Const.String.FolderGrains };
		public WindowService64()
		{
			ServiceName = Const.String.ServiceName64;
		}
	}
}
