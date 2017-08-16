using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Aolyn.Config
{
	public class ConnectionStringCollection : IReadOnlyCollection<ConnectionStringSetting>
	{
		private readonly List<ConnectionStringSetting> _items = new List<ConnectionStringSetting>();

		public int Count => _items.Count;

		public IEnumerator<ConnectionStringSetting> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		public ConnectionStringSetting this[string key] => _items.FirstOrDefault(it => it.Name == key);

		internal void Add(ConnectionStringSetting item)
		{
			_items.Add(item);
		}

	}
}
