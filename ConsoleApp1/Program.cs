using Orleans;
using Orleans.Configuration;
using Precision.Core;
using Precision.Core.Orleans.Enum;
using Precision.Core.Orleans.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		static async Task<int> Main(string[] args)
		{


			IClusterClient client = new ClientBuilder()
					.UseLocalhostClustering(Const.Integer.GatewayPort)
					.Configure<ClusterOptions>(options =>
					{
						options.ClusterId = Const.String.ClusterId;
						options.ServiceId = Const.String.ServiceId;
					})
					//不指定IP的話，在無網路情況下會Exception
					.Configure<ClientMessagingOptions>(option =>
					{
						option.LocalAddress = IPAddress.Loopback;
					})
					.Build();


			await client.Connect();

			await Task.Delay(3 * 1000);

			IServiceGrain x86service = client.GetGrain<IServiceGrain>((long)ServiceKeyEnum.Default, "X86.Grain.ServiceGrain");
			string x86Info = await x86service.GetInfo();
			Console.WriteLine(x86Info);
			IServiceGrain x64service = client.GetGrain<IServiceGrain>((long)ServiceKeyEnum.Default, "X64.Grain.ServiceGrain");
			string x64Info = await x64service.GetInfo();
			Console.WriteLine(x64Info);

			Console.ReadLine();

			return 0;
		}
	}
}
