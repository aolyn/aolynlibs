using Aolyn.Data.EntityFramework;

namespace Aolyn.Config.Data.EntityFramework
{
	internal class EntityDataProviderItem
	{
		public string Name { get; set; }

		public IEntityDataProvider Provider { get; set; }

		public override string ToString()
		{
			return $"{Name}, {Provider}";
		}
	}
}