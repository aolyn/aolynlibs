using System;
using Aolyn.Config.Data.EntityFramework;
using Microsoft.Extensions.Configuration;

namespace Aolyn.Config
{
	public static class ConfigurationManager
	{
		/// <summary>
		/// default ConfigManager instance
		/// </summary>
		private static ConfigManager _default = new ConfigManager();

		/// <summary>
		/// default configuration
		/// </summary>
		public static IConfiguration Configuration => _default.Configuration;

		/// <summary>
		/// Entity Framework Configuration Manager
		/// </summary>
		public static IEntityConfigManager Entity => _default.Entity;

		public static ConnectionStringCollection ConnectionStrings => _default.ConnectionStrings;

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
