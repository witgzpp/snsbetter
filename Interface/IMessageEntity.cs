using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{

    /// <summary>
    /// 返回给前端的信息模型
    /// </summary>
    public interface IMessageEntity
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        bool Msgflag { get; set; }
        /// <summary>
        /// 操作返回的信息
        /// </summary>
        object Msgvalue { get; set; }
    }
}
