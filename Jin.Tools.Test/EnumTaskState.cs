using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jin.Tools.Test
{
    /// <summary>
    /// 任务状态
    /// </summary>
    //public enum EnumTaskState
    //{
    //    [EnumDescription(DefaultDescription = "未完成")]
    //    Unfinished = 1,
    //    [EnumDescription(DefaultDescription = "已完成")]
    //    Finished = 2,
    //    [EnumDescription(DefaultDescription = "已启用")]
    //    Enabled = 3
    //}

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum EnumGender
    {
        [Description("男")]
        Man = 1,
        [Description("女")]
        Woman = 2,
        [Description("未知")]
        UnKnow = 3
    }
}
