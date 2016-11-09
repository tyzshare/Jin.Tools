using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    internal interface IExpireCallback
    {
        object Key { get; }

        Action Callback { get; }
    }
}
