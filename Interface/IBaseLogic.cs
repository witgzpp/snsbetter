using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace Interface
{
    public interface IBaseLogic
    {
        IMessageEntity GetInt(string sql, params MySqlParameter[] pms);
        IMessageEntity GetOne(string sql, params MySqlParameter[] pms);
        IMessageEntity GetInfo(string sql, params MySqlParameter[] pms);
        IMessageEntity GetDataTable(string sql, params MySqlParameter[] pms);
    }
}
