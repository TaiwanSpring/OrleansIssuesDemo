﻿using Precision.Core.Orleans.Interfaces;
using System.Threading.Tasks;

namespace X64.Grain
{
	public class ServiceGrain : Orleans.Grain, IServiceGrain
	{
		public Task<string> GetInfo()
		{
			return Task.FromResult(typeof(ServiceGrain).FullName);
		}
	}
}
