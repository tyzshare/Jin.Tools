using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// 提供通用相等比较方法
    /// </summary>
    public static class GeneralEqualityComparer
    {
        /// <summary>
        /// 提供一个比较器
        /// </summary>
        public static GeneralEqualityComparer<T> On<T>(Func<T, object> keySelector)
        {
            return GeneralEqualityComparer<T>.On(keySelector);
        }
        /// <summary>
        /// 提供一个比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static GeneralEqualityComparer<T> On<T>(T obj, Func<T, object> keySelector)
        {
            return GeneralEqualityComparer<T>.On(keySelector);
        }

        /// <summary>
        /// 提供一个比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static GeneralEqualityComparer<T> OnIEnumerable<T>(IEnumerable<T> source, Func<T, object> keySelector)
        {
            return GeneralEqualityComparer<T>.On(keySelector);
        }
    }

    /// <summary>
    /// 提供通用相等比较方法
    /// </summary>
    public class GeneralEqualityComparer<T> : IEqualityComparer<T>
    {
        GeneralEqualityComparer()
        {

        }

        List<Func<T, object>> _keySelector = new List<Func<T, object>>();


        /// <summary>
        /// 提供一个比较器
        /// </summary>
        public static GeneralEqualityComparer<T> On(Func<T, object> keySelector)
        {
            return new GeneralEqualityComparer<T>().OnInternal(keySelector);
        }



        public GeneralEqualityComparer<T> And(Func<T, object> keySelector)
        {
            return this.OnInternal(keySelector);
        }

        internal GeneralEqualityComparer<T> OnInternal(Func<T, object> keySelector)
        {
            this._keySelector.Add(keySelector);
            return this;
        }

        public bool Equals(T x, T y)
        {
            foreach (var item in _keySelector)
            {
                if (!Equals(x, y, item))
                {
                    return false;
                }
            }
            return true;
        }

        bool Equals(T obj1, T obj2, Func<T, object> func)
        {
            return object.Equals(func(obj1), func(obj2));
        }

        public int GetHashCode(T obj)
        {
            int count = 0;
            foreach (var item in _keySelector)
            {
                var data = item(obj);
                if (data == null)
                {
                    continue;
                }
                count += data.GetHashCode();
            }
            return count;
        }
    }


}
