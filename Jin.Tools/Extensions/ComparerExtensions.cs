using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ComparerExtensions
    {
        public static GeneralComparer<T> CreateGeneralComparer<T>(this T obj, Func<T, T, int> keySelector)
        {
            return GeneralComparer.On<T>(keySelector);
        }


        public static GeneralEqualityComparer<T> CreateGeneralEqualityComparer<T>(this T obj, Func<T, object> keySelector)
        {
            return GeneralEqualityComparer.On<T>(keySelector);
        }

    }
}
