using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Aolyn.Config
{
	public class ConnectionStringCollection : IReadOnlyCollection<ConnectionStringItem>
	{
		private readonly List<ConnectionStringItem> _items = new List<ConnectionStringItem>();

		public int Count => _items.Count;

		public IEnumerator<ConnectionStringItem> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		public ConnectionStringItem this[string key] => _items.FirstOrDefault(it => it.Name == key);

		internal void Add(ConnectionStringItem item)
		{
			_items.Add(item);
		}

	}
}
