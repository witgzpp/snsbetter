using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal
{
    /// <summary>
    /// 对敏感词表的封装
    /// </summary>
    [Serializable]
    public class Disable_word
    {
        private string id = string.Empty;
        private string content = string.Empty;
        private DateTime cdate;

        /// <summary>
        /// 表id
        /// </summary>
        public string Id 
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 敏感词内容
        /// </summary>
        public string Content 
        {
            get { return content; }
            set { content = value; }
        }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Cdate 
        {
            get { return cdate; }
            set { cdate = value; }

        }
    }
}
