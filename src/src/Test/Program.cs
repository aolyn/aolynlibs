using Aolyn.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Test
{
	public class Program
	{
		public static void Main1(string[] args)
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", true, true);
			var configuration = builder.Build();

			ConfigurationManager.Initialize(configuration);

			var optionsBuilder = new DbContextOptionsBuilder();

			ConfigurationManager.Entity.Use(optionsBuilder, "n");
			ConfigurationManager.Entity.Use(optionsBuilder, "ss");
			ConfigurationManager.Entity.Use(optionsBuilder, "sl");

			var csn = ConfigurationManager.ConnectionStrings["n"];
			var ss = ConfigurationManager.ConnectionStrings["ss"];
			var sl = ConfigurationManager.ConnectionStrings["sl"];


			var type = typeof(string);
		}
	}
}