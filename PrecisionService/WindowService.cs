using Precision.Core;
using PrecisionService.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrecisionService
{
	class WindowService : SiloHostServiceBase
	{
		protected override int SiloPort => Const.Integer.SiloPort;
		protected override int SiloGateway => Const.Integer.GatewayPort;
		protected override string[] GrainFolders => new string[] { Const.String.FolderServices, Const.String.FolderGrains };

		private IWindowServiceController[] windowServiceControllers;

		public WindowService()
		{
			ServiceName = Const.String.ServiceName;

			List<IWindowServiceController> windowServiceControllers = new List<IWindowServiceController>();
			IWindowServiceController windowServiceController = WindowServiceController.GetWindowServiceControl(Const.String.ServiceName32);
			if (windowServiceController != null)
			{
				windowServiceControllers.Add(windowServiceController);
			}
			windowServiceController = WindowServiceController.GetWindowServiceControl(Const.String.ServiceName64);
			if (windowServiceController != null)
			{
				windowServiceControllers.Add(windowServiceController);
			}
			this.windowServiceControllers = windowServiceControllers.ToArray();
		}

		protected override void OnStart(string[] args)
		{
			base.OnStart(args);
#if DEBUG
#else
			if (this.windowServiceControllers.Length > 0)
			{
				Task[] tasks = new Task[this.windowServiceControllers.Length];
				for (int i = 0; i < this.windowServiceControllers.Length; i++)
				{
					tasks[i] = this.windowServiceControllers[i].StartAsync();
				}
				Task.WhenAll(tasks);
			}
#endif
		}

		protected override void OnStop()
		{
#if DEBUG
#else
			if (this.windowServiceControllers.Length > 0)
			{
				Task[] tasks = new Task[this.windowServiceControllers.Length];
				for (int i = 0; i < this.windowServiceControllers.Length; i++)
				{
					tasks[i] = this.windowServiceControllers[i].StopAsync();
				}
				Task.WhenAll(tasks);
			}
#endif
			base.OnStop();
		}
	}
}
