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
                        //Talk talk = new Talk();
                        //talk.Id = Convert.ToInt32(reader["id"]);
                        //talk.Theme = Convert.ToString(reader["theme"]);
                        //talk.Title = Convert.ToString(reader["title"]);
                        //talk.Content = Convert.ToString(reader["content"]);
                        //talk.Cdate = Convert.ToDateTime(reader["cdate"]);
                        //talk.Udate = Convert.ToDateTime(reader["udate"] == System.DBNull.Value ? DateTime.Now : reader["udate"]);
                        //talk.Uid = Convert.ToInt32(reader["uid"]);
                        //talk.Uname = Convert.ToString(reader["Uname"] == System.DBNull.Value ? "" : reader["Uname"]);
                        //talk.Unames = Convert.ToString(reader["Unames"] == System.DBNull.Value ? "" : reader["Unames"]);
                        //talk.Updateid = Convert.ToInt32(reader["updateid"] == System.DBNull.Value ? "0" : reader["updateid"]);
                        //talk.Cname = Convert.ToString(reader["cname"]);

                        //talk.Display = Convert.ToInt32(reader["display"]);

                        //talkList.Add(talk);
                    }
                }
            }
            msg.Msgvalue = talkList;
            return msg;
        }
        

        public IMessageEntity SelectAllTalk() 
        {
            string sql = "select * from talk";
            return SelectInfo(sql, null);
        }


        /// <summary>
        /// 根据cname查询Talk信息，并以List<Talk>为msg的value返回
        /// </summary>
        /// <param name="Uname"></param>
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
        /// 发布人和非发布人(普通用户)公用该方法
        /// </summary>
        /// <param name="Uname"></param>
        /// <param name="talk"></param>
        /// <returns></returns>
        public IMessageEntity UpdateTalk(string Uname,Talk talk) 
        {
            //IMessageEntity msg = new MessageEntity();
            //IMessageEntity msguser = userlogic.SelectUserId(Uname);

            

            //if (msguser.Msgflag)
            //{
            //    talk.Updateid = Convert.ToInt32(msguser.Msgvalue);
            //}
            //else
            //{
            //    msg.Msgvalue = "更新用户id未查询到";
            //    return msg;
            //}

            ////IMessageEntity msgUnames = SearchUnames(talk.Id);
            ////if (msgUnames.Msgflag) //存在数据
            ////{
            ////    if (msgUnames.Msgvalue.ToString().Length <= 1)
            ////    {
            ////        talk.Unames = Uname;
            ////    }
            ////    else
            ////    {
            ////        talk.Unames = msgUnames.Msgvalue.ToString() + ";" + Uname;
            ////    }

                
            ////}
            ////else
            ////{
            ////    talk.Unames = Uname;
            ////}
            

            //string sql = "update  talk set theme=@theme,title=@title,content=@content,udate=@udate,updateid=@updateid,Uname=@Uname,Unames=@Unames where id=@id ";
            //MySqlParameter[] pms = new MySqlParameter[8];
            //pms[0] = new MySqlParameter("@theme", MySqlDbType.String) { Value = talk.Theme };
            //pms[1] = new MySqlParameter("@title", MySqlDbType.String) { Value = talk.Title };
            //pms[2] = new MySqlParameter("@content", MySqlDbType.String) { Value = talk.Content };
            //pms[3] = new MySqlParameter("@udate", MySqlDbType.DateTime) { Value = talk.Udate };
            //pms[4] = new MySqlParameter("@updateid", MySqlDbType.Int32) { Value = talk.Updateid };

            //pms[5] = new MySqlParameter("@Uname", MySqlDbType.String) { Value = Uname };
            //pms[6] = new MySqlParameter("@Unames", MySqlDbType.String) { Value = talk.Unames };
            //pms[7] = new MySqlParameter("@id", MySqlDbType.Int32) { Value = talk.Id };

            //return GetInt(sql, pms);
            return new MessageEntity();
           
        }

        public IMessageEntity DeleteTalk(int id)
        {
            string sql = "delete from talk where id=@id";
            MySqlParameter pms = new MySqlParameter("@id", MySqlDbType.Int32) { Value = id };

            return GetInt(sql, pms);
        }



        public IMessageEntity ShowTalk(string talkid) 
        {
            string sql = "update talk set display=1 where id=@id";
            MySqlParameter pms = new MySqlParameter("@id", MySqlDbType.String) { Value = talkid };

            return GetInt(sql, pms);
        }


        /// <summary>
        /// 更新talk表的display字段为0，隐藏该条信息
        /// </summary>
        /// <param name="talkid"></param>
        /// <returns></returns>
        public IMessageEntity HideTalk(string talkid)
        {
            string sql = "update talk set display=0 where id=@id";
            MySqlParameter pms = new MySqlParameter("@id", MySqlDbType.String) { Value = talkid };

            return GetInt(sql, pms);
        }


        
     

        
        
    }
}
