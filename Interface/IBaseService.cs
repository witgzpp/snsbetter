using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Interface
{
    public interface IBaseService
    {
        object GetOne(string sql, params MySqlParameter[] pms);
        MySqlDataReader GetInfo(string sql,params MySqlParameter[] pms);

        DataTable GetDataTable(string sql, params MySqlParameter[] pms);
    }
}
