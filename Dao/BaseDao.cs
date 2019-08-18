using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Common;
using Interface;

namespace Dao
{
    public class BaseDao:IBaseDao
    {
        /// <summary>
        /// 检查增、删、该操作是否成功执行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public int GetInt(string sql, params MySqlParameter[] pms)
        {
            return SqlHelper.GetInt(sql, pms);
        }


        

    }
}
