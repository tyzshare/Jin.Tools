using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System
{

    internal class ExpireMemoryCache<TKey, TValue> : IExpireCache<TKey, TValue>
    {

        public ExpireMemoryCache()
            : this(new TimeSpan(1, 0, 0))
        {

        }

        bool _state;

        IExpireCallback _callback;

        public ExpireMemoryCache(TimeSpan expireTime)
        {
            this.ExpireTime = expireTime;

            _callback = GuidExpireCallback.Create(delegate
            {
                if (!_state)
                {
                    _lock.EnterReadLock();
                    List<TKey> list = new List<TKey>();
                    foreach (var item in _map)
                    {
                        if (item.Value.Expire)
                        {
                            if (OnClearExpire != null)
                            {
                                OnClearExpire(this, new ClearExpireEventArgs<TValue>(item.Value.Item));
                            }
                            list.Add(item.Key);
                        }
                    }
                    _lock.ExitReadLock();
                    _lock.EnterWriteLock();
                    foreach (var item in list)
                    {
                        _map.Remove(item);
                    }
                    _lock.ExitWriteLock();
                }
            });
            CacheFactory.AddCallback(_callback);
        }

        Dictionary<TKey, IExpireItem<TValue>> _map = new Dictionary<TKey, IExpireItem<TValue>>();
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

        /// <summary>
        /// 获取或设置过期时间
        /// </summary>
        public TimeSpan ExpireTime { get; set; }

        public bool Async { get; set; }

        public TValue Get(TKey key, Func<TValue> factory)
        {
            // Check cache
            _lock.EnterReadLock();

            _state = true;
            IExpireItem<TValue> val;
            try
            {
                if (_map.TryGetValue(key, out val) && !val.Expire)
                    return val.Item;
            }
            finally
            {
                _lock.ExitReadLock();
                _state = false;
            }


            // Cache it
            _lock.EnterWriteLock();
            _state = true;
            try
            {
                // Check again
                if (_map.TryGetValue(key, out val) && !val.Expire)
                    return val.Item;



                if (val == null)
                {
                    // Create it
                    val = new ExpireItem<TValue>();
                    val.ExpireTime = this.ExpireTime;
                    val.Async = this.Async;
                    if (OnExpire != null)
                    {
                        val.OnExpire += Val_OnExpire;
                    }
                    val.Item = factory();
                    // Store it
                    _map.Add(key, val);
                }
                else
                {
                    // Change it
                    val.Item = factory();
                }
                // Done
                return val.Item;
            }
            finally
            {
                _lock.ExitWriteLock();
                _state = false;
            }
        }


        public async Task<TValue> GetAsync(TKey key, Func<Task<TValue>> factory)
        {
            // Check cache
            //_lock.EnterReadLock();
            await AsyncLock.WaitAsync(-1);
            _state = true;
            IExpireItem<TValue> val;
            try
            {
                if (_map.TryGetValue(key, out val) && !val.Expire)
                    return val.Item;
            }
            finally
            {
                //_lock.ExitReadLock();
                AsyncLock.Release();
                _state = false;
            }


            // Cache it
            //_lock.EnterWriteLock();
            await AsyncLock.WaitAsync(-1);
            _state = true;
            try
            {
                // Check again
                if (_map.TryGetValue(key, out val) && !val.Expire)
                    return val.Item;



                if (val == null)
                {
                    // Create it
                    val = new ExpireItem<TValue>();
                    val.ExpireTime = this.ExpireTime;
                    val.Async = this.Async;
                    if (OnExpire != null)
                    {
                        val.OnExpire += Val_OnExpire;
                    }
                    val.Item = await factory();
                    // Store it
                    _map.Add(key, val);
                }
                else
                {
                    // Change it
                    val.Item = await factory();
                }
                // Done
                return val.Item;
            }
            finally
            {
                //_lock.ExitWriteLock();
                AsyncLock.Release();
                _state = false;
            }
        }

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
                IExpireItem<TValue> val;
                try
                {
                    _map.TryGetValue(key, out val);
                }
                finally
                {
                    _lock.ExitReadLock();
                }
                return val == null ? default(TValue) : val.Item;
            }
            set
            {
                AddOrUpdate(key, value);
            }
        }

        private void Val_OnExpire(object sender, ExpireEventArgs<TValue> e)
        {
            OnExpire(sender, e);
        }

        DateTime _lastTime = DateTime.MinValue;

        public event EventHandler<ExpireEventArgs<TValue>> OnExpire;

        /// <summary>
        /// 当清理数据过期的时候触发
        /// </summary>
        public event EventHandler<ClearExpireEventArgs<TValue>> OnClearExpire;

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

        public bool Update(TKey key, TValue value)
        {
            _lock.EnterReadLock();
            IExpireItem<TValue> val;
            try
            {
                if (!_map.TryGetValue(key, out val))
                    return false;
                val.ResetExpire();
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
            IExpireItem<TValue> val;
            try
            {
                if (_map.TryGetValue(key, out val))
                    return false;
                val = new ExpireItem<TValue>();
                val.ExpireTime = this.ExpireTime;
                val.Async = this.Async;
                if (OnExpire != null)
                {
                    val.OnExpire += Val_OnExpire;
                }
                val.Item = value;
                // Store it
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
            IExpireItem<TValue> val;
            try
            {
                if (_map.TryGetValue(key, out val))
                {
                    val.ResetExpire();
                    val.Item = value;
                }
                else
                {
                    val = new ExpireItem<TValue>();
                    val.ExpireTime = this.ExpireTime;
                    val.Async = this.Async;
                    if (OnExpire != null)
                    {
                        val.OnExpire += Val_OnExpire;
                    }
                    val.Item = value;
                    // Store it
                    _map.Add(key, val);
                }

            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public bool ContainsKey(TKey key)
        {
            _lock.EnterReadLock();
            IExpireItem<TValue> val;
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


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _map.Select(o => new KeyValuePair<TKey, TValue>(o.Key, o.Value.Item)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            //  return ((IEnumerable)_map).GetEnumerator();
            return GetEnumerator();
        }

        public void Dispose()
        {
            Flush();
            CacheFactory.RemoveCallback(_callback);
        }
    }
}
