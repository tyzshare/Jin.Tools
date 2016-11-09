namespace System
{
    /// <summary>
    /// 为清理过期数据提供事件参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClearExpireEventArgs<T> : EventArgs
    {
        public ClearExpireEventArgs(T item)
        {
            Item = item;
        }
        /// <summary>
        /// 过期数据
        /// </summary>
        public T Item { get; private set; }

    }
}
