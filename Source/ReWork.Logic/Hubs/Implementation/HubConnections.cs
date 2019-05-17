using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Hubs.Implementation
{
    public class HubConnections<TKey>
    {
        private Dictionary<TKey, HashSet<string>> _connections;
        private object _locker;

        public HubConnections()
        {
            _connections = new Dictionary<TKey, HashSet<string>>();
            _locker = new object();
        }


        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }


        public void Add(TKey key, string connectionId)
        {
            lock (_locker)
            {
                if (_connections.ContainsKey(key))
                {
                    _connections[key].Add(connectionId);
                }
                else
                {
                    HashSet<string> hubConnections = new HashSet<string>();
                    hubConnections.Add(connectionId);
                    _connections.Add(key, hubConnections);
                }
            }
        }


        public IEnumerable<string> GetConnections(TKey key)
        {
            if (!_connections.ContainsKey(key))
                throw new KeyNotFoundException($"Element with key={key} not found");

            return _connections[key];
        }


        public void Remove(TKey key, string connectionId)
        {
            if (!_connections.ContainsKey(key))
                throw new KeyNotFoundException($"Element with key={key} not found");

            lock (_locker)
            {
                HashSet<string> hubConnections = _connections[key];
                hubConnections.Remove(connectionId);

                if (hubConnections.Count == 0)
                {
                    _connections.Remove(key);
                }
            }
        }
    }
}
