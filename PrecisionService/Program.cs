using Precision.Core;
using PrecisionService.Core;

namespace PrecisionService
{
	class Program
	{
		static void Main(string[] args)
		{			
			ServiceProgram.Run<WindowService>(args, Const.String.ServiceName);
		}
	}
}
