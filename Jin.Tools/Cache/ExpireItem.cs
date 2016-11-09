using System.Threading.Tasks;

namespace System
{
    internal class ExpireItem<T> : DataItem<T>, IExpireItem<T>
    {
        public TimeSpan ExpireTime { get; set; }

        T _item;

        DateTime _lastTime;

        public event EventHandler<ExpireEventArgs<T>> OnExpire;

        T _oldItem;

        public override T Item
        {
            get { return _item; }
            set
            {
                _oldItem = _item;
                _item = value;
                _lastTime = DateTime.Now;
                _expire = false;
                ProcessExpire();
            }
        }
        /// <summary>
        /// 是否需要处理过期数据
        /// </summary>
        bool _processExpire = false;
        /// <summary>
        /// 数据是否过期
        /// </summary>
        bool _expire;

        public bool Expire
        {
            get
            {
                if (_expire)
                {
                    return _expire;
                }
                _expire = DateTime.Now - _lastTime > this.ExpireTime;
                if (_expire)
                {
                    _processExpire = true;
                }
                return _expire;
            }
        }

        void ProcessExpire()
        {
            if (!_processExpire)
            {
                return;
            }
            if (OnExpire != null)
            {
                var args = new ExpireEventArgs<T>(_oldItem, this.Item);
                if (Async)
                {
                    Task.Run(() => OnExpire(this, args));
                }
                else
                {
                    OnExpire(this, args);
                }
            }
            _processExpire = false;
            _oldItem = default(T);
        }

        public void ResetExpire()
        {
            _expire = DateTime.Now - _lastTime > this.ExpireTime;
        }

        public bool Async { get; set; }
    }
}
