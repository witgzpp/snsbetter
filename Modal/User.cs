using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal
{
    
    /// <summary>
    /// 对user表的封装
    /// </summary>
    [Serializable]  
    public class User
    {
        //id,Uname,Upwd,rname,age,sex,des,cdate,tel,email
        private string id;        
        private string uname;
        private string upwd;
        private string rname;
        private int age;
        private string sex;
        private string des;
        private DateTime cdate;
        private string tel;
        private string email;
        private DateTime udate = DateTime.Now;

       
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 用户名(登录名)
        /// </summary>
        public string Uname 
        {
            get { return uname; }
            set { uname = value; }
        }

        /// <summary>
        /// 登录密码
        /// </summary>

        public string Upwd
        {
            get { return upwd; }
            set { upwd = value; }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>

        public string Rname 
        {
            get { return rname; }
            set { rname = value; }
        }


        /// <summary>
        /// 用户年龄
        /// </summary>
        public int Age 
        {
            get { return age; }
            set { age = value; }
        }

        /// <summary>
        /// 用户性别
        /// </summary>
        public string Sex 
        {
            get { return sex; }
            set { sex = value; }
        }

        /// <summary>
        /// 个人描述
        /// </summary>
        public string Des 
        {
            get { return des; }
            set { des = value; }
        }

       

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime Cdate 
        {
            get { return cdate; }
            set { cdate = value; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Udate
        {
            get { return udate; }
            set
            {
                udate = value; 
            }
        }

        /// <summary>
        /// 用户手机
        /// </summary>
        public string Tel 
        {
            get { return tel; }
            set { tel = value; }
        }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email 
        {
            get { return email; }
            set { email = value; }
        }

       
        
        
    }
    
}
