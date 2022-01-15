using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modal;
using Common;
using Interface;
using MySql.Data.MySqlClient;

namespace Logic
{
    public class Disable_wordLogic:BaseLogic
    {

        

        public IMessageEntity AddDisable_word(Disable_word w) 
        {
            IMessageEntity msg = new MessageEntity();
            w.Cdate = DateTime.Now;
            string sql = "insert into disable_word(content,cdate) values(@content,@cdate)";
            MySqlParameter[] pms = new MySqlParameter[2];
            pms[0] = new MySqlParameter("@content", MySqlDbType.String) { Value = w.Content };
            pms[1] = new MySqlParameter("@cdate", MySqlDbType.DateTime) { Value = w.Cdate };
            msg = GetInt(sql, pms);
            return msg;
        }

        public IMessageEntity SelectContent() 
        {
            List<string> contents = new List<string>();
            IMessageEntity msgflag = new MessageEntity();
            string sql = "select content from disable_word";
            msgflag = GetInfo(sql, null);  
            using (MySqlDataReader reader = (MySqlDataReader)msgflag.Msgvalue) 
            {
                    if (reader.HasRows) 
                    {
                        while (reader.Read()) 
                        {
                            string item = Convert.ToString(reader["content"]);
                            contents.Add(item);
                        }
                    }
             }



            msgflag.Msgvalue = contents;
            return msgflag;
        }
    }
}
