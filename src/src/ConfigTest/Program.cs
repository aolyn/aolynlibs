using System;
using Aolyn.Config;
using Microsoft.Extensions.Configuration;

namespace ConfigTest
{
	class Program
	{
		static void Main(string[] args)
		{
			//var builder = new ConfigurationBuilder()
			//	.AddJsonFile("appsettings.json", true, true);
			//var configuration = builder.Build();

			//ConfigurationManager.Initialize(configuration);

			//var optionsBuilder = new DbContextOptionsBuilder();

			//ConfigurationManager.Entity.Use(optionsBuilder, "n");
			//ConfigurationManager.Entity.Use(optionsBuilder, "ss");
			//ConfigurationManager.Entity.Use(optionsBuilder, "sl");

			var csn = ConfigurationManager.ConnectionStrings["n"];
			var ss = ConfigurationManager.ConnectionStrings["ss"];
			var sl = ConfigurationManager.ConnectionStrings["sl"];

			Console.WriteLine("Hello World!");
		}
	}
}