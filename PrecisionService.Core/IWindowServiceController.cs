using System.ServiceProcess;
using System.Threading.Tasks;

namespace PrecisionService.Core
{
	public interface IWindowServiceController
	{
		string ServiceName { get; }
		ServiceControllerStatus Status { get; }
		Task StartAsync();
		Task StopAsync();
	}
}
