namespace System
{
    /// <summary>
    /// 一个接口,支持数据
    /// </summary>
    internal interface IDataItem<T>
    {
        T Item { get; set; }
    }
}
