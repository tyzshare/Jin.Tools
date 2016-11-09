using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace System
{

    /// <summary>
    /// 一个接口,表示缓存
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface ICache<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// 获取当前缓存的数量
        /// </summary>
        int Count { get; }

        IEnumerable<TKey> Keys { get; }

        /// <summary>
        /// 是否包含键
        /// </summary>
        bool ContainsKey(TKey key);

        /// <summary>
        /// 查询缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        TValue Get(TKey key, Func<TValue> factory);

        /// <summary>
        /// 查询缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<TValue> GetAsync(TKey key, Func<Task<TValue>> factory);

        /// <summary>
        /// 获取数据,没有返回默认值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        TValue this[TKey key] { get; set; }

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Flush();

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Update(TKey key, TValue value);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Add(TKey key, TValue value);

        /// <summary>
        /// 添加或更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        void AddOrUpdate(TKey key, TValue value);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Remove(TKey key);

    }
}
