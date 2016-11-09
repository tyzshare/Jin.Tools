using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class PageResultExtensions
    {
        /// <summary>
        /// 从<see cref="T:System.Collections.Generic.IEnumerable`1" />创建一个<see cref="T:System.IPageResult`1" />
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> GetPageData<TSource>(this IEnumerable<TSource> source, int pageIndex, int pageSize)
        {
            return source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 从<see cref="T:System.Collections.Generic.IEnumerable`1" />创建一个<see cref="T:System.IPageResult`1" />
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static IPageResult<TSource> ToPage<TSource>(this IEnumerable<TSource> source, int pageIndex, int pageSize)
        {
            int total = source.Select(o => 1).Count();
            return PageResult.Create(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(), pageIndex, pageSize, total);
        }

        /// <summary>
        /// 从<see cref="T:System.Collections.Generic.IEnumerable`1" />创建一个<see cref="T:System.IPageResult`1" />
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static IPageResult<TSource> ToPage<TSource>(this IEnumerable<TSource> source, int pageIndex, int pageSize, int total)
        {
            if (source is IList<TSource>)
            {
                return PageResult.Create(source as IList<TSource>, pageIndex, pageSize, total);
            }
            if (source is TSource[])
            {
                return PageResult.Create(source as TSource[], pageIndex, pageSize, total);
            }
            return PageResult.Create(source.ToList(), pageIndex, pageSize, total);
        }

    }
}
