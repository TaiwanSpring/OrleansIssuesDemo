using Precision.Core;
using PrecisionService.Core;
using System.ServiceProcess;

namespace PrecisionService64
{
	class Program
	{
		static void Main(string[] args)
		{
			ServiceProgram.Run<WindowService64>(args, Const.String.ServiceName64);
		}
	}
}
