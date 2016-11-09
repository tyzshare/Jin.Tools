using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> IfWhere<T>(this IQueryable<T> source, Func<bool> ifPredicate, Expression<Func<T, bool>> predicate)
        {
            if (ifPredicate())
            {
                source = source.Where(predicate);
            }
            return source;
        }

        public static IQueryable<T> IfWhere<T>(this IQueryable<T> source, bool ifPredicate, Expression<Func<T, bool>> predicate)
        {
            if (ifPredicate)
            {
                source = source.Where(predicate);
            }
            return source;
        }

        public static IQueryable<T> IfElseWhere<T>(this IQueryable<T> source, Func<bool> ifPredicate, Expression<Func<T, bool>> truePredicate, Expression<Func<T, bool>> falsePredicate)
        {
            if (ifPredicate())
            {
                source = source.Where(truePredicate);
            }
            else
            {
                source = source.Where(falsePredicate);
            }
            return source;
        }

        public static IQueryable<T> IfElseWhere<T>(this IQueryable<T> source, bool ifPredicate, Expression<Func<T, bool>> truePredicate, Expression<Func<T, bool>> falsePredicate)
        {
            if (ifPredicate)
            {
                source = source.Where(truePredicate);
            }
            else
            {
                source = source.Where(falsePredicate);
            }
            return source;
        }

        public static IQueryable<TSource> DistinctBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            if (source == null)
            {
                return source;
            }
            return source.GroupBy(keySelector).Select(x => x.FirstOrDefault());
        }

    }
}
