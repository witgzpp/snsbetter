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

using HannDao;


namespace Logic
{
    public class BaseLogic:IBaseLogic
    {

        private MysqlDao dao_third = new MysqlDao();
        private BaseDaos dao=null;
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
                dao = new BaseDaos();

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



       
       





        public IMessageEntity Add<T>(string tableName, T t)
        {
            IMessageEntity msg = new MessageEntity();
            bool result = dao_third.Add<T>(tableName, t);
            if (result)
            {
                msg.Msgflag = result;
                msg.Msgvalue = "1";
            }
            else 
            {
                msg.Msgflag = false;
                msg.Msgvalue = "0";
            }
            return msg;
        }


        public IMessageEntity Delete<T>(string tableName, string query)
        {
            IMessageEntity msg = new MessageEntity();
            bool result = dao_third.Delete<T>(tableName, query);
            if (result)
            {
                msg.Msgflag = result;
                msg.Msgvalue = "删除成功";
            }
            return msg;
        }

        public IMessageEntity GetBigInfo<T>(string tableName, string query)
        {
            IMessageEntity msg = new MessageEntity();
            List<T> result = dao_third.GetBigInfo<T>(tableName, query);
            if (result!=null && result.Count>=1)
            {
                msg.Msgflag = true;
                msg.Msgvalue = result;
            }
            return msg;
        }

        public IMessageEntity GetInfo<T>(string tableName, string query)
        {
            IMessageEntity msg = new MessageEntity();
            List<T> result = dao_third.GetInfo<T>(tableName, query);
            if (result != null && result.Count >= 1)
            {
                msg.Msgflag = true;
                msg.Msgvalue = result;
            }
            else 
            {
                msg.Msgvalue = "data server error";
            }
            return msg;
        }

        public IMessageEntity GetListField<T>(string tableName, string field, string query)
        {
            IMessageEntity msg = new MessageEntity();
            List<T> result = dao_third.GetListField<T>(tableName, field, query);
            if (result != null && result.Count >= 1)
            {
                msg.Msgflag = true;
                msg.Msgvalue = result;
            }
            return msg;
        }

        public IMessageEntity Update<T>(string tableName, T t)
        {
            IMessageEntity msg = new MessageEntity();
            bool result = dao_third.Update<T>(tableName, t);
            if (result)
            {
                msg.Msgflag = result;
                msg.Msgvalue = "更新成功";
            }
            else 
            {
                msg.Msgvalue = "更新失败";
            }
            return msg;
        }
    }
}
