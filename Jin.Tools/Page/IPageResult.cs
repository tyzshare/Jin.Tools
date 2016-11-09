using System.Collections;
using System.Collections.Generic;
namespace System
{
    /// <summary>
    /// 一个接口,表示一个分页结果集
    /// </summary>
    public interface IPageResult : IEnumerable
    {
        /// <summary>
        /// 获取当前页码
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 每页显示数
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 获取数据总数
        /// </summary>
        int TotalCount { get; }
        /// <summary>
        /// 获取分页总数
        /// </summary>
        int PageCount { get; }
        /// <summary>
        /// 获取当前集合的数量
        /// </summary>
        int ItemCount { get; }
    }

    /// <summary>
    /// 一个接口,表示一个分页结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPageResult<out T> : IPageResult, IEnumerable, IEnumerable<T>
    {

    }
}
