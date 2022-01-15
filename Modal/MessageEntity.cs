using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Modal
{
    /// <summary>
    /// 返回给前端的信息模型
    /// </summary>
    [Serializable]
    public class MessageEntity:IMessageEntity
    {
        private bool msgFlag;
        private object msgValue;

        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Msgflag 
        {
            get { return msgFlag; }
            set 
            {
                msgFlag = value;
            }
        }

        /// <summary>
        /// 操作返回的信息
        /// </summary>
        public object Msgvalue 
        {
            get { return msgValue; }
            set { msgValue = value; }
        }


       
    }
}
