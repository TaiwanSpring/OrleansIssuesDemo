using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace PrecisionService.Core
{
	public class WindowServiceController : IWindowServiceController
	{
		public static IWindowServiceController GetWindowServiceControl(string serviceName)
		{
			try
			{
				return new WindowServiceController(new ServiceController(serviceName));
			}
			catch (Exception e)
			{
				Debug.Print(e.Message);
				return null;
			}
		}

		private readonly ServiceController serviceController;

		public string ServiceName { get { return this.serviceController.ServiceName; } }

		public ServiceControllerStatus Status { get { return this.serviceController.Status; } }

		private WindowServiceController(ServiceController serviceController)
		{
			this.serviceController = serviceController;
		}

		public async Task StartAsync()
		{
			if (this.serviceController.Status == ServiceControllerStatus.Running)
			{
				return;
			}
			await Task.Run(() =>
			{
				this.serviceController.Start();
				this.serviceController.WaitForStatus(ServiceControllerStatus.Running);
			});
		}

		public async Task StopAsync()
		{
			if (this.serviceController.Status == ServiceControllerStatus.Stopped)
			{
				return;
			}

			if (this.serviceController.CanStop)
			{
				await Task.Run(() =>
				{
					this.serviceController.Stop();
					this.serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
				});
			}
			else
			{
				Debug.Fail("");
			}
		}
	}
}
