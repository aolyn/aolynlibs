using Microsoft.EntityFrameworkCore;

namespace Aolyn.Data.EntityFramework
{
	public interface IEntityDataProvider
	{
		void Use(DbContextOptionsBuilder optionsBuilder, string connectionString);
	}
}
