using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    internal class DataItem<T> : IDataItem<T>
    {
        public virtual T Item { get; set; }
    }
}
