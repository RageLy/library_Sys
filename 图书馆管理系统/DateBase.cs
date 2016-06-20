using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace 图书馆管理系统
{
    class DateBase
    {
        public SqlConnection connection;
        /// <summary>
        /// 打开数据库
        /// </summary>
        public void open()
        {
            string connectionString = "Data Source=LY;Initial Catalog=Library_Soft;User ID=sa;Password=ly1994318kb";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void close()
        {
            connection.Dispose();
            connection.Close();
            connection = null;
        }
        /// <summary>
        /// 查询数据库连接状态
        /// </summary>
        /// <returns></returns>
        public int Check()
        {
            try
            {
                open();
                close();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 输入sql命令，得到DataReader对象
        /// </summary>
        /// <param name="sqlstring"></param>
        /// <returns></returns>
        public SqlDataReader GetDataReader(string sqlstring)
        {
            open();
            SqlCommand mycom = new SqlCommand(sqlstring, connection);
            SqlDataReader dr = mycom.ExecuteReader();
            return dr;
        }
        /// <summary>
        /// 输入sql命令，得到DataSet对象
        /// </summary>
        /// <param name="sqlstring"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sqlstring,string name)
        {
            open();
            SqlCommand mycom = new SqlCommand(sqlstring, connection);
            SqlDataAdapter myadp = new SqlDataAdapter();
            myadp.SelectCommand = mycom;
            DataSet mydt = new DataSet();
            myadp.Fill(mydt,name);
            return mydt;
        }
        /// <summary>
        /// 执行非查询的SQL命令
        /// </summary>
        /// <param name="sqlstring"></param>
        /// <returns></returns>
        public int ExecuteSQL(string sqlstring)
        {
            int count = -1;
            open();
            try
            {
                SqlCommand cmd = new SqlCommand(sqlstring, connection);
                count = cmd.ExecuteNonQuery();
            }
            catch
            {
                count = -1;
            }
            finally
            {
                close();
            }
            return count;
        }
        /// <summary>
        /// 输入SQL命令，检查数据表中是否有该数据信息
        /// </summary>
        /// <param name="sqlstring"></param>
        /// <returns></returns>
        public int GetdataRow(string sqlstring)
        {
            int CountRow = 0;
            open();
            SqlCommand mycom = new SqlCommand(sqlstring, connection);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = mycom;
            DataSet ds = new DataSet();
            da.Fill(ds);
            ds.CaseSensitive = false;
            CountRow = ds.Tables[0].Rows.Count; //取行集合中元素的总数
            close();
            return CountRow;
        }
        /// <summary>
        /// 输入SQL命令，得到DataTable对象
        /// </summary>
        /// <param name="sqlstring"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sqlstring)
        {
            DataSet ds = new DataSet(sqlstring);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            return dt;
        }
        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="sqlstring"></param>
        /// <returns></returns>
        public object GetScalar(string sqlstring)
        {
            open();
            SqlCommand mycom = new SqlCommand(sqlstring, connection);
            object result = mycom.ExecuteScalar();
            close();
            return result;
        }
        /// <summary>
        /// 对整体数据集实施批量更新，一般用于列表这样的对象
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="sql"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool doUpdate(DataSet ds, String sql, String tableName)
        {
            bool flag = false;
            open();
            //强制资源清理；清理非托管资源、不受GC控制的资源。using结束后会隐式的调用
            //dispose方法
            using (SqlDataAdapter da = new SqlDataAdapter(sql, connection))
            {
                //数据库表中一定要有主键列，否则刺出无法通过
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                try
                {
                    lock (this)
                    {
                        da.Update(ds, tableName);
                        flag = true;
                    }
                }
                catch (SqlException e1)
                {
                    throw new Exception(e1.Message);
                }
            }
            close();
            return flag;
        }
        /// <summary>
        /// 查询某个表的某列属性，并形成列表
        /// </summary>
        /// <param name="sqlstring">查询SQL字符串</param>
        /// <param name="m">第m列的属性，整数类型</param>
        /// <returns></returns>
        public ArrayList GetListArray(string sqlstring, int m)
        {
            ArrayList array = new ArrayList();   //创建arraylist对象
            SqlDataReader dr = GetDataReader(sqlstring);
            while (dr.Read())                   //遍历所有的结果集
            {
                array.Add(dr.GetValue(m));       //取到结果集索引的第0列的值并添加到ArrayList对象中
            }
            return array;                   //返回arraylist对象
        }
        /// <summary>
        /// 执行存储过程，返回command对象
        /// </summary>
        /// <param name="sqlstring"></param>
        /// <returns></returns>
        public SqlCommand GetProcCommand(string sqlstring)
        {
            open();
            SqlCommand mycom = new SqlCommand(sqlstring, connection);
            return mycom;
        }
    }
}
