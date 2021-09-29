using Precision.Core;
using PrecisionService.Core;
using System.ServiceProcess;

namespace PrecisionService32
{
	class Program
	{
		static void Main(string[] args)
		{
			ServiceProgram.Run<WindowService32>(args, Const.String.ServiceName32);
		}
	}
}