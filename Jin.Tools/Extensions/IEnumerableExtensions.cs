using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class IEnumerableExtensions
    {

        static readonly ICache<Type, Action<Object, DataTable>> _objToDataTableFactory = CacheFactory.Instance.CreateMemoryCache<Type, Action<Object, DataTable>>();

        /// <summary>
        /// 将集合转换成<see cref="System.Data.DataTable" />
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            DataTable table = new DataTable();

            foreach (var item in source)
            {
                if (item == null)
                {
                    continue;
                }
                var type = item.GetType();
                var factory = _objToDataTableFactory.Get(type, () =>
                {
                    return ExpressionExtensions.GetConvertToDataTable(type);
                });
                factory(item, table);
            }
            return table;
        }

        /// <summary>
        /// 将集合转换成<see cref="System.Data.DataTable" />,并指定表名
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IEnumerable source, string tableName)
        {
            var table = ToDataTable(source);
            table.TableName = tableName;
            return table;
        }

        /// <summary>
        /// 将集合转换成<see cref="System.Data.DataTable" />
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<TKey, TValue>(this IEnumerable<IDictionary<TKey, TValue>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            DataTable table = new DataTable();

            //先这样
            foreach (var item in source)
            {
                if (item == null)
                {
                    continue;
                }
                foreach (var keyValue in item)
                {
                    if (!table.Columns.Contains(keyValue.Key.ToString()))
                    {
                        var type = keyValue.Value == null ? typeof(object) : keyValue.Value.GetType();
                        table.Columns.Add(keyValue.Key.ToString(), type);
                    }
                }
                var row = table.NewRow();
                foreach (var keyValue in item)
                {
                    row[keyValue.Key.ToString()] = keyValue.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// 将集合转换成<see cref="System.Data.DataTable" />,并指定表名
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<TKey, TValue>(this IEnumerable<IDictionary<TKey, TValue>> source, string tableName)
        {
            var table = ToDataTable(source);
            table.TableName = tableName;
            return table;
        }

    }
}
