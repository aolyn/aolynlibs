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

			_connectionStrings = ConnectionStringHelper.GetConnectionStrings(entityDataConfigNode);

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

		private static EntityDataProviderItem[] GetProviders(IConfigurationSection providersNode)
		{
			var providerNodes = providersNode.GetChildren().ToArray();

			var providerConfigurationItems = providerNodes
				.Select(it => new ProviderConfigurationItem
				{
					Name = it["Name"],
					Type = it["Type"],
				})
				.ToArray();

			var defaultProviders = DefaultProviders.Value;

			var providers = new List<EntityDataProviderItem>();
			providers.AddRange(defaultProviders);
			foreach (var providerNode in providerConfigurationItems)
			{
				var provider = ReflectHelper.CreateInstanceByIdentifier<object>(providerNode.Type) as IEntityDataProvider;
				if (provider == null)
					continue;

				//remove exist same name provider
				var sameNameProvider = providers.FirstOrDefault(it => it.Name == providerNode.Name);
				if (sameNameProvider != null)
					providers.Remove(sameNameProvider);

				providers.Add(new EntityDataProviderItem
				{
					Name = providerNode.Name,
					Provider = provider
				});
			}

			return providers.ToArray();
		}

		private static readonly Lazy<List<EntityDataProviderItem>> DefaultProviders = new Lazy<List<EntityDataProviderItem>>(GetDefaultProviders);

		private static List<EntityDataProviderItem> GetDefaultProviders()
		{
			var defaultProviders = new[]
			{
				new ProviderConfigurationItem
				{
					Name = "Npgsql",
					Type = "Aolyn.Data.Npgsql.NpgsqlEntityDataProvider, Aolyn.Data.Npgsql",
				},
				new ProviderConfigurationItem
				{
					Name = "SqlServer",
					Type = "Aolyn.Data.SqlServer.SqlServerEntityDataProvider, Aolyn.Data.SqlServer",
				},
				new ProviderConfigurationItem
				{
					Name = "Sqlite",
					Type = "Aolyn.Data.Sqlite.SqliteEntityDataProvider, Aolyn.Data.Sqlite",
				}
			};

			var providers = new List<EntityDataProviderItem>();
			foreach (var item in defaultProviders)
			{
				try
				{
					var provider = ReflectHelper.CreateInstanceByIdentifier<object>(item.Type) as IEntityDataProvider;
					if (provider == null)
						continue;
					providers.Add(new EntityDataProviderItem
					{
						Name = item.Name,
						Provider = provider,
					});
				}
				catch (Exception)
				{
					//ignore not reference default provider
				}
			}

			return providers;
		}

		class ProviderConfigurationItem
		{
			public string Name { get; set; }
			public string Type { get; set; }
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