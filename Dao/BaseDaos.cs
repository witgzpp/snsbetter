using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Common;
using Interface;
using HannDao;

namespace Dao
{
    public class BaseDaos:MysqlDao,IBaseDao
    {
        /// <summary>
        /// 检查增、删、该操作是否成功执行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public int GetInt(string sql, params MySqlParameter[] pms)
        {
            return Common.SqlHelper.GetInt(sql, pms);
        }


        

    }
}
