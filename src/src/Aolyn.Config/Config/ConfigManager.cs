using System;
using Aolyn.Config.Data.EntityFramework;
using Microsoft.Extensions.Configuration;

namespace Aolyn.Config
{
	public class ConfigManager
	{
		/// <summary>
		/// default configuration
		/// </summary>
		public IConfiguration Configuration { get; private set; }

		/// <summary>
		/// Entity Framework Configuration Manager
		/// </summary>
		public IEntityConfigManager Entity { get; set; }

		public ConnectionStringCollection ConnectionStrings { get; set; }

		public void Initialize(IConfiguration configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			Configuration = configuration;
			Entity = new EntityConfigManager(configuration);

			var entityDataConfigNode = configuration.GetSection("Aolyn.Data");
			var cs1 = ConnectionStringHelper.GetConnectionStrings(entityDataConfigNode);
			var cs2 = new ConnectionStringCollection();
			foreach (var item in cs1)
			{
				cs2.Add(item);
			}
			ConnectionStrings = cs2;
		}

	}
}