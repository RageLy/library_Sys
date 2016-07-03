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
    public partial class Book_Alter : Form
    {
        public Book_Alter(string a,string b)
        {
            InitializeComponent();
            value[4] = a;
            value[5] = b;
        }
        private SqlDataAdapter sda=new SqlDataAdapter();
        private SqlDataAdapter sda1 = new SqlDataAdapter();
        private DataTable dt=new DataTable();
        private DataTable dt1= new DataTable();
        private string[] value = new string[8];
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("内容禁止为空", "警告", MessageBoxButtons.OK);
            }
            else
            {
                string sqlstring1 = "select 编号,图书名称,出版社,库存数量,馆藏位置 from Library_Book where 编号='" + textBox1.Text.ToString() + "'";
                Fresh(sqlstring1);
            }
        }

        public void Fresh(string a)
        {
            DateBase date = new DateBase();
            date.open();
            SqlCommand cmd = new SqlCommand(a, date.connection);
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = false;
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                if (c.Index != 3)
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

        private void Book_Alter_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            label15.Text = "提示：直接在库存数量中修改，点击确认即保存";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";
            label22.Text = "";
            label23.Text = "";
            label24.Text = "";

            label16.ForeColor = Color.Red;
            label17.ForeColor = Color.Red;
            label18.ForeColor = Color.Red;
            label19.ForeColor = Color.Red;
            label20.ForeColor = Color.Red;
            label21.ForeColor = Color.Red;
            label22.ForeColor = Color.Red;
            label23.ForeColor = Color.Red;
            label24.ForeColor = Color.Red;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确认保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                int i = dataGridView1.CurrentCell.RowIndex;
                value[0] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                value[1] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                value[2] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                DateBase date = new DateBase();
                string sqlstring3 = "select 库存数量 from Library_Book where 编号='" + value[0].ToString() + "'";
                SqlDataReader dr = date.GetDataReader(sqlstring3);
                if(dr.Read())
                {
                    value[3] = dr[0].ToString();
                }
                else
                {
                    MessageBox.Show("数据库错误，代码1001", "警告", MessageBoxButtons.OK);
                }
                sda.Update(dt);
                int nums = int.Parse(value[2]) - int.Parse(value[3]);
                string xinwei="";
                if(nums<0)
                {
                    xinwei="损耗";
                }
                else if(nums>0)
                {
                    xinwei="增添";
                }

                string time = DateTime.Now.ToString();
                string sqlstring4 = "insert into Library_Book_Change(图书编号,图书名称,行为,时间,数量,操作人员,人员ID) values('" + value[0].ToString() + "','" + value[1].ToString() + "','" + xinwei.ToString() + "','" + time.ToString() + "','" + nums + "','" + value[5].ToString() + "','" + value[4].ToString() + "')";
                
                if (xinwei == "")
                {
                    i = 1;
                }
                else
                {        
                    MessageBox.Show(sqlstring4);
                    i = date.ExecuteSQL(sqlstring4);
                }

                if (i == 1&&xinwei!="")
                {
                    MessageBox.Show("修改成功");
                }
                else if(i!=1&&xinwei!="")
                {
                    MessageBox.Show("修改失败");
                }
            }
            catch(Exception man)
            {
                MessageBox.Show(man.ToString());
            }
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="")
            {
                MessageBox.Show("内容禁止为空", "警告", MessageBoxButtons.OK);
            }
            else
            {
                string sqlstring2 = "select 编号,图书名称,出版社,库存数量,馆藏位置 from Library_Book where 图书名称 like '%" + textBox2.Text.ToString() + "%'";
                test();
                Fresh(sqlstring2);
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if ( textBox3.Text.Length < 2 || textBox3.Text.Length > 10)
            {
                label16.Text = "名称长度应该在2—10";
            }
            else if(textBox3.Text == "")
            {
                label16.Text = "禁止为空";
            }
            else
            {
                label16.Text = "";
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                label17.Text = "禁止为空";
            }
            else if (textBox4.Text.Length < 5 || textBox4.Text.Length > 10)
            {
                label17.Text = "编号长度应该在5—10";
            }
            else
            {
                label17.Text = "";
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                label20.Text = "禁止为空";
            }
            else if (textBox5.Text != "1" && textBox5.Text != "2" && textBox5.Text != "3" && textBox5.Text != "4")
            {
                label20.Text = "类别在1—4之间";
            }
            else
            {
                label20.Text = "";
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                label18.Text = "禁止为空";
            }
            else
            {
                label18.Text = "";
            }
        }
        private int judge_number(string a,string b)
        {
            int i = 0;
            double j = 0;
            try
            {
                if (b == "double")
                {
                    j = double.Parse(a); 
                }
                else if(b=="int")
                {
                    i = int.Parse(a);
                }
                i=1;
            }
            catch
            {
                i = 0;
            }

            return i;
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            int i = judge_number(textBox6.Text,"double");
            if (textBox6.Text == "")
            {
                label19.Text = "禁止为空";
            }
            else if (i == 1)
            {
                label19.Text = "";
            }
            else
            {
                label19.Text = "必须为数字";
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            int i = judge_number(textBox8.Text, "int");
            if (textBox8.Text == "")
            {
                label21.Text = "禁止为空";
            }
            else if (i == 1)
            {
                label21.Text = "";
            }
            else
            {
                label21.Text = "必须为数字";
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            int i = judge_number(textBox9.Text, "int");
            if (textBox8.Text == "")
            {
                label22.Text = "禁止为空";
            }
            else if (i == 1)
            {
                label22.Text = "";
            }
            else
            {
                label22.Text = "必须为数字";
            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if(textBox10.Text=="")
            {
                label23.Text = "禁止为空";
            }
            else
            {
                label23.Text = "";
            }
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
            {
                label24.Text = "禁止为空";
            }
            else
            {
                label24.Text = "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool a = Judge_bianhao(textBox4.Text);
            if(a==false)
            {
                MessageBox.Show(textBox4.Text+"编号已存在！", "提示", MessageBoxButtons.OK);
            }
            else
            {
                DateBase date = new DateBase();
                string time = DateTime.Now.ToString();
                string sqlstring4 = "insert into Library_Book_Change(图书编号,图书名称,行为,时间,数量,操作人员,人员ID) values('" + textBox4.Text.ToString() + "','" + textBox3.Text.ToString() + "','添加','"+time+"','" + textBox8.Text.ToString() + "','" + value[5].ToString() + "','" + value[4].ToString() + "')";
                string sqlstring1 = "insert into Library_Book(编号,类别,图书名称,单价,分类号,库存数量,剩余库存数量,可借数量,剩余可借数量,出版社,馆藏位置,简介) values('" + textBox4.Text.ToString() + "','" + textBox7.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox8.Text.ToString() + "','" + textBox8.Text.ToString() + "','" + textBox9.Text.ToString() + "','" + textBox9.Text.ToString() + "','" + textBox10.Text.ToString() + "','" + textBox11.Text.ToString() + "','" + richTextBox1.Text.ToString() + "')";
                MessageBox.Show(sqlstring4);
                int i = date.ExecuteSQL(sqlstring1);
                if (i == 1)
                {                    
                    i = date.ExecuteSQL(sqlstring4);
                    if(i==1)
                    {
                        MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK);
                }
            }
        }
        private bool Judge_bianhao(string a)
        {
            DateBase date = new DateBase();
            string sqlstring = "select 单价 from Library_Book where 编号='" + a + "'";
            int m = date.GetdataRow(sqlstring);
            if(m>0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public void Fresh1(string a)
        {
            DateBase date = new DateBase();
            date.open();
            SqlCommand cmd = new SqlCommand(a, date.connection);
            sda1.SelectCommand = cmd;
            sda1.Fill(dt1);
            dataGridView2.DataSource = dt1;
            dataGridView2.ReadOnly = false;
            foreach (DataGridViewColumn c in dataGridView2.Columns)
            {
                if (c.Index != 3)
                {
                    c.ReadOnly = true;
                }
            }
        }
        public void test1()
        {
            dt1.Clear();
            dataGridView2.DataSource = dt1;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox12.Text == "")
            {
                MessageBox.Show("内容禁止为空", "警告", MessageBoxButtons.OK);
            }
            else
            {
                string sqlstring1 = "select 编号,图书名称,类别,单价,分类号,出版社,库存数量,馆藏位置 from Library_Book where 编号='" + textBox12.Text.ToString() + "'";
                test1();
                Fresh1(sqlstring1);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox13.Text == "")
            {
                MessageBox.Show("内容禁止为空", "警告", MessageBoxButtons.OK);
            }
            else
            {
                string sqlstring2 = "select 编号,图书名称,类别,单价,分类号,出版社,库存数量,馆藏位置 from Library_Book where 图书名称 like '%" + textBox13.Text.ToString() + "%'";
                test1();
                Fresh1(sqlstring2);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommandBuilder scb1 = new SqlCommandBuilder(sda1);
                sda1.Update(dt1);
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK);
            }
            catch (Exception man)
            {
                MessageBox.Show(man.ToString());
            }
        }
    }
}
