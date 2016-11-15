using System;
using Aolyn.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Aolyn.Data.Sqlite
{
	public class SqliteEntityDataProvider : IEntityDataProvider
	{
		public void Use(DbContextOptionsBuilder optionsBuilder, string connectionString)
		{
			if (optionsBuilder == null)
				throw new ArgumentNullException(nameof(optionsBuilder));
			optionsBuilder.UseSqlite(connectionString);
		}
	}

}
