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
    public class CommentLogic:BaseLogic
    {
        /// <summary>
        /// 用户添加Comment评论
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public IMessageEntity AddComment(Comment com) 
        {
            string sql = "insert into comment(content,cdate,uid,tid) values(@content,@cdate,@uid,@tid)";
            MySqlParameter[] pms = new MySqlParameter[4];

            pms[0] = new MySqlParameter("@content", MySqlDbType.String) { Value = com.Content };
            pms[1] = new MySqlParameter("@cdate", MySqlDbType.DateTime) { Value = com.CDate };
            pms[2] = new MySqlParameter("@uid", MySqlDbType.Int32) { Value = com.UId };
            pms[3] = new MySqlParameter("@tid", MySqlDbType.Int32) { Value = com.TId };

            return GetInt(sql, pms);
        }


        /// <summary>
        /// 更改评论
        /// </summary>
        /// <param name="id">修改评论id</param>
        /// <param name="com">评论实体</param>
        /// <returns></returns>
        public IMessageEntity UpdateComment(int id,Comment com) 
        {
            string sql = "update table comment set content=@content,udate =@udate where id=@id";
            MySqlParameter[] pms = new MySqlParameter[3];
            pms[0] = new MySqlParameter("@content", MySqlDbType.String) { Value = com.Content };
            pms[1] = new MySqlParameter("@udate", MySqlDbType.DateTime) { Value = com.UDate };
            pms[2] = new MySqlParameter("@id", MySqlDbType.Int32) { Value = id };


            return GetInt(sql, pms);
        }
    }
}
