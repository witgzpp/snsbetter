using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 所有后台表名称枚举类型
    /// </summary>
    public enum AllInfo
    {
        /// <summary>
        /// 用户信息表
        /// </summary>
        User,
        /// <summary>
        /// 用户talk表
        /// </summary>
        Talk,
        /// <summary>
        /// 用户评论表
        /// </summary>
        Comment,
        /// <summary>
        /// 用户日志表
        /// </summary>
        User_log,
        /// <summary>
        /// 用户错误操作表
        /// </summary>
        Log_error,

        /// <summary>
        /// 敏感词表
        /// </summary>
        Disable_word
    }
}
