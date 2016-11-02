using Aolyn.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Aolyn.Config.Data.EntityFramework
{
	internal interface IEntityDbConfig
	{
		string Name { get; }
		string ConnectionString { get; }
		IEntityDataProvider Provider { get; }
		void Use(DbContextOptionsBuilder optionsBuilder);
	}
}