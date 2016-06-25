using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 图书馆管理系统
{
    public partial class book_return : Form
    {
        public book_return(string mark1,string mark2,string mark3,string mark4)
        {
            InitializeComponent();
            value[0] = mark1;
            value[1] = mark2;
            value[2] = mark3;
            value[3] = mark4;
        }
        public string[] value = new string[5];
        private void book_return_Load(object sender, EventArgs e)
        {
            label5.Text = value[0];
            label6.Text = value[2];
            label10.Text = value[3];
            int a = JudgeTime(value[2]);
            if (a < int.Parse(value[3]))
            { 
                label7.Text = "否";
                label8.Text = "0元";
            }
            else
            {
                label7.Text = "是";
                float b = Calcute(a - int.Parse(value[3]));
                label8.Text = b + "元";
            }
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 判断还书的时间与结束时间的间隔
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int JudgeTime(string m)
        {
            DateTime t1 = new DateTime();
            DateTime t2 = new DateTime();
            t1=Convert.ToDateTime(m);
            t2 = DateTime.Now;
            TimeSpan ts = t2.Subtract(t1);
            int a = int.Parse(ts.Days.ToString());
            return a;
        }
        /// <summary>
        /// 计算超期后的罚金数量
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public float Calcute(int m)
        {
            DateBase date = new DateBase();
            float money = 0;
            string sqlstring = "select 超期罚金 from Library_Parameter where ID='1'";
            SqlDataReader dr = date.GetDataReader(sqlstring);
            if(dr.Read())
            {
                money = float.Parse(dr[0].ToString());
            }
            else
            {
                MessageBox.Show("数据库已损坏！请联系维护人员", "警告", MessageBoxButtons.OK);
            }
            return money*m;
        }
        /// <summary>
        /// 确认归还按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
