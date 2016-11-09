using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace System
{


    internal class MemoryCache<TKey, TValue> : ICache<TKey, TValue>
    {
        Dictionary<TKey, IDataItem<TValue>> _map = new Dictionary<TKey, IDataItem<TValue>>();
        ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        SemaphoreSlim _asyncLock;

        SemaphoreSlim AsyncLock
        {
            get
            {
                if (_asyncLock == null)
                {
                    _asyncLock = new SemaphoreSlim(1, 1);
                }
                return _asyncLock;
            }
        }


        public int Count
        {
            get
            {
                return _map.Count;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return _map.Keys;
            }
        }

        #region Get
        public TValue Get(TKey key, Func<TValue> factory)
        {
            // Check cache
            _lock.EnterReadLock();
            IDataItem<TValue> val;
            try
            {
                if (_map.TryGetValue(key, out val))
                    return val.Item;
            }
            finally
            {
                _lock.ExitReadLock();
            }


            // Cache it
            _lock.EnterWriteLock();
            try
            {
                // Check again
                if (_map.TryGetValue(key, out val))
                    return val.Item;

                val = new DataItem<TValue>();
                // Create it
                val.Item = factory();

                // Store it
                _map.Add(key, val);

                // Done
                return val.Item;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public async Task<TValue> GetAsync(TKey key, Func<Task<TValue>> factory)
        {
            // Check cache
            //_lock.EnterReadLock();
            await AsyncLock.WaitAsync(-1);
            IDataItem<TValue> val;
            try
            {
                if (_map.TryGetValue(key, out val))
                    return val.Item;
            }
            finally
            {
                AsyncLock.Release();
            }


            // Cache it
            await AsyncLock.WaitAsync(-1);
            try
            {
                // Check again
                if (_map.TryGetValue(key, out val))
                    return val.Item;

                val = new DataItem<TValue>();
                // Create it
                val.Item = await factory();

                // Store it
                _map.Add(key, val);

                // Done
                return val.Item;
            }
            finally
            {
                AsyncLock.Release();
            }
        }


        #endregion

        /// <summary>
        /// 获取数据,没有返回默认值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                _lock.EnterReadLock();
                IDataItem<TValue> val;
                try
                {
                    if (_map.TryGetValue(key, out val))
                        return val.Item;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
                return default(TValue);
            }
            set
            {
                AddOrUpdate(key, value);
            }
        }

        public bool Update(TKey key, TValue value)
        {
            _lock.EnterReadLock();
            IDataItem<TValue> val;
            try
            {
                if (!_map.TryGetValue(key, out val))
                    return false;
                val.Item = value;
                return true;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }


        public bool Add(TKey key, TValue value)
        {
            _lock.EnterReadLock();
            IDataItem<TValue> val;
            try
            {
                if (_map.TryGetValue(key, out val))
                    return false;
                val = new DataItem<TValue>();
                val.Item = value;
                _map.Add(key, val);
                return true;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            _lock.EnterReadLock();
            IDataItem<TValue> val;
            try
            {
                if (_map.TryGetValue(key, out val))
                    val.Item = value;
                else
                {
                    val = new DataItem<TValue>();
                    val.Item = value;
                    _map.Add(key, val);
                }
            }
            finally
            {
                _lock.ExitReadLock();
            }

        }

        public bool Remove(TKey key)
        {
            _lock.EnterReadLock();
            try
            {
                return _map.Remove(key);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Flush()
        {
            // Cache it
            _lock.EnterWriteLock();
            try
            {
                _map.Clear();
            }
            finally
            {
                _lock.ExitWriteLock();
            }

        }

        public bool ContainsKey(TKey key)
        {
            _lock.EnterReadLock();
            IDataItem<TValue> val;
            try
            {
                if (_map.TryGetValue(key, out val))
                    return true;
                return false;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _map.Select(o => new KeyValuePair<TKey, TValue>(o.Key, o.Value.Item)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // return ((IEnumerable)_map).GetEnumerator();
            return GetEnumerator();
        }
    }
}
