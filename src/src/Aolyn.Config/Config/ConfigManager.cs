using System;
using Aolyn.Config.Data;
using Aolyn.Config.Data.EntityFramework;
using Microsoft.Extensions.Configuration;

namespace Aolyn.Config
{
	public class ConfigManager
	{
		/// <summary>
		/// default ConfigManager instance
		/// </summary>
		public static ConfigManager Default { get; private set; } = new ConfigManager();

		/// <summary>
		/// default configuration
		/// </summary>
		public IConfiguration Configuration { get; private set; }

		/// <summary>
		/// Entity Framework Configuration Manager
		/// </summary>
		public IEntityConfigManager Entity { get; set; }

		/// <summary>
		/// initialize default
		/// </summary>
		/// <param name="configuration"></param>
		public static void Initialize(IConfiguration configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			var efManager = new EntityConfigManager(configuration);

			var manager = new ConfigManager
			{
				Configuration = configuration,
				Entity = efManager,
			};

			Default = manager;
		}

	}
}