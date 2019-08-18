using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using MySql.Data;
using MySql.Data.MySqlClient;
using Dao;
using Service;
using Modal;
using System.Data;
using Common;

namespace Logic
{
    public class BaseLogic:IBaseLogic
    {

        
        private BaseDao dao=null;
        private BaseService service=null;

        /// <summary>
        /// 增、删、改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pms">参数</param>
        /// <returns>返回IMessageEntity</returns>
        public IMessageEntity GetInt(string sql, params MySqlParameter[] pms) 
        {
            IMessageEntity msg = new MessageEntity();
            msg.Msgflag = false;
            
            try
            {
                dao = new BaseDao();

                if (dao.GetInt(sql, pms) >= 1)
                {
                    msg.Msgvalue = "ok";
                    msg.Msgflag = true;
                }
                else
                {
                    msg.Msgflag = false;
                    msg.Msgvalue = "操作失败";
                }

                
            }
            catch(Exception ex)
            {

                msg.Msgvalue = "出现异常："+ex.Message;
            }

            return msg;
        }

        /// <summary>
        /// 查询大量数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pms">参数</param>
        /// <returns></returns>
        public IMessageEntity GetInfo(string sql,params MySqlParameter[] pms)
        {
            IMessageEntity msg = new MessageEntity();
            msg.Msgflag = false;
            service = new BaseService();
            MySqlDataReader reader = null;
            try
            {
                reader = service.GetInfo(sql, pms);
                if (reader != null || !reader.Equals(null))
                {
                    msg.Msgflag = true;
                    msg.Msgvalue = reader;
                }
                else
                {
                    
                    msg.Msgvalue = "空数据";
                }
                
            }
            catch(Exception ex)
            {
                msg.Msgvalue = "出现异常：" + ex.Message;
            }


            
            return msg;
        }

        /// <summary>
        /// 查询一个数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pms">参数</param>
        /// <returns></returns>
        public IMessageEntity GetOne(string sql, params MySqlParameter[] pms) 
        {
            IMessageEntity msg = new MessageEntity();
            msg.Msgflag = false;
            service = new BaseService();
            object obj= null;
            try
            {
                obj = service.GetOne(sql, pms);

                if (obj == null)
                {
                    obj = 0;
                }
               
                if (obj != null && !obj.Equals(null))
                {

                    msg.Msgflag = true;
                    msg.Msgvalue = obj;
                }
                else 
                {
                    msg.Msgvalue = "没有数据";
                    
                }
            }
            catch(Exception ex)
            {
                msg.Msgvalue = "出现异常：" + ex.Message;
            }
            return msg;
        }

        /// <summary>
        /// 查询少量数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pms">参数</param>
        /// <returns></returns>
        public IMessageEntity GetDataTable(string sql, params MySqlParameter[] pms)
        {
            IMessageEntity msg = new MessageEntity();
            msg.Msgflag = false;
            service=new BaseService();
            DataTable dt = null;
            try
            {
                dt = service.GetDataTable(sql, pms);
                if (dt != null || !dt.Equals(null))
                {
                    msg.Msgflag = true;
                    msg.Msgvalue = dt;
                }
                else 
                {
                    msg.Msgvalue = "没有数据";
                    
                }
            }
            catch (Exception ex)
            {
                msg.Msgvalue = "出现异常：" + ex.Message;
            }
            return msg;
        }



       
        /// <summary>
        /// 模糊查询信息列表
        /// </summary>
        /// <param name="allinfo">查询那个表的信息</param>
        /// <param name="keyword">关键字查询</param>
        /// <param name="flag">数据排列顺序：true 升序   false：降序（根据创建时间来查询）</param>
        /// <returns></returns>
        public IMessageEntity SearchInfo(AllInfo allinfo,string keyword,bool flag) 
        {

            IMessageEntity msg = new MessageEntity();
            
            string sql = "";

            MySqlParameter pms = new MySqlParameter("@keyword", MySqlDbType.String) { Value = keyword };

            List<User> listUser = null;
            List<Talk> listTalk = null;
            List<Comment> listComment = null;
            List<User_log> listUserlog = null;
            List<Log_error> listLogerror = null;



            #region User
            if (allinfo==AllInfo.User)
            {
                listUser = new List<User>();

                if (flag)
                {
                    sql =string.Format( "select * from user where  uname like'%{0}%' or rname like'%{0}%'  or sex like'%{0}%' or des like'%{0}%'  or tel like'%{0}%' or email like'%{0}%' order by cdate",keyword);
                }
                else 
                {
                    sql = string.Format("select * from user where  uname like'%{0}%' or rname like'%{0}%'  or sex like'%{0}%' or des like'%{0}%'  or tel like'%{0}%' or email like'%{0}%' order by cdate desc", keyword);
                }


                msg = GetInfo(sql, null);

                if (msg.Msgflag) 
                {
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
                }

                msg.Msgvalue = listUser;




            }

            #endregion

            #region Talk

            if (allinfo==AllInfo.Talk)
            {
                listTalk = new List<Talk>();

                if (flag)
                {
                    sql = string.Format("select * from talk where (theme like'%{0}%' or title like'%{0}%' or content like'%{0}%') and display=0 order by cdate", keyword);
                }
                else
                {
                    sql = string.Format("select * from talk where (theme like'%{0}%' or title like'%{0}%' or content like'%{0}%') and display=0 order by cdate desc", keyword);
                }

                msg = GetInfo(sql, null);
                if (msg.Msgflag)
                { 
                    using (MySqlDataReader reader =(MySqlDataReader)msg.Msgvalue) 
                    {
                        if (reader.HasRows) 
                        {
                            while (reader.Read()) 
                            {
                                Talk talk = new Talk();
                                talk.Id = Convert.ToInt32(reader["id"]);
                                talk.Theme = Convert.ToString(reader["theme"]);
                                talk.Title = Convert.ToString(reader["title"]);
                                talk.Content = Convert.ToString(reader["content"]);
                                talk.CDate = Convert.ToDateTime(reader["cdate"]);
                                talk.UDate = Convert.ToDateTime(reader["udate"] == System.DBNull.Value ? DateTime.Now : reader["udate"]);
                                talk.UId = Convert.ToInt32(reader["uid"]);
                                listTalk.Add(talk);
                            }
                        }
                    }
                }

                msg.Msgvalue = listTalk;

                
            }

            #endregion

            #region Comment

            if (allinfo == AllInfo.Comment) 
            {
                listComment = new List<Comment>();

                if (flag) 
                {
                    sql = string.Format("select * from comment where content like'%{0}%' order by cdate", keyword);
                }
                else 
                {
                    sql = string.Format("select * from comment where content like'%{0}%' order by cdate desc", keyword);
                }
                msg = GetInfo(sql, pms);
                if (msg.Msgflag) 
                {
                    using (MySqlDataReader reader = (MySqlDataReader)msg.Msgvalue) 
                    {
                        if (reader.HasRows) 
                        {
                            while (reader.Read()) 
                            {
                                Comment comment = new Comment();
                                comment.Id = Convert.ToInt32(reader["id"]);
                                comment.Content = Convert.ToString(reader["content"] == System.DBNull.Value ? "" : reader["content"]);
                                comment.CDate = Convert.ToDateTime(reader["cdate"]);
                                comment.Display = Convert.ToInt32(reader["display"]);
                                comment.UId = Convert.ToInt32(reader["uid"]);
                                comment.TId = Convert.ToInt32(reader["tid"]);
                                comment.UDate = Convert.ToDateTime(reader["udate"] == System.DBNull.Value ? DateTime.Now : reader["udate"]);
                                listComment.Add(comment);
                            }
                        }
                    }
                }
                msg.Msgvalue = listComment;

            }
            #endregion

            #region User_log

            if (allinfo == AllInfo.User_log) 
            {
                listUserlog = new List<User_log>();

                if (flag)
                {
                    sql = string.Format("select * from user_log where  content like'%{0}%' order by cdate", keyword);
                }
                else
                {
                    sql = string.Format("select * from user_log where  content like'%{0}%' order by cdate desc", keyword);
                }

            }
            #endregion


            #region Log_error
            if (allinfo == AllInfo.Log_error) 
            {
                listLogerror = new List<Log_error>();

                if (flag)
                {
                    sql = string.Format("select * from log_error where content like'%@keyword%' order by cdate", keyword);
                }
                else
                {
                    sql = string.Format("select * from log_error where content like'%@keyword%' order by cdate desc", keyword);
                }

            }
            #endregion


            return msg;
        }


    }
}
