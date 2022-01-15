using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Interface
{
    public interface IBaseDao
    {
        int GetInt(string sql,params MySqlParameter[] pms);
    }
}
