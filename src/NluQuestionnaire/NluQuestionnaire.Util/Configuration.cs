using System;
using System.Data;
using System.Data.SqlClient;

namespace NluQuestionnaire.Util
{
    public sealed class Configuration
    {
        public DataTable ExecuteDataTablePro(string ProcedureName, params SqlParameter[] pars)
        {
            string connStr = "Data Source=nltc566;Initial Catalog=NluTestDb;Persist Security Info=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    DataTable dataTable = new DataTable();
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;//指定执行存储过程操作
                        cmd.CommandText = ProcedureName;//存储过程名称
                        if (pars != null)
                        {
                            //遍历参数,添加到SqlCommand 对象子执行
                            foreach (SqlParameter parm in pars)
                                cmd.Parameters.Add(parm);//
                        }
                        DataSet dataset = new DataSet();//新建DataSet对象，用于保存查询结果
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);//把执行cmd，更新数据结果到adapter对象
                        adapter.Fill(dataset);//adapter对象的Fill方法把结果添加到DataSet对象中
                        dataTable = dataset.Tables[0];
                    }
                    catch (Exception e)
                    {
                        string ExecuteResult = e.Message;
                        Console.Write(ExecuteResult);
                        return null;
                    }
                    finally
                    {
                        conn.Close();
                        cmd.Parameters.Clear();
                    }

                    return dataTable;
                }
            }
        }
        public int ExecuteNonQueryPro(string ProcedureName, params SqlParameter[] pars)
        {
            string connStr = "Data Source=nltc566;Initial Catalog=NluTestDb;Integrated Security =True;MultipleActiveResultSets=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    int num = 0;
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;//指定执行存储过程操作
                        cmd.CommandText = ProcedureName;//存储过程名称
                        if (pars != null)
                        {
                            //遍历参数,添加到SqlCommand 对象子执行
                            foreach (SqlParameter parm in pars)
                                cmd.Parameters.Add(parm);//
                        }
                        num = cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        string ExecuteResult = e.Message;
                        Console.Write(ExecuteResult);
                        return 0;
                    }
                    finally
                    {
                        conn.Close();
                    }
                    return num;//执行数据库语句并返回受影响的行数
                }
            }
        }
    }
}
