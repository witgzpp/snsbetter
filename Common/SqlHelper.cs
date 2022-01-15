  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;


namespace Common
{   
        public static class SqlHelper
        {
            /// <summary>
            /// 数据库连接字符串  ,客户端config配置key必须为conn.
            /// </summary>
            private static readonly string constr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            /// <summary>
            /// 判断增删改的操作是否成功     成功>0       失败=<0    异常时=-1
            /// </summary>
            /// <param name="sql">数据库查询语句</param>
            /// <param name="pms">查询参数</param>
            /// <returns>返回int类型数据</returns>
            public static int GetInt(string sql, params MySqlParameter[] pms)
            {
                int result = 0;
                MySqlConnection con = null;
                try
                {
                    using (con = new MySqlConnection(constr))
                    {

                        using (MySqlCommand com = new MySqlCommand(sql, con))
                        {
                            if (pms != null)
                            {

                                com.Parameters.AddRange(pms);
                            }

                            con.Open();
                            result = com.ExecuteNonQuery();
                            com.Parameters.Clear();
                            return result;
                        }
                    }
                }
                catch(Exception e)
                {
                    return -1;
                }
               
            }
            /// <summary>
            /// 查询一个数据
            /// </summary>
            /// <param name="sql">数据库查询语句</param>
            /// <param name="pms">查询参数</param>
            /// <returns>返回一object类型的数据</returns>
            public static object GetOne(string sql, params MySqlParameter[] pms)
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand com = new MySqlCommand(sql, con))
                    {
                        if (pms != null)
                        {
                            com.Parameters.AddRange(pms);
                        }
                        con.Open();
                        object result = com.ExecuteScalar();
                        com.Parameters.Clear();
                        return result;
                    }
                }
            }
            /// <summary>
            /// 大数据量查询
            /// </summary>
            /// <param name="sql">数据库查询语句</param>
            /// <param name="pms">查询参数</param>
            /// <returns>返回查询数据</returns>
            public static MySqlDataReader GetInfo(string sql, params MySqlParameter[] pms)
            {
                MySqlConnection con = new MySqlConnection(constr);
                using (MySqlCommand com = new MySqlCommand(sql, con))
                {
                    if (pms != null)
                    {
                        com.Parameters.AddRange(pms);
                    }
                    try
                    {
                        con.Open();
                        MySqlDataReader result = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        com.Parameters.Clear();
                        return result;
                    }
                    catch
                    {
                        con.Dispose();
                        con.Close();
                        throw;
                    }
                }
            }

            /// <summary>
            /// 小数据量查询
            /// </summary>
            /// <param name="sql">数据库查询语句</param>
            /// <param name="pms">查询参数</param>
            /// <returns>返回查询语句</returns>
            public static DataTable GetDataTable(string sql, params MySqlParameter[] pms)
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand com = new MySqlCommand(sql, con))
                    {
                        if (pms != null)
                        {
                            com.Parameters.AddRange(pms);
                        }
                        con.Open();
                        MySqlDataAdapter sqldataadapter = new MySqlDataAdapter(com);
                        DataSet dataset = new DataSet();
                        com.Parameters.Clear();
                        sqldataadapter.Fill(dataset);
                        return dataset.Tables[0];
                    }
                }
            }
            
        }
    
}
