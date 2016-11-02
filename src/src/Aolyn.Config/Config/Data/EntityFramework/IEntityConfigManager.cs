using Microsoft.EntityFrameworkCore;

namespace Aolyn.Config.Data.EntityFramework
{
	public interface IEntityConfigManager
	{
		/// <summary>
		/// use specific database by Configuration name
		/// </summary>
		/// <param name="optionsBuilder"></param>
		/// <param name="database">database config name</param>
		void Use(DbContextOptionsBuilder optionsBuilder, string database);
	}
}