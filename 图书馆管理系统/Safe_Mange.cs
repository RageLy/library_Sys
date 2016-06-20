using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;

namespace 图书馆管理系统
{
    class Safe_Mange
    {
        /// <summary>
        /// 获取电脑的cpu编号
        /// </summary>
        /// <returns></returns>
        public string CPU_inf()
        {
            try
            {
                string cpuInfo = "";//cpu序列号                                   
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return "cpu序列号：" + cpuInfo;
            }
            catch { return "unknow"; }
            finally { }
        }
        /// <summary>
        /// 加密获取的字符串
        /// </summary>
        /// <returns></returns>
        public String EnCrypt()
        {
            string cpu = CPU_inf();
            //cpu += DateTime.Now.Year.ToString();
            //cpu += DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            //cpu += DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            //cpu += DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            cpu += "rageangle--1357924680.。";

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(cpu)), 4, 8);
            t2 = t2.Replace("-", "");          
            return t2; 
        }
        /// <summary>
        /// 将加密后的字符串写入到数据库
        /// </summary>
        /// <returns></returns>
        public int Write_code()
        {
            DateBase db = new DateBase();
            string code = EnCrypt();
            string sql = "insert into Safe_Rec(safe_ID,购买) values('" + code + "','0')";
            int i = db.ExecuteSQL(sql);
            return i;
        }

        public int Prove_Safe()
        {
            int count = 0;
            int state = 0;
            DateBase Date = new DateBase();
            string SqlString = "select * from Safe_Rec";
            count = Date.GetdataRow(SqlString);
            SqlDataReader dr;
            dr = Date.GetDataReader(SqlString);
            if (dr.Read())
            {              
                state = int.Parse(dr[1].ToString());
                if(state==1)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
