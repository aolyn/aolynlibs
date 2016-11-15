namespace Aolyn.Config
{
	public class ConnectionStringItem
	{
		public string Name { get; set; }

		public string ConnectionString { get; set; }

		public string Provider { get; set; }

		public override string ToString()
		{
			return $"{Name}, {Provider}";
		}
	}
}