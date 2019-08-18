using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using Modal;
using MySql.Data.MySqlClient;
using Common;

namespace Logic
{
    public class UserLogic:BaseLogic
    {
        public IMessageEntity AddUser(User user) 
        {
            //uname、upwd、sex、cdate、email
            IMessageEntity msg = new MessageEntity();
            user.UPwd = Md5Helper.GetMd5(user.UPwd, 16);
            
            
            
          
           
            
            user.CDate = DateTime.Now;

            string sql = "insert into user(uname,upwd,rname,age,sex,des,cdate,tel,email) values(@uname,@upwd,@rname,@age,@sex,@des,@cdate,@tel,@email)";

            MySqlParameter[] pms = new MySqlParameter[9];
            pms[0] = new MySqlParameter("@uname", MySqlDbType.String) { Value=user.UName};
            pms[1] = new MySqlParameter("@upwd", MySqlDbType.String) { Value = user.UPwd };
            pms[2] = new MySqlParameter("@rname", MySqlDbType.String) { Value = user.RName };
            pms[3] = new MySqlParameter("@age", MySqlDbType.Int32) { Value = user.Age };
            pms[4] = new MySqlParameter("@sex", MySqlDbType.String) { Value = user.Sex };
            pms[5] = new MySqlParameter("@des", MySqlDbType.String) { Value = user.Des };
            pms[6] = new MySqlParameter("@cdate", MySqlDbType.DateTime) { Value = user.CDate };
            pms[7] = new MySqlParameter("@tel", MySqlDbType.String) { Value = user.Tel };
            pms[8] = new MySqlParameter("@email", MySqlDbType.String) { Value = user.Email };


            if (!string.IsNullOrEmpty(user.UName) && !string.IsNullOrEmpty(user.UPwd))
            {
                return GetInt(sql, pms);
            }
            else
            {
                return msg;
            }
            
            

            
        }

        /// <summary>
        /// 根据用户id查询用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public IMessageEntity SelectUser(int id) 
        {
            IMessageEntity msg = new MessageEntity();
            msg.Msgflag = false;
            if (string.IsNullOrEmpty(id.ToString())) 
            {
                msg.Msgvalue = "参数有误";
                return msg;
            }


            string sql = "select * from user where id=@id";
            MySqlParameter pms = new MySqlParameter("@id", MySqlDbType.Int32) { Value=id};

            msg = GetInfo(sql, pms);
            User use = new User();
            using (MySqlDataReader reader = (MySqlDataReader)msg.Msgvalue) 
            {
                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        use.Id = Convert.ToInt32(reader["id"]);
                        use.UName = Convert.ToString(reader["uname"]);
                        use.UPwd = Convert.ToString(reader["upwd"]);
                        use.RName = Convert.ToString(reader["rname"] == System.DBNull.Value ? "" : reader["rname"]);
                        use.Age = Convert.ToInt32(reader["age"] == System.DBNull.Value ? 0 : reader["age"]);
                        use.Des = Convert.ToString(reader["des"] == System.DBNull.Value ? "" : reader["des"]);
                        use.Sex = Convert.ToString(reader["sex"]);
                        use.CDate = Convert.ToDateTime(reader["cdate"]);
                        use.Tel = Convert.ToString(reader["tel"] == System.DBNull.Value ? "" : reader["tel"]);
                        use.Email = Convert.ToString(reader["email"] == System.DBNull.Value ? "" : reader["email"]);
                    }
                }
            }
            msg.Msgvalue = use;

            return msg;
        }


        

        /// <summary>
        /// 根据用户名查询用户信息
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public IMessageEntity SelectUser(string name) 
        {
            IMessageEntity msg = new MessageEntity();
            msg.Msgflag = false;
            if (string.IsNullOrEmpty(name)) 
            {
                msg.Msgvalue = "参数有误";
                return msg;
            }


            string sql = "select * from user where uname=@uname";
            MySqlParameter pms = new MySqlParameter("@uname", MySqlDbType.String) { Value = name };

            msg = GetInfo(sql, pms);
            User use = new User();
            using (MySqlDataReader reader = (MySqlDataReader)msg.Msgvalue) 
            {
                if (reader.HasRows) 
                {
                    while (reader.Read()) 
                    {
                        use.Id = Convert.ToInt32(reader["id"]);
                        use.UName = Convert.ToString(reader["uname"]);
                        use.UPwd = Convert.ToString(reader["upwd"]);
                        use.RName = Convert.ToString(reader["rname"] == System.DBNull.Value ? "" : reader["rname"]);
                        use.Age = Convert.ToInt32(reader["age"] == System.DBNull.Value ? 0 : reader["age"]);
                        use.Des = Convert.ToString(reader["des"] == System.DBNull.Value ? "" : reader["des"]);
                        use.Sex = Convert.ToString(reader["sex"]);
                        use.CDate = Convert.ToDateTime(reader["cdate"]);
                        use.Tel = Convert.ToString(reader["tel"] == System.DBNull.Value ? "" : reader["tel"]);
                        use.Email = Convert.ToString(reader["email"] == System.DBNull.Value ? "" : reader["email"]);
                    }
                }
            }

            msg.Msgvalue = use;


            return msg;
        }

        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <returns>msgFlag=true； 内容为msgValue</returns>
        public IMessageEntity SelectUser() 
        {
            string sql = "select * from user";
            IMessageEntity msg=new MessageEntity();
            msg=GetInfo(sql,null);
            List<User> listUser = new List<User>();
            using (MySqlDataReader reader = (MySqlDataReader)msg.Msgvalue) 
            {
                if (reader.HasRows)
                {
                    while (reader.Read()) 
                    {
                        User use = new User();
                        use.Id = Convert.ToInt32(reader["id"]);
                        use.UName = Convert.ToString(reader["uname"]);
                        use.UPwd = Convert.ToString(reader["upwd"]);
                        use.RName = Convert.ToString(reader["rname"] == System.DBNull.Value ? "" : reader["rname"]);
                        use.Age = Convert.ToInt32(reader["age"] == System.DBNull.Value ? 0 : reader["age"]);
                        use.Des = Convert.ToString(reader["des"] == System.DBNull.Value ? "" : reader["des"]);
                        use.Sex = Convert.ToString(reader["sex"]);
                        use.CDate = Convert.ToDateTime(reader["cdate"]);
                        use.Tel = Convert.ToString(reader["tel"] == System.DBNull.Value ? "" : reader["tel"]);
                        use.Email = Convert.ToString(reader["email"] == System.DBNull.Value ? "" : reader["email"]);
                        use.UDate = Convert.ToDateTime(reader["udate"] == System.DBNull.Value ? DateTime.Now : reader["udate"]);
                        listUser.Add(use);
                    }
                }
            }

            msg.Msgvalue = listUser;

            return msg;
        }


        /// <summary>
        /// 根据邮箱查询用户是否存在
        /// </summary>
        /// <param name="str">用户名/邮箱</param>
        /// <param name="flag">1：用户名，2：邮箱</param>
        /// <returns></returns>
        public IMessageEntity IsUserExist(string str,int flag) 
        {
            IMessageEntity msg=new MessageEntity();
            msg.Msgflag = false;
            if (string.IsNullOrEmpty(str))
            {
                msg.Msgvalue = "参数有误";
                return msg;
            }
            else
            {
                if (flag == 2)
                {
                    IMessageEntity msgflag1 = new MessageEntity();

                    string sql1 = "select count(1) from user where email=@email";
                    MySqlParameter pms1 = new MySqlParameter("@email", MySqlDbType.String) { Value = str };
                    msgflag1 = GetOne(sql1, pms1);

                    msg.Msgflag = msgflag1.Msgflag && Convert.ToInt32(msgflag1.Msgvalue) >= 1;
                }
                if (flag == 1)
                {
                    IMessageEntity msgflag2 = new MessageEntity();
                    string sql2 = "select count(1) from user where uname=@uname";
                    MySqlParameter pms2 = new MySqlParameter("@uname", MySqlDbType.String) { Value = str };
                    msgflag2=GetOne(sql2,pms2);
                    msg.Msgflag = msgflag2.Msgflag && Convert.ToInt32(msgflag2.Msgvalue) >= 1;

                }
            }

            

            return msg;
            
            
        }


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public IMessageEntity UpdateUser(int id, User user) 
        {
            IMessageEntity msg = new MessageEntity();
            msg.Msgflag = false;
            if (string.IsNullOrEmpty(id.ToString()) || user == null)
            {
                return msg;
            }
            string sql = "update user set upwd=@upwd,rname=@rname,age=@age,sex=@sex,des=@des,udate=@udate,tel=@tel,email=@email where id=@id";

            MySqlParameter[] pms = new MySqlParameter[9];
            user.UDate = DateTime.Now;
            user.UPwd = Md5Helper.GetMd5(user.UPwd, 16);

            pms[0] = new MySqlParameter("@upwd", MySqlDbType.String) { Value = user.UPwd };
            pms[1] = new MySqlParameter("@rname", MySqlDbType.String) { Value = user.RName };
            pms[2] = new MySqlParameter("@age", MySqlDbType.Int32) { Value = user.Age };
            pms[3] = new MySqlParameter("@sex", MySqlDbType.String) { Value = user.Sex };
            pms[4] = new MySqlParameter("@des", MySqlDbType.String) { Value = user.Des };
            pms[5] = new MySqlParameter("@udate", MySqlDbType.DateTime) { Value = user.UDate };
            pms[6] = new MySqlParameter("@tel", MySqlDbType.String) { Value = user.Tel };
            pms[7] = new MySqlParameter("@email", MySqlDbType.String) { Value = user.Email };

            pms[8] = new MySqlParameter("@id", MySqlDbType.Int32) { Value = id };

            return GetInt(sql, pms);

            

        }

        


        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public IMessageEntity IsUserExist(string name,string pwd) 
        {



            string sql = "select COUNT(1) from user where uname=@uname and upwd=@upwd ";
            MySqlParameter[] pms = new MySqlParameter[2];
            pms[0] = new MySqlParameter("@uname", MySqlDbType.String) { Value = name };
            pms[1] = new MySqlParameter("@upwd", MySqlDbType.String) { Value = pwd };
            IMessageEntity msg = new MessageEntity();
            msg.Msgflag = false;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pwd))
            {
                IMessageEntity msgflag = new MessageEntity();

                msgflag = GetOne(sql, pms);

                if (IsUserExist(name,1).Msgflag)
                {
                    if (msgflag.Msgflag && Convert.ToInt32(msgflag.Msgvalue)>=1)
                    {
                        msg.Msgvalue = "登录成功";
                    }
                    else
                    {
                        msg.Msgvalue = "密码错误";
                    }
                }
                else
                {
                    msg.Msgvalue = "用户不存在";
                }
            }
            else 
            {
                msg.Msgvalue = "参数有误";
            }
            return msg;
        }



        /// <summary>
        /// 根据邮箱更新密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public IMessageEntity UpdateUser(string email, string pwd) 
        {
            IMessageEntity msg = new MessageEntity();
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(pwd))
            {
                pwd = Md5Helper.GetMd5(pwd, 16);

                string sql = @"update user set upwd=@upwd where email=@email ";
                MySqlParameter[] pms = new MySqlParameter[2];
                pms[0] = new MySqlParameter("@upwd", MySqlDbType.String) { Value = pwd };
                pms[1] = new MySqlParameter("@email", MySqlDbType.String) { Value = email };

                msg = GetInt(sql, pms);
            }


            return msg;
        }



        /// <summary>
        /// 根据用户id查找用户名
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>返回的用户名存储在msgvalue中</returns>
        public IMessageEntity SelectUserName(int id)
        {
            
            string sql = "select uname from user where id=@id";
            MySqlParameter pms = new MySqlParameter("@id", MySqlDbType.Int32) { Value = id };
            return GetOne(sql, pms);
            
        }


        /// <summary>
        /// 根据用户名查找用户id
        /// </summary>
        /// <param name="uname">用户名</param>
        /// <returns>返回的用户名存储在msgvalue中</returns>
        public IMessageEntity SelectUserId(string uname)
        {
            string sql = "select id from user where uname=@uname";
            MySqlParameter pms = new MySqlParameter("@uname", MySqlDbType.String) { Value = uname };
            return GetOne(sql, pms);
        }








        




    }
}
