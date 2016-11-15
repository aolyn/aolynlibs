using System;
using Aolyn.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Aolyn.Data.SqlServer
{
	public class SqlServerEntityDataProvider : IEntityDataProvider
	{
		public void Use(DbContextOptionsBuilder optionsBuilder, string connectionString)
		{
			if (optionsBuilder == null)
				throw new ArgumentNullException(nameof(optionsBuilder));
			optionsBuilder.UseSqlServer(connectionString);
		}
	}

}
