using System.Net;
using System.Net.Sockets;

namespace Precision.Core
{
	/// <summary>
	/// 常數
	/// </summary>
	public static class Const
	{
		/// <summary>
		/// 字串
		/// </summary>
		public static class String
		{
			public const string ServiceName = "PrecisionService";
			public const string ServiceName32 = "PrecisionService32";
			public const string ServiceName64 = "PrecisionService64";
			public const string DefaultFileGrainStorageProvider = "FileGrainStorage";
			public const string MemoryGrainStorageProvider = "MemoryGrainStorage";
			public const string LibraryFolderName = "Library";
			public const string ServiceId = "Dev";
			public const string ClusterId = "Dev";
			public const string X64 = "x64";
			public const string X86 = "x86";
			public const string FolderServices = "Services";
			public const string FolderGrains = "Grains";
			public const string FolderLibrary = "Library";
		}
		/// <summary>
		/// 整數
		/// </summary>
		public static class Integer
		{
			public static readonly int GatewayPort = 33842;
			public static readonly int GatewayPort_x64 = 30064;
			public static readonly int GatewayPort_x86 = 30086;

			public static readonly int SiloPort = 13842;
			public static readonly int SiloPort_x64 = 10064;
			public static readonly int SiloPort_x86 = 10086;

			public const int ServiceUninitializeDelay = 1000;
			public const int ServiceStopDelay = 1000;
		}
	}
}
