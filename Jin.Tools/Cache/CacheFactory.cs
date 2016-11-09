using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System
{

    /// <summary>
    /// 缓存工厂
    /// </summary>
    public sealed class CacheFactory : ICacheFactory
    {
        public static readonly ICacheFactory Instance = new CacheFactory();


        static ConcurrentDictionary<object, Action> _callbacks;
        static readonly Timer _timer;
        static object lockObj = new object();

        static CacheFactory()
        {
            _expireTime = 60;
            _callbacks = new ConcurrentDictionary<object, Action>();
            _timer = new Timer(o =>
            {
                var actions = o as ConcurrentDictionary<object, Action>;
                lock (lockObj)
                {
                    foreach (var item in actions)
                    {
                        try
                        {
                            item.Value();
                        }
                        catch
                        {
                        }
                    }
                }
            }, _callbacks, Timeout.Infinite, Timeout.Infinite);
        }

        static int _expireTime;
        /// <summary>
        /// 获取或设置过期时间
        /// </summary>
        public static int ExpireTime
        {
            get { return _expireTime; }
            set
            {
                _expireTime = value;
                ResetTimer();
            }
        }


        static void ResetTimer()
        {
            if (_callbacks.Count == 0)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            else
            {
                int time = 1000 * 60 * _expireTime;
                _timer.Change(time, time);
            }
        }


        internal static void AddCallback(IExpireCallback callback)
        {
            lock (lockObj)
            {
                int count = _callbacks.Count;
                _callbacks.AddOrUpdate(callback.Key, callback.Callback, (k, a) => callback.Callback);
                if (count == 0 && _callbacks.Count > count)
                {
                    ResetTimer();
                }
            }
        }


        internal static void RemoveCallback(IExpireCallback callback)
        {
            lock (lockObj)
            {
                int count = _callbacks.Count;
                Action tempCallback;
                _callbacks.TryRemove(callback.Key, out tempCallback);
                if (count > 0 && _callbacks.Count == 0)
                {
                    ResetTimer();
                }
            }
        }

        /// <summary>
        /// 创建一个缓存
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        [Obsolete("Use CacheFactory.Instance.CreateMemoryCache")]

        public static ICache<TKey, TValue> CreateCache<TKey, TValue>()
        {
            return Instance.CreateMemoryCache<TKey, TValue>();
        }

        /// <summary>
        /// 创建一个过期缓存
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        [Obsolete("Use CacheFactory.Instance.CreateExpireMemoryCache")]
        public static IExpireCache<TKey, TValue> CreateExpireCache<TKey, TValue>()
        {
            return Instance.CreateExpireMemoryCache<TKey, TValue>();
        }
    }
}
