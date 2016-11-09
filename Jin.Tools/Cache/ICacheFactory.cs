using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public interface ICacheFactory
    {


    }


    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class CacheFactoryExtensions
    {
        /// <summary>
        /// 创建一个内存缓存
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static ICache<TKey, TValue> CreateMemoryCache<TKey, TValue>(this ICacheFactory factory)
        {
            return new MemoryCache<TKey, TValue>();
        }

        /// <summary>
        /// 创建一个过期内存缓存
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static IExpireCache<TKey, TValue> CreateExpireMemoryCache<TKey, TValue>(this ICacheFactory factory)
        {
            return new ExpireMemoryCache<TKey, TValue>();
        }

        /// <summary>
        /// 创建一个过期缓存,并指定过期时间
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static IExpireCache<TKey, TValue> CreateExpireMemoryCache<TKey, TValue>(this ICacheFactory factory, TimeSpan expireTime)
        {
            var cache = factory.CreateExpireMemoryCache<TKey, TValue>();
            cache.ExpireTime = expireTime;
            return cache;
        }

    }
}
