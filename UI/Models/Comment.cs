using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    /// <summary>
    /// 对comment表的封装
    /// </summary>
    [Serializable]
    public class Comment
    {
        //id,content,cdate,display,uid,tid
        private int id;

        private string content;

        private DateTime cdate;
        private int display;
        private int uid;
        private int tid;



        /// <summary>
        /// 评论Id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }



        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }


        /// <summary>
        /// 评论创建时间
        /// </summary>
        public DateTime CDate
        {
            get { return cdate; }
            set { cdate = value; }
        }

        /// <summary>
        /// 评论是否显示
        /// </summary>
        public int Display
        {
            get { return display; }
            set { display = value; }
        }



        /// <summary>
        /// 用户id，外键（与用户表的主键关联）
        /// </summary>
        public int UId
        {
            get { return uid; }
            set { uid = value; }
        }


        /// <summary>
        /// 说说id，外键（与说说表的主键关联）
        /// </summary>
        public int TId
        {
            get { return tid; }
            set { tid = value; }
        }
    }
}