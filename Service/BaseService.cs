using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MySql.Data;
using MySql.Data.MySqlClient;
using Interface;
using System.Data;

namespace Service
{
    public class BaseService:IBaseService
    {
        public object GetOne(string sql, params MySqlParameter[] pms)
        {

            object obj = SqlHelper.GetOne(sql, pms);
            return obj == System.DBNull.Value ? null : obj;
        }


        public MySqlDataReader GetInfo(string sql, params MySqlParameter[] pms)
        {
            return SqlHelper.GetInfo(sql, pms);
        }

        public DataTable GetDataTable(string sql,params MySqlParameter[] pms) 
        {
            return SqlHelper.GetDataTable(sql,pms);
        }
    }
}
