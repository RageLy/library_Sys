using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 图书馆管理系统
{
    public partial class Main : Form
    {
        public Main(int rank_,int mark1_,int mark2_,string name_)
        {
            InitializeComponent();
            rank = rank_;
            mark1 = mark1_;
            mark2 = mark2_;
            name = name_;
        }
        public Main()
        {
            InitializeComponent();
        }
        public int rank=0;
        public int mark1=0;
        public int mark2=0;
        public static string name="";
        private void Main_Load(object sender, EventArgs e)
        {
            initialize();
        }       
        private void initialize()
        {
            label6.Text = name;
            label7.Text = "成功";
            label7.ForeColor = Color.Green;
            label8.Text = DateTime.Now.ToString();
            if(mark1==1)
            {
                label2.Text = "";
            }
            else if(mark1==0)
            {
                label2.Text = "本软件尚未购买，请联系销售人员";
            }
            else
            {
                label2.Text = "本软件尚未注册，请联系管理人员";
            }
        }
        /// <summary>
        /// 时间工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.ToString();
        }
        /// <summary>
        /// 查询数据库连接状况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            DateBase date = new DateBase();
            int i = date.Check();
            if (i == 0)
            {
                label7.Text = "失败";
                label7.ForeColor = Color.Red;
            }
            else
            {
                label7.Text = "成功";
                label7.ForeColor = Color.Green;
            }
        }
        /// <summary>
        /// 返回当前的操作用户
        /// </summary>
        /// <returns></returns>
        public string CheckName()
        {
            return name;
        }
        /// <summary>
        /// 图书借阅按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            BooksBroow.getBookBorrow().Show();
            BooksBroow.getBookBorrow().Activate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rank > 2)
            {
                Book_Alter f1 = new Book_Alter(rank.ToString(), name);
                f1.Show();
            }
            else
            {
                MessageBox.Show("当前用户没有权限进行用户管理！", "警告", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (rank > 2)
            {
                User_Mange f1 = new User_Mange();
                f1.Show();
            }
            else
            {
                MessageBox.Show("当前用户没有权限进行用户管理！", "警告", MessageBoxButtons.OK);
            }
        }

        private void 联系我们ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.rageangle.cn");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Information_Check f1 = new Information_Check();
            f1.Show();
        }
    }
}
