using System;
using Aolyn.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Aolyn.Data.Npgsql
{
	public class NpgsqlEntityDataProvider : IEntityDataProvider
	{
		public void Use(DbContextOptionsBuilder optionsBuilder, string connectionString)
		{
			if (optionsBuilder == null)
				throw new ArgumentNullException(nameof(optionsBuilder));
			optionsBuilder.UseNpgsql(connectionString);
		}
	}

}
