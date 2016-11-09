namespace System
{
    /// <summary>
    /// 一个接口,支持过期数据
    /// </summary>
    internal interface IExpireItem<T> : IDataItem<T>
    {
        TimeSpan ExpireTime { get; set; }
        /// <summary>
        /// 获取一个指,指示数据是否过期
        /// </summary>
        /// <returns></returns>
        bool Expire { get; }
        /// <summary>
        /// 当数据过期的时候触发
        /// </summary>
        event EventHandler<ExpireEventArgs<T>> OnExpire;

        bool Async { get; set; }

        void ResetExpire();

    }
}
