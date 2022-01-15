using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    /// <summary>
    /// 对talk表的封装
    /// </summary>

    [Serializable]
    public class Talk
    {
        //id,theme,title,value,cdate,udate,uid
        private int id;
        private string theme;
        private string title;
        private string content;

        private DateTime cdate;
        private DateTime udate;
        List<Comment> comments = new List<Comment>();

        private int uid;

        /// <summary>
        /// 说说id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        /// <summary>
        /// 主题
        /// </summary>
        public string Theme
        {
            get { return theme; }
            set { theme = value; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
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
        /// 更新时间
        /// </summary>
        public DateTime UDate
        {
            get { return udate; }
            set { udate = value; }
        }

        /// <summary>
        /// 所有评论
        /// </summary>
        public List<Comment> Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        /// <summary>
        /// 用户id，外键（与用户表的主键关联）
        /// </summary>

        public int UId
        {
            get { return uid; }
            set { uid = value; }
        }


    }
}