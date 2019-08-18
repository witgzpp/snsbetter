using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using Common;
using MySql.Data.MySqlClient;
using Modal;
using System.Configuration;

namespace Logic
{
    
    public class TalkLogic:BaseLogic 
    {
        UserLogic userlogic = new UserLogic();
        private IMessageEntity SelectInfo(string sql, params MySqlParameter[] pms) 
        {
            IMessageEntity msg = GetInfo(sql, pms);
            List<Talk> talkList = new List<Talk>();

            using (MySqlDataReader reader = (MySqlDataReader)msg.Msgvalue)
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
                        talk.UName = Convert.ToString(reader["uname"] == System.DBNull.Value ? "" : reader["uname"]);
                        talk.UNames = Convert.ToString(reader["unames"] == System.DBNull.Value ? "" : reader["unames"]);
                        talk.Updateid = Convert.ToInt32(reader["updateid"] == System.DBNull.Value ? "0" : reader["updateid"]);
                        talk.CName = Convert.ToString(reader["cname"]);

                        talk.Display = Convert.ToInt32(reader["display"]);

                        talkList.Add(talk);
                    }
                }
            }
            msg.Msgvalue = talkList;
            return msg;
        }
        
        /// <summary>
        /// 查询所有Talk信息，并以List<Talk>为msg的value返回（display=0的）
        /// </summary>
        /// <returns></returns>
        public IMessageEntity SelectTalk() 
        {
            string sql = "select * from talk where display=0";
            return SelectInfo(sql, null);
            
        }

        public IMessageEntity SelectAllTalk() 
        {
            string sql = "select * from talk";
            return SelectInfo(sql, null);
        }


        /// <summary>
        /// 根据cname查询Talk信息，并以List<Talk>为msg的value返回
        /// </summary>
        /// <param name="uname"></param>
        /// <returns></returns>
        public IMessageEntity SelectTalk(string cname) 
        {
            IMessageEntity msg = new MessageEntity();
            string sql = "select * from talk where cname=@cname";
            MySqlParameter pms = new MySqlParameter("@cname", MySqlDbType.String) { Value = cname };
            msg = SelectInfo(sql, pms);
            return msg;
        }


        /// <summary>
        /// 根据标题查询出所有信息
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public IMessageEntity SelectTalkByTheme(string theme) 
        {
            IMessageEntity msg = new MessageEntity();
            string sql="select * from talk where theme=@theme and display=0";
            MySqlParameter pms = new MySqlParameter("@theme", MySqlDbType.Text) { Value = theme };
            msg = SelectInfo(sql, pms);
            return msg;          
        }
        

        /// <summary>
        /// 查询一条Talk信息,并以Talk为msg的value返回
        /// </summary>
        /// <param name="id">Talk的id</param>
        /// <returns></returns>
        public IMessageEntity SelectTalk(int id) 
        {
            string sql = "select * from talk where id=@id";
            Talk talk = new Talk();
            MySqlParameter pms = new MySqlParameter("@id", MySqlDbType.Int32) { Value = id };
            IMessageEntity msg = new MessageEntity();
            msg = GetInfo(sql, pms);

            using (MySqlDataReader reader = (MySqlDataReader)msg.Msgvalue) 
            {
                if (reader.HasRows) 
                {
                    while (reader.Read()) 
                    {
                        talk.Id = Convert.ToInt32(reader["id"]);
                        talk.Theme = Convert.ToString(reader["theme"]);
                        talk.Title = Convert.ToString(reader["title"]);
                        talk.Content = Convert.ToString(reader["content"]);
                        talk.CDate = Convert.ToDateTime(reader["cdate"]);
                        talk.UDate = Convert.ToDateTime(reader["udate"] == System.DBNull.Value ? DateTime.Now : reader["udate"]);
                        talk.UId = Convert.ToInt32(reader["uid"]);
                        talk.UName = Convert.ToString(reader["uname"] == System.DBNull.Value ? "" : reader["uname"]);
                        talk.UNames = Convert.ToString(reader["unames"] == System.DBNull.Value ? "" : reader["unames"]);
                        talk.Updateid = Convert.ToInt32(reader["updateid"] == System.DBNull.Value ? "0" : reader["updateid"]);
                        talk.CName = Convert.ToString(reader["cname"]);
                    }              
                   
                }
            }
            msg.Msgvalue = talk;

            return msg;


        }


        



        public IMessageEntity AddTalk(Talk talk)
        {
            
            string sql = "insert into talk(theme,title,content,cdate,uid,cname,display) values(@theme,@title,@content,@cdate,@uid,@cname,@display)";
            //theme、title、content、cdate、uid、cname,display        

            MySqlParameter[] pms=new MySqlParameter[7];
            pms[0] = new MySqlParameter("@theme", MySqlDbType.String) { Value = talk.Theme };
            pms[1] = new MySqlParameter("@title", MySqlDbType.String) { Value = talk.Title };
            pms[2] = new MySqlParameter("@content", MySqlDbType.String) { Value = talk.Content };
            pms[3] = new MySqlParameter("@cdate", MySqlDbType.DateTime) { Value = talk.CDate };
            pms[4] = new MySqlParameter("@uid", MySqlDbType.String) { Value = talk.UId };
            pms[5] = new MySqlParameter("@cname", MySqlDbType.String) { Value = talk.CName };
            pms[6] = new MySqlParameter("@display", MySqlDbType.Int32) { Value = talk.Display };
            return GetInt(sql, pms);


        }



        /// <summary>
        /// 发布人和非发布人(普通用户)公用该方法
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="talk"></param>
        /// <returns></returns>
        public IMessageEntity UpdateTalk(string uname,Talk talk) 
        {
            IMessageEntity msg = new MessageEntity();
            IMessageEntity msguser = userlogic.SelectUserId(uname);

            

            if (msguser.Msgflag)
            {
                talk.Updateid = Convert.ToInt32(msguser.Msgvalue);
            }
            else
            {
                msg.Msgvalue = "更新用户id未查询到";
                return msg;
            }

            IMessageEntity msgUnames = SearchUNames(talk.Id);
            if (msgUnames.Msgflag) //存在数据
            {
                if (msgUnames.Msgvalue.ToString().Length <= 1)
                {
                    talk.UNames = uname;
                }
                else
                {
                    talk.UNames = msgUnames.Msgvalue.ToString() + ";" + uname;
                }

                
            }
            else
            {
                talk.UNames = uname;
            }
            

            string sql = "update  talk set theme=@theme,title=@title,content=@content,udate=@udate,updateid=@updateid,uname=@uname,unames=@unames where id=@id ";
            MySqlParameter[] pms = new MySqlParameter[8];
            pms[0] = new MySqlParameter("@theme", MySqlDbType.String) { Value = talk.Theme };
            pms[1] = new MySqlParameter("@title", MySqlDbType.String) { Value = talk.Title };
            pms[2] = new MySqlParameter("@content", MySqlDbType.String) { Value = talk.Content };
            pms[3] = new MySqlParameter("@udate", MySqlDbType.DateTime) { Value = talk.UDate };
            pms[4] = new MySqlParameter("@updateid", MySqlDbType.Int32) { Value = talk.Updateid };

            pms[5] = new MySqlParameter("@uname", MySqlDbType.String) { Value = uname };
            pms[6] = new MySqlParameter("@unames", MySqlDbType.String) { Value = talk.UNames };
            pms[7] = new MySqlParameter("@id", MySqlDbType.Int32) { Value = talk.Id };

            return GetInt(sql, pms);
           
        }

        public IMessageEntity DeleteTalk(int id)
        {
            string sql = "delete from talk where id=@id";
            MySqlParameter pms = new MySqlParameter("@id", MySqlDbType.Int32) { Value = id };

            return GetInt(sql, pms);
        }


        private IMessageEntity SearchUNames(int id) 
        {

            string sql = "select unames from talk where id=@id";
            MySqlParameter pms = new MySqlParameter("@id", MySqlDbType.Int32) { Value = id };
            return GetOne(sql, pms);
        }

        public IMessageEntity ShowTalk(int talkid) 
        {
            string sql = "update talk set display=0 where id=@id";
            MySqlParameter pms = new MySqlParameter("@id", MySqlDbType.Int32) { Value = talkid };

            return GetInt(sql, pms);
        }


        /// <summary>
        /// 更新talk表的display字段为1，隐藏该条信息
        /// </summary>
        /// <param name="talkid"></param>
        /// <returns></returns>
        public IMessageEntity HideTalk(int talkid)
        {
            string sql = "update talk set display=1 where id=@id";
            MySqlParameter pms = new MySqlParameter("@id", MySqlDbType.Int32) { Value = talkid };

            return GetInt(sql, pms);
        }


        
        /// <summary>
        /// 更新talk表的display字段为0，显示该条信息
        /// </summary>
        /// <param name="cname"></param>
        /// <returns></returns>
        public IMessageEntity DeleteTalk(string cname) 
        {
            string sql = "delete from talk where cname=@cname";
            MySqlParameter pms = new MySqlParameter("@cname", MySqlDbType.String) { Value = cname };
            return GetInt(sql, pms);
        }


        
        
    }
}
