namespace System
{
    /// <summary>
    /// 为过期数据提供事件参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExpireEventArgs<T> : EventArgs
    {
        public ExpireEventArgs(T old, T @new)
        {
            Old = old;
            New = @new;
        }
        /// <summary>
        /// 旧数据
        /// </summary>
        public T Old { get; private set; }
        /// <summary>
        /// 新数据
        /// </summary>
        public T New { get; private set; }

    }
}
