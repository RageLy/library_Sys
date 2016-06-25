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
        public book_return(string mark1,string mark2,string mark3,string mark4,string mark5)
        {
            InitializeComponent();
            value[0] = mark1;     //图书名称
            value[1] = mark2;     //ID
            value[2] = mark3;     //借阅时间
            value[3] = mark4;     //借阅时长
            value[4] = mark5;    //当前的操作员
        }
        public string[] value = new string[7];
        private void book_return_Load(object sender, EventArgs e)
        {
            label5.Text = value[0];
            string s = value[2].Substring(0, 10);
            label6.Text = s;
            label10.Text = value[3];
            int a = JudgeTime(value[2]);
            if (a < int.Parse(value[3]))
            { 
                label7.Text = "否";
                value[6] = "0";
                value[5] = "0";
                label8.Text = "0元";
            }
            else
            {
                label7.Text = "是";
                value[6]="1";
                float b = Calcute(a - int.Parse(value[3]));
                value[5]=b.ToString();     //违约金额
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
            string[] mark=new string[15];
            int[] mark1 = new int[5];
            string sqlstring1 = "select * from Library_Borrow where 图书名称='" + value[0] + "'and ID = '" + value[1] + "'";
            string sqlstring2 = "delete  from Library_Borrow where 图书名称='" + value[0] + "'and ID = '" + value[1] + "'";
            DateBase date = new DateBase();
            
            //先把数据库中的借阅记录转移到历史记录，增加一条还书记录，
            SqlDataReader dr = date.GetDataReader(sqlstring1);
            if(dr.Read())
            {
                for(int i=0;i<10;i++)
                {
                    mark[i] = dr[i].ToString();
                }
            }
            else
            {
                MessageBox.Show("数据库已损坏！请联系维护人员", "警告", MessageBoxButtons.OK);
            }
            date.close();

            Creat_ID_Re cr = new Creat_ID_Re();
            mark[10] = cr.GetFormCode(3, 5);
            mark[11] = DateTime.Now.ToString();
            mark[12]=Judeg_class(mark[4]);
            string sqlstring3 = "insert into Library_Borrow_Re(单号,图书名称,ID,编号,分类号,类别,借阅时间,借阅时长,是否违约,操作员)  values('" + mark[0] + "','" + mark[1] + "','" + int.Parse(mark[2]) + "','" + mark[3] + "','" + int.Parse(mark[4]) + "','" + mark[5] + "','" + mark[6] + "','" + int.Parse(mark[7]) + "','" + mark[8] + "','" + mark[9] + "')";
            string sqlstring4 = "insert into Library_Return(还书单号,借书单号,图书名称,ID,编号,分类号,类别,还书时间,借阅时长,是否违约,操作员)   values('" + mark[10] + "','" + mark[0] + "','" + mark[1] + "','" + int.Parse(mark[2]) + "','" + mark[3]  + "','" + int.Parse(mark[4]) + "','" + mark[5] + "','" + mark[11] + "','" + int.Parse(mark[7]) + "','" + mark[8] + "','" + mark[9] + "')";
            string sqlstring5 = "update Library_User_Inf set 历史借阅数量=Library_User_Inf.历史借阅数量+1," + mark[12] + "=Library_User_Inf." + mark[12] + "-1,违约次数=Library_User_Inf.违约次数+'" + int.Parse(value[6]) + "',违约金=Library_User_Inf.违约金+'" + float.Parse(value[5]) + "' where ID='" + mark[2] + "'";
            string sqlstring6 = "update Library_Book set 剩余可借数量=Library_Book.剩余可借数量+1 where 编号='" + mark[3] + "'";
            //MessageBox.Show(sqlstring5);
            mark1[0] = date.ExecuteSQL(sqlstring3);
            if (mark1[0] == 1)
            {
                mark1[0] = date.ExecuteSQL(sqlstring4);
                if (mark1[0] == 1)
                {
                    mark1[0] = date.ExecuteSQL(sqlstring2);
                    if (mark1[0] == 1)
                    {
                        mark1[0] = date.ExecuteSQL(sqlstring5);
                        if (mark1[0] == 1)
                        {
                            mark1[0] = date.ExecuteSQL(sqlstring6);
                            if (mark1[0] == 1)
                            {
                                MessageBox.Show("还书成功！", "提示", MessageBoxButtons.OK);
                                BooksBroow f = new BooksBroow();
                                f.Fresh();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("5");
                            }
                        }
                        else
                        {
                            MessageBox.Show("4");
                        }
                    }
                    else
                    {
                        MessageBox.Show("3");
                    }
                }
                else
                {
                    MessageBox.Show("2");
                }
            }
            else
            {
                MessageBox.Show("1");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public string Judeg_class(string m)
        {
            string a = "";
            switch (m)
            {
                case "1":
                    a = "一类已借";
                    break;
                case "2":
                    a = "二类已借";
                    break;
                case "3":
                    a = "三类已借";
                    break;
                case "4":
                    a = "四类已借";
                    break;
            }
            return a;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
