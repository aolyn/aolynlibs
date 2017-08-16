using System;
using Aolyn.Config.Data.EntityFramework;
using Microsoft.Extensions.Configuration;

namespace Aolyn.Config
{
	public static class ConfigurationManager
	{
		private static bool _initialized;
		private static readonly object InitializeLock = new object();

		/// <summary>
		/// default ConfigManager instance
		/// </summary>
		private static ConfigManager _default;

		/// <summary>
		/// default configuration
		/// </summary>
		public static IConfiguration Configuration => Default?.Configuration;

		/// <summary>
		/// Entity Framework Configuration Manager
		/// </summary>
		public static IEntityConfigManager Entity => Default?.Entity ?? EntityConfigManager.Empty;

		public static ConnectionStringCollection ConnectionStrings => Default?.ConnectionStrings;

		/// <summary>
		/// default ConfigManager instance
		/// </summary>
		private static ConfigManager Default
		{
			get
			{
				if (_default != null || _initialized)
					return _default;

				//do default initialize when first access
				lock (InitializeLock)
				{
					if (_default == null && !_initialized)
					{
						_default = new ConfigManager();
						var envConfig = new ConfigurationBuilder()
							.AddEnvironmentVariables("ASPNETCORE_")
							.Build();

						var environmentName = envConfig["ENVIRONMENT"];

						var builder = new ConfigurationBuilder()
							//.SetBasePath(env.ContentRootPath)
							.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
							.AddEnvironmentVariables();

						if (!string.IsNullOrWhiteSpace(environmentName))
						{
							builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
						}
						var configuration = builder.Build();

						Initialize(configuration);
						_initialized = true;
					}
				}

				return _default;
			}
		}

		/// <summary>
		/// initialize default
		/// </summary>
		/// <param name="configuration"></param>
		public static void Initialize(IConfiguration configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			var cfg = new ConfigManager();
			cfg.Initialize(configuration);
			_default = cfg;
		}

	}
}
