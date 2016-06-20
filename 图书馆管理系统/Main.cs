using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public int rank=0;
        public int mark1=0;
        public int mark2=0;
        public string name="";
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

        private void button1_Click(object sender, EventArgs e)
        {
            BooksBroow.getBookBorrow().Show();
            BooksBroow.getBookBorrow().Activate();
        }
    }
}
