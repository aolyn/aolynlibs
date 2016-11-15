﻿using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Aolyn.Config
{
	internal class ConnectionStringHelper
	{

		public static ConnectionStringItem[] GetConnectionStrings(IConfigurationSection entityDataConfigNode)
		{
			var efConfigs = entityDataConfigNode.GetSection("ConnectionStrings").GetChildren().ToArray();
			var connectionStrings = efConfigs
				.Select(it => new ConnectionStringItem
				{
					Name = it["Name"],
					ConnectionString = it["ConnectionString"],
					Provider = it["Provider"],
				})
				.ToArray();
			return connectionStrings;
		}

	}
}