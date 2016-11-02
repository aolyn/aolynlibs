using System;
using System.Collections.Generic;
using System.Linq;
using Aolyn.Data.EntityFramework;
using Aolyn.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Aolyn.Config.Data.EntityFramework
{
	internal class EntityConfigManager : IEntityConfigManager
	{
		private EntityDbConfig[] _configs = new EntityDbConfig[0];
		private ConnectionStringItem[] _connectionStrings;
		private EntityDataProviderItem[] _providers;

		public EntityConfigManager(IConfiguration configuration)
		{
			Initialize(configuration);
		}

		private void Initialize(IConfiguration configuration)
		{
			var entityDataConfigNode = configuration.GetSection("Aolyn.Data");

			var providersNode = entityDataConfigNode.GetSection("EntityFramework:Providers");
			_providers = GetProviders(providersNode);

			_connectionStrings = GetConnectionStrings(entityDataConfigNode);

			var configs = new List<EntityDbConfig>();
			foreach (var item in _connectionStrings)
			{
				var providerName = item.Provider;
				var provider = _providers.FirstOrDefault(it => it.Name == providerName);
				if (provider == null)
					continue;

				var dbItem = new ConnectionStringItem
				{
					Name = item.Name,
					Provider = providerName,
					ConnectionString = item.ConnectionString,
				};
				configs.Add(new EntityDbConfig(dbItem.Name, dbItem.ConnectionString, provider.Provider));
			}

			_configs = configs.ToArray();
		}

		private static ConnectionStringItem[] GetConnectionStrings(IConfigurationSection entityDataConfigNode)
		{
			var efConfigs = entityDataConfigNode.GetSection("ConnectionStrings").GetChildren().ToArray();
			var connectionStrings = efConfigs
				.Select(it => new ConnectionStringItem
				{
					Name = it["Name"],
					ConnectionString = it["ConnectionString"],
					Provider = it["Provider"],
				})
				.ToArray();
			return connectionStrings;
		}

		private static EntityDataProviderItem[] GetProviders(IConfigurationSection providersNode)
		{
			var providerNodes = providersNode.GetChildren().ToArray();
			if (!providerNodes.Any()) return new EntityDataProviderItem[0];

			var providers = new List<EntityDataProviderItem>();
			foreach (var providerNode in providerNodes)
			{
				var name = providerNode["Name"];
				var type = providerNode["Type"];

				var provider = ReflectHelper.CreateInstanceByIdentifier<object>(type) as IEntityDataProvider;
				if (provider == null)
					continue;

				providers.Add(new EntityDataProviderItem
				{
					Name = name,
					Provider = provider
				});
			}

			return providers.ToArray();
		}

		/// <summary>
		/// use specific database
		/// </summary>
		/// <param name="name">database config name</param>
		/// <returns></returns>
		private IEntityDbConfig GetProvider(string name)
		{
			return _configs.FirstOrDefault(it => it.Name == name);
		}

		/// <summary>
		/// use specific database
		/// </summary>
		/// <param name="optionsBuilder"></param>
		/// <param name="database">database config name</param>
		public void Use(DbContextOptionsBuilder optionsBuilder, string database)
		{
			var provider = GetProvider(database);
			if (provider == null)
			{
				var connectionString = _connectionStrings.FirstOrDefault(it => it.Name == database);
				if (connectionString != null)
					throw new InvalidOperationException($"provider of connection string {database} is not a EntityFramework data provider");

				throw new ArgumentOutOfRangeException(nameof(database), "Database configuration error can't find Database: " + database);
			}
			provider.Use(optionsBuilder);
		}
	}
}