using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal
{
    /// <summary>
    /// 对talk表的封装
    /// </summary>

    [Serializable]
    public class Talk
    {
        //id,theme,title,value,cdate,udate,uid
        private int id;
        private string theme=string.Empty;
        private string title=string.Empty;
        private string content=string.Empty;
        private DateTime cdate = DateTime.Now;
        private DateTime udate = DateTime.Now;
        List<Comment> comments = new List<Comment>();
        private int uid;
        private string uname=string.Empty;
        private string unames=string.Empty;
        private int updateid;

        private string cname=string.Empty;

        private int display;


        /// <summary>
        /// 该talk是否显示
        /// </summary>
        public int Display 
        {
            get { return display; }
            set { display = value; }
        }

        /// <summary>
        /// 发布用户名
        /// </summary>
        public string CName 
        {
            get { return cname; }
            set { cname = value; }
        }


        /// <summary>
        /// 最后一次修改人id
        /// </summary>
        public int Updateid 
        {
            get { return updateid; }
            set { updateid = value; }
        }

        /// <summary>
        /// 所有修改人列表，用;分割
        /// </summary>
        public string UNames 
        {
            get 
            {
                return unames;
            }
            set 
            {
                unames = value;
            }
        }



        /// <summary>
        /// 最后一次修改人
        /// </summary>
        public string UName 
        {
            get 
            {
                return uname;
            }
            set 
            {
                uname = value;
            }
        }


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
        /// 最后一次更新时间
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
