using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal
{
    /// <summary>
    /// 对utalk表的封装
    /// </summary>

    [Serializable]
    public class UTalk
    {
        //id,oldcontent,newcontent,talkid,cdate,updateuname
        private int id;
        private string oldcontent;
        private string newcontent;
        private int talkid;
        private DateTime cdate;
        private string updateuname;


        /// <summary>
        /// 主键
        /// </summary>
        public int Id{
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 之前内容
        /// </summary>
        public string Oldcontent 
        {
            get { return oldcontent; }
            set { oldcontent = value; }
        }

        /// <summary>
        /// 用户修改后的内容
        /// </summary>
        public string Newcontent 
        {
            get { return newcontent; }
            set { newcontent = value; }
        }

        /// <summary>
        /// talk的id
        /// </summary>
        public int Talkid 
        {
            get { return talkid; }
            set { talkid = value; }
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
        /// 修改人用户名
        /// </summary>
        public string Updateuname 
        {
            get { return updateuname; }
            set { updateuname = value; }
        }
        
    }
}
