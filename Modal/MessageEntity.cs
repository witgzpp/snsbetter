using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Modal
{
    /// <summary>
    /// 对messageentity的封装
    /// </summary>
    [Serializable]
    public class MessageEntity:IMessageEntity
    {
        private bool msgFlag;
        private object msgValue;


        public bool Msgflag 
        {
            get { return msgFlag; }
            set 
            {
                msgFlag = value;
            }
        }

        public object Msgvalue 
        {
            get { return msgValue; }
            set { msgValue = value; }
        }



        public object MsgValue
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
