using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.ApplicationParts;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;
using Orleans.Runtime.Placement;
using Precision.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.ServiceProcess;

namespace PrecisionService.Core
{
	public abstract class SiloHostServiceBase : ServiceBase
	{
		protected ISiloHost SiloHost { get; private set; }
		protected IClusterClient ClusterClient { get; private set; }
		protected abstract int SiloPort { get; }
		protected abstract int SiloGateway { get; }
		protected abstract string[] GrainFolders { get; }

		protected override void OnStart(string[] args)
		{
			FileInfo fileinfo = new FileInfo(Assembly.GetEntryAssembly().Location);

			SiloHost = new SiloHostBuilder()
				.UseLocalhostClustering(SiloPort, SiloGateway
				, new IPEndPoint(IPAddress.Loopback, Const.Integer.SiloPort)) //Main Silo IPEndPoint
				.Configure<ClusterOptions>(options =>
				  {
					  options.ClusterId = Const.String.ClusterId;
					  options.ServiceId = Const.String.ServiceId;
				  })
				  //Add services
				  .ConfigureApplicationParts(
					  applicationPartManager =>
					  {
						  for (int i = 0; i < GrainFolders.Length; i++)
						  {
							  string grainDllPath = Path.Combine(fileinfo.DirectoryName, GrainFolders[i]);

							  if (Directory.Exists(grainDllPath))
							  {
								  foreach (var item in Directory.GetFiles(grainDllPath))
								  {
									  FileInfo fileInfo = new FileInfo(item);

									  if (fileInfo.Extension == ".dll")
									  {
										  Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
										  bool needAdd = false;

										  foreach (var type in assembly.GetTypes())
										  {
											  if (!needAdd && type.GetInterface(nameof(IAddressable)) != null)
											  {
												  needAdd = true;
											  }
										  }

										  if (needAdd)
										  {
											  applicationPartManager.AddApplicationPart(assembly).WithReferences();
										  }
									  }
								  }
							  }
						  }
					  })
				 .AddMemoryGrainStorageAsDefault()
#if DEBUG
				 .ConfigureLogging(logging => logging.AddConsole())
#endif
				 .Build();

			SiloHost.StartAsync();

			Console.WriteLine($"{Const.String.ServiceName} Silo Started.");
		}

		protected override void OnStop()
		{
			if (SiloHost != null)
			{
				SiloHost.StopAsync().Wait();
			}
		}
	}
}
