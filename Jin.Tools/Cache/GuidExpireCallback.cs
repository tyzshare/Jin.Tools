using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    internal sealed class GuidExpireCallback : IExpireCallback
    {
        GuidExpireCallback() { }

        public Action Callback { get; private set; }

        public object Key { get; private set; }


        public static GuidExpireCallback Create(Action callback)
        {
            return new GuidExpireCallback()
            {
                Callback = callback,
                Key = Guid.NewGuid().ToString()
            };
        }
    }
}
