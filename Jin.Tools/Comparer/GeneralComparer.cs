using System.Collections.Generic;

namespace System
{

    /// <summary>
    /// 提供通用相等比较方法
    /// </summary>
    public static class GeneralComparer
    {
        /// <summary>
        /// 提供通用相等比较方法
        /// </summary>
        public static GeneralComparer<T> On<T>(Func<T, T, int> keySelector)
        {
            return GeneralComparer<T>.On(keySelector);
        }

        /// <summary>
        /// 提供通用相等比较方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static GeneralComparer<T> On<T>(T obj, Func<T, T, int> keySelector)
        {
            return GeneralComparer<T>.On(keySelector);
        }

        /// <summary>
        /// 提供通用相等比较方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static GeneralComparer<T> OnIEnumerable<T>(IEnumerable<T> source, Func<T, T, int> keySelector)
        {
            return GeneralComparer<T>.On(keySelector);
        }


        /// <summary>
        /// 提供通用相等比较方法
        /// </summary>
        public static GeneralComparer<dynamic> On(Func<dynamic, dynamic, int> keySelector)
        {
            return GeneralComparer<dynamic>.On(keySelector);
        }
    }

    /// <summary>
    /// 提供通用相等比较方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public sealed class GeneralComparer<T> : Comparer<T>, IComparer<T>
    {
        internal GeneralComparer() { }

        List<Func<T, T, int>> _keySelector = new List<Func<T, T, int>>();


        bool _isValueType { get; set; }


        /// <summary>
        ///提供通用比较方法
        /// </summary>
        internal GeneralComparer<T> OnInternal(Func<T, T, int> keySelector)
        {
            this._keySelector.Add(keySelector);
            return this;
        }

        /// <summary>
        /// 提供一个比较器
        /// </summary>
        public static GeneralComparer<T> On(Func<T, T, int> keySelector)
        {
            return new GeneralComparer<T>().OnInternal(keySelector);
        }

        public GeneralComparer<T> And(Func<T, T, int> keySelector)
        {
            return OnInternal(keySelector);
        }

        public override int Compare(T x, T y)
        {
            foreach (var item in _keySelector)
            {
                int result = item(x, y);
                if (result != 0)
                {
                    continue;
                }
                return result;
            }
            return 0;
        }

    }


}
