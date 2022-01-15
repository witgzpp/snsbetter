using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    /// <summary>
    /// 对user_log表的封装
    /// </summary>
    [Serializable]
    public class User_log
    {
        //id,content,cdate,uid
        private int id;
        private string content;
        private DateTime cdate;
        private int uid;

        /// <summary>
        /// 用户日志表id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CDate
        {
            get { return cdate; }
            set { cdate = value; }
        }

        /// <summary>
        /// 针对哪个用户的操作
        /// </summary>
        public int UId
        {
            get { return uid; }
            set { uid = value; }
        }
    }
}