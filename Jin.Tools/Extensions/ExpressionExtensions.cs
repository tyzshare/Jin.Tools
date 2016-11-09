using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    internal static class ExpressionExtensions
    {
        /// <summary>
        /// 对象转换为DataTable
        /// </summary>
        /// <remarks></remarks>
        public static Action<object, DataTable> GetConvertToDataTable(Type type)
        {



            ////处理字典类型
            //if (type.IsGenericType && type.GenericTypeArguments.Length == 2)
            //{
            //    var dicType = typeof(IDictionary<,>).MakeGenericType(type.GenericTypeArguments);
            //    if (dicType.IsAssignableFrom(type))
            //    {
            //        return GetConvertToDataTableByDictionary(type);
            //    }
            //}


            List<MemberBinding> bindings = new List<MemberBinding>();

            var typeTable = typeof(DataTable);

            var indexExpression = typeof(DataRow).GetProperty("Item", new Type[] { typeof(string) });



            ParameterExpression objectInstance = Expression.Parameter(typeof(object), "objectInstance");

            //ParameterExpression typeInstance = Expression.Parameter(type, "typeInstance");

            var typeInstance = Expression.Convert(objectInstance, type);

            ParameterExpression tableInstance = Expression.Parameter(typeTable, "rowInstance");

            var ps = type.GetProperties();

            var tableColumnsExpression = Expression.Property(tableInstance, typeof(DataTable).GetProperty("Columns"));

            List<Expression> list = new List<Expression>();


            foreach (var item in ps)
            {
                if (item.GetMethod == null)
                {
                    continue;
                }
                //处理列名
                var containsExpression = Expression.Call(tableColumnsExpression, typeof(DataColumnCollection).GetMethod("Contains", new Type[] { typeof(string) }), Expression.Constant(item.Name, typeof(string)));


                var addColumnExpression = Expression.Call(tableColumnsExpression, typeof(DataColumnCollection).GetMethod("Add", new Type[] { typeof(string), typeof(Type) }), Expression.Constant(item.Name, typeof(string)), Expression.Constant(item.PropertyType, typeof(Type)));

                var ifThen = Expression.IfThen(Expression.IsFalse(containsExpression), addColumnExpression);
                list.Add(ifThen);
            }

            //创建Row

            var rowVariable = Expression.Variable(typeof(DataRow));

            var rowAssign = Expression.Assign(rowVariable, Expression.Call(tableInstance, typeof(DataTable).GetMethod("NewRow")));

            //var rowInstance = Expression.Call(tableInstance, typeof(DataTable).GetMethod("NewRow"));
            list.Add(rowAssign);
            foreach (var item in ps)
            {
                if (item.GetMethod == null)
                {
                    continue;
                }

                var setExpression = Expression.MakeIndex(rowVariable, indexExpression, new Expression[] { Expression.Constant(item.Name, typeof(string)) });

                var propertyExpression = Expression.Property(typeInstance, item);
                //var setMethod = typeof(DataRow).GetProperty("Item", new Type[] { typeof(string) }).SetMethod;
                //var ex = Expression.Call(rowVariable, setMethod, Expression.Constant(item.Name, typeof(string)), Expression.Convert(propertyExpression, typeof(object)));
                var ex = Expression.Assign(setExpression, Expression.Convert(propertyExpression, typeof(object)));
                //var ex = Expression.Assign(setExpression, propertyExpression);
                list.Add(ex);
            }
            var rowExpression = Expression.Property(tableInstance, typeof(DataTable).GetProperty("Rows"));
            var rowAddExpression = Expression.Call(rowExpression, typeof(DataRowCollection).GetMethod("Add", new Type[] { typeof(DataRow) }), rowVariable);
            list.Add(rowAddExpression);
            var body = Expression.Block(new ParameterExpression[] { rowVariable }, list);

            return Expression.Lambda<Action<object, DataTable>>(body, objectInstance, tableInstance).Compile();
        }

    }
}
