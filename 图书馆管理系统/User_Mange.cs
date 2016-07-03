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
    public partial class User_Mange : Form
    {
        public User_Mange()
        {
            InitializeComponent();
        }
        private string[] value = new string[10];
        private  SqlDataAdapter sda = new SqlDataAdapter();
        private   DataTable dt = new DataTable();
        private void User_Mange_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = false;
            Fresh();
        }
        public void Fresh()
        {
            DateBase date = new DateBase();
            string sqlstring1 = "select 用户名,ID,密码,级别,电话,地址,状态 from Library_Login";
            date.open();
            SqlCommand cmd = new SqlCommand(sqlstring1, date.connection);
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = false;
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                if (c.Index != 2 && c.Index != 5 && c.Index != 4)
                {
                    c.ReadOnly = true;
                }
            }
        }
        public void test()
        {
            dt.Clear();
            dataGridView1.DataSource = dt;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确认该按钮
        /// 上传更改的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                sda.Update(dt);
                test();
                Fresh();
                MessageBox.Show("更新成功"); 
            }
            catch
            {
                MessageBox.Show("更新失败");
            }
            
        }
        /// <summary>
        /// 确认删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            string ID = dataGridView1.Rows[i].Cells[1].Value.ToString();
            delete_user f1 = new delete_user(ID);
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            add_user f1 = new add_user();
            f1.Show();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 把第4列显示*号，*号的个数和实际数据的长度相同
            if (e.ColumnIndex == 2)
            {
                if (e.Value != null && e.Value.ToString().Length > 0)
                {
                    e.Value = new string('*', 8);
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 编辑第4列时，把第4列显示为*号
            TextBox t = e.Control as TextBox;
            if (t != null)
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == 2)
                    t.PasswordChar = '*';
                else
                    t.PasswordChar = new char();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            test();
            Fresh();
        }
    }
}
