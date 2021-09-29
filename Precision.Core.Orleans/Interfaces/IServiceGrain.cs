using Orleans;
using System.Threading.Tasks;

namespace Precision.Core.Orleans.Interfaces
{
	/// <summary>
	/// 服務Grain
	/// </summary>
	public interface IServiceGrain : IGrainWithIntegerKey
	{
		Task<string> GetInfo();
	}
}
