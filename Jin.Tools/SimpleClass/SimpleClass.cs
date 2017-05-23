using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 简单类：合适的场景代替匿名类使用
    /// </summary>
    public class SimpleClass
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
    }


    public class SimpleClass<T>
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        public T Value
        {
            get;
            set;
        }
    }
}
