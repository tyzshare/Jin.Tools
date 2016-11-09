using System.Collections.Generic;
using System.Linq;

namespace System
{

    internal class PageResult
    {
        /// <summary>
        /// 创建一个IPageResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalItems"></param>
        /// <returns></returns>
        public static IPageResult<T> Create<T>(ICollection<T> items, int pageIndex, int pageSize, int totalCount)
        {
            return new PageResult<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = items
            };
        }
    }

    /// <summary>
    /// 表示一个包含分页结果的集合
    /// </summary>
    internal class PageResult<T> : PageResult, IPageResult<T>
    {
        /// <summary>
        /// 获取分页的集合
        /// </summary>
        public IEnumerable<T> Items { get; internal set; }
        /// <summary>
        /// 获取当前页码
        /// </summary>
        public int PageIndex { get; internal set; }
        /// <summary>
        /// 每页显示数
        /// </summary>
        public int PageSize { get; internal set; }
        /// <summary>
        /// 获取数据总数
        /// </summary>
        public int TotalCount { get; internal set; }
        /// <summary>
        /// 获取分页总数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (this.PageSize == 0)
                {
                    return 0;
                }
                return (int)Math.Max((this.TotalCount + this.PageSize - 1) / this.PageSize, 1);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)this.Items).GetEnumerator();
        }


        public int ItemCount
        {
            get { return this.Items.Count(); }
        }

    }
}
