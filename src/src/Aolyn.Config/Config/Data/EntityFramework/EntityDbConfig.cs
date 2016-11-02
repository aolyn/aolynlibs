using System;
using Aolyn.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Aolyn.Config.Data.EntityFramework
{
	internal class EntityDbConfig : IEntityDbConfig
	{
		public string Name { get; }
		public string ConnectionString { get; set; }
		public IEntityDataProvider Provider { get; set; }

		public EntityDbConfig(string name, string connectionString, IEntityDataProvider provider)
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));

			Name = name;
			ConnectionString = connectionString;
			Provider = provider;
		}

		public void Use(DbContextOptionsBuilder optionsBuilder)
		{
			Provider.Use(optionsBuilder, ConnectionString);
		}

	}
}