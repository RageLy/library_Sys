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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int rank=0;
        public int mark1 = 0;
        public int mark2=0;
        public string name = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            Creat_ID_Re p = new Creat_ID_Re();
            Safe_Mange safe = new Safe_Mange();
            int i = safe.Prove_Safe();
            int j = 1;
            if(i==0)
            {               
                j=safe.Write_code();
            }
            mark1 = i;
            mark2 = j;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            int state = 0;
       
            if(textBox1.Text==""||textBox2.Text=="")
            {
                MessageBox.Show("用户名或者账户为空！", "警告", MessageBoxButtons.OK);
            }
            else
            {
                DateBase Date = new DateBase();
                try
                {                  
                    string SqlString = "select * from Library_Login where 用户名='"+textBox1.Text+"' and 密码='"+textBox2.Text+"'";
                    count = Date.GetdataRow(SqlString);
                    SqlDataReader dr;
                    dr = Date.GetDataReader(SqlString);
                    if (dr.Read())
                    {
                        count = int.Parse(dr[3].ToString());
                        state = int.Parse(dr[6].ToString());
                        rank = count;
                        name = dr[1].ToString();
                    }
                    if (state == 0)
                    {
                        MessageBox.Show("账户已经被停用！请联系管理员", "警告", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (count >= 2)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else if (count == 1)
                        {
                            MessageBox.Show("权限不够！请联系管理员", "警告", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("用户名或者账户错误！", "警告", MessageBoxButtons.OK);
                        }
                    }
                }
                catch
                {
                    
                }
            }
        }
    }
}
