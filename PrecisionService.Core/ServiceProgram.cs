using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;

namespace PrecisionService.Core
{
	public static class ServiceProgram
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		public static void Run<T>(string[] args, string serviceName)
		{
			if (Environment.UserInteractive)
			{
#if DEBUG
				Type type = typeof(T);

				ServiceBase serviceBase = type.Assembly.CreateInstance(type.FullName) as ServiceBase;

				Console.Title = serviceBase.ServiceName;

				if (serviceBase != null)
				{
					try
					{
						RunInteractive(serviceBase);
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
						Console.ReadLine();
					}
				}
#else

				if (args.Length == 1)
				{
					switch (args[0])
					{
						case "install":
						case "-install":
						case "-i":
							System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
							AssemblyInstaller assemblyInstaller = GetInstaller<T>();
							if (IsInstalled(serviceName))
							{
								if (IsRunning(serviceName))
								{
									CloseService(serviceName);
								}
								assemblyInstaller.Uninstall(hashtable);
							}
							assemblyInstaller.Install(hashtable);
							break;
						case "uninstall":
						case "-uninstall":
						case "-u":
							if (IsInstalled(serviceName))
							{
								if (IsRunning(serviceName))
								{
									CloseService(serviceName);
								}
								GetInstaller<T>().Uninstall(new System.Collections.Hashtable());
							}
							break;
						case "open":
						case "o":
						case "-o":
							if (IsInstalled(serviceName))
							{
								OpenService(serviceName);
							}
							break;
						case "c":
						case "close":
						case "-c":
							if (IsInstalled(serviceName))
							{
								if (IsRunning(serviceName))
								{
									CloseService(serviceName);
								}
							}
							break;
						default:
							throw new NotImplementedException();
					}
				}
#endif
			}
			else
			{
				Type type = typeof(T);
				ServiceBase serviceBase = type.Assembly.CreateInstance(type.FullName) as ServiceBase;

				if (serviceBase != null)
				{
					ServiceBase.Run(serviceBase);
				}
			}
		}

		public static bool IsInstalled(string serviceName)
		{
			using (ServiceController controller =
				new ServiceController(serviceName))
			{
				try
				{
					ServiceControllerStatus status = controller.Status;
				}
				catch (Exception e)
				{
					Debug.Print(e.Message);
					return false;
				}
				return true;
			}
		}

		public static bool IsRunning(string serviceName)
		{
			if (!IsInstalled(serviceName)) return false;

			using (ServiceController controller =
				new ServiceController(serviceName))
			{
				return (controller.Status == ServiceControllerStatus.Running);
			}
		}
		public static void OpenService(string serviceName)
		{
			if (IsInstalled(serviceName))
			{
				if (!IsRunning(serviceName))
				{
					using (ServiceController controller =
					new ServiceController(serviceName))
					{
						controller.Start();
					}
				}
			}
		}
		public static void CloseService(string serviceName)
		{
			if (IsInstalled(serviceName))
			{
				using (ServiceController controller =
					new ServiceController(serviceName))
				{
					controller.Stop();
				}
			}
		}
		private static AssemblyInstaller GetInstaller<T>()
		{
			AssemblyInstaller installer = new AssemblyInstaller(
				typeof(T).Assembly, null);
			installer.UseNewContext = true;
			return installer;
		}

		private static void RunInteractive(ServiceBase service)
		{
			// 利用Reflection取得非公開之 OnStart() 方法資訊
			MethodInfo onStartMethod = typeof(ServiceBase).GetMethod("OnStart",
				BindingFlags.Instance | BindingFlags.NonPublic);

			// 執行 OnStart 方法
			Console.WriteLine("Starting {0}...", service.ServiceName);
			onStartMethod.Invoke(service, new object[] { new string[] { } });
			Console.WriteLine("Started {0}", service.ServiceName);

			Console.WriteLine("Press any key to stop the services");
			Console.ReadLine();

			// 利用Reflection取得非公開之 OnStop() 方法資訊
			MethodInfo onStopMethod = typeof(ServiceBase).GetMethod("OnStop",
				BindingFlags.Instance | BindingFlags.NonPublic);

			// 執行 OnStop 方法
			Console.WriteLine("Stopping {0}...", service.ServiceName);
			onStopMethod.Invoke(service, null);
			Console.WriteLine("Stopped {0}", service.ServiceName);
		}
	}
}
