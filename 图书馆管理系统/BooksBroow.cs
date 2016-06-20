using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 图书馆管理系统
{
    public partial class BooksBroow : Form
    {
        public BooksBroow()
        {
            InitializeComponent();
        }

        public static int[] mark = new int[10];
        public static int[] number_ = new int[10];
        public static string[] recode = new string[10];

        private void BooksBroow_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            button6.Text = "确认";
            dataGridView1.RowHeadersVisible = false;
        }
        /// <summary>
        /// 输入用户名后的借书选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            button6.Visible = false;
            button4.Enabled = true;
            button4.Visible = true;
            panel2.Visible = false;
            int state = 0;
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入用户名！", "提示", MessageBoxButtons.OK);
            }
            else
            {
                recode[0] = textBox1.Text.ToString();
                DateBase date = new DateBase();
                string sqlstring = "select * from Library_User_Inf where 用户名='" + textBox1.Text.ToString() + "'";
                SqlDataReader dr = date.GetDataReader(sqlstring);
                if (dr.Read())
                {
                    state = int.Parse(dr[3].ToString());
                    int temp = 0;
                    number_[8] = int.Parse(dr[10].ToString());
                    number_[0] = int.Parse(dr[8].ToString());
                    number_[1] = int.Parse(dr[9].ToString());
                    number_[2] = int.Parse(dr[11].ToString());
                    number_[3] = int.Parse(dr[12].ToString());
                    number_[4] = int.Parse(dr[13].ToString());
                    number_[5] = int.Parse(dr[14].ToString());
                    number_[6] = int.Parse(dr[15].ToString());
                    number_[7] = int.Parse(dr[16].ToString());
                    if (state == 0)
                    {
                        MessageBox.Show("名为" + textBox1.Text.ToString() + "的用户已经被停用", "警告", MessageBoxButtons.OK);
                    }
                    else if ((number_[0] - number_[1] + number_[2] - number_[3] + number_[4] - number_[5] + number_[6] - number_[7]) < 1)
                    {
                        MessageBox.Show("名为" + textBox1.Text.ToString() + "的用户借书已达到上限", "提示", MessageBoxButtons.OK);
                    }
                    else
                    {
                        temp = number_[0] - number_[1];
                        label4.Text = temp.ToString();
                        temp = number_[2] - number_[3];
                        label6.Text = temp.ToString();
                        temp = number_[4] - number_[5];
                        label8.Text = temp.ToString();
                        temp = number_[6] - number_[7];
                        label10.Text = temp.ToString();
                        panel1.Visible = true;
                        panel3.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("没有名为" + textBox1.Text.ToString() + "此用户", "提示", MessageBoxButtons.OK);
                }
            }
        }
        /// <summary>
        /// 按照编号查询剩余书籍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            DateBase date = new DateBase();
            string sqlstring = "select 图书名称,分类号,类别,剩余可借数量 from Library_Book where 编号 ='" + textBox2.Text.ToString() + "'";
            recode[2] = "Library_Book";
            DataSet DS = new DataSet();           
            DS = date.GetDataSet(sqlstring,recode[2]);
            if (DS.Tables[0] == null || DS.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("没有编号为" + textBox2.Text.ToString() + "的书籍信息", "提示", MessageBoxButtons.OK);
            }
            else
            {
                mark[0] = int.Parse(DS.Tables[0].Rows[0]["剩余可借数量"].ToString());
                mark[1] = int.Parse(DS.Tables[0].Rows[0]["分类号"].ToString());
                recode[3] = DS.Tables[0].Rows[0]["类别"].ToString();
                recode[1] = textBox2.Text.ToString();
                recode[4] = DS.Tables[0].Rows[0]["图书名称"].ToString();
                dataGridView1.DataSource = DS;
                dataGridView1.DataMember = recode[2];
                panel2.Visible = true;
            }
        }
        /// <summary>
        /// 确认借出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            mark[2] = number_[mark[1] * 2 - 2] - number_[mark[1] * 2 - 1];    //判断该类书是否可以借
            mark[3] = number_[mark[1] * 2 - 1];                               //读取该类已借的数量
            if(mark[0]>0&&mark[2]>0) 
            {
                book_borrow_confirm f1 = new book_borrow_confirm();
                f1.Show();
            }
            else if(mark[2]==0)
            {
                MessageBox.Show("您对该类书籍的借阅已经达到上限！", "提示", MessageBoxButtons.OK);
            }
            else if(mark[3]==0)
            {
                MessageBox.Show("该类书籍的借阅已经达到上限！", "提示", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string[] trans_Value()
        {
            string[] value = new string[15];
            value[0] = recode[0];
            value[1] = recode[1];
            value[2] = recode[2];
            value[3] = mark[0].ToString();
            value[4] = mark[1].ToString();
            value[5] = mark[2].ToString();
            value[6] = recode[3];
            value[7] = mark[3].ToString();
            value[8] = recode[4];
            return value;
        }
        /// <summary>
        /// 还书按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button4.Visible = false;
            button6.Enabled = true;
            button6.Visible = true;
            panel1.Visible = false;

            if(textBox1.Text=="")
            {
                MessageBox.Show("请输入用户名！", "提示", MessageBoxButtons.OK);
            }
            else
            {
                int state=0;
                int temp=0;
                DateBase date = new DateBase();
                string sqlstring = "select * from Library_User_Inf where 用户名='" + textBox1.Text.ToString() + "'";
                SqlDataReader dr = date.GetDataReader(sqlstring);
                if (dr.Read())
                {
                    state = int.Parse(dr[3].ToString());
                    number_[8] = int.Parse(dr[10].ToString());
                    number_[0] = int.Parse(dr[8].ToString());
                    number_[1] = int.Parse(dr[9].ToString());
                    number_[2] = int.Parse(dr[11].ToString());
                    number_[3] = int.Parse(dr[12].ToString());
                    number_[4] = int.Parse(dr[13].ToString());
                    number_[5] = int.Parse(dr[14].ToString());
                    number_[6] = int.Parse(dr[15].ToString());
                    number_[7] = int.Parse(dr[16].ToString());
                    if (state == 0)
                    {
                        MessageBox.Show("名为" + textBox1.Text.ToString() + "的用户已经被停用", "警告", MessageBoxButtons.OK);
                    }
                    else
                    {
                        temp = 100;
                    }
                }
                else
                {
                    MessageBox.Show("没有名为" + textBox1.Text.ToString() + "此用户", "提示", MessageBoxButtons.OK);
                }
                if(temp==100)
                {
                    temp = number_[0] - number_[1];
                    label4.Text = temp.ToString();
                    temp = number_[2] - number_[3];
                    label6.Text = temp.ToString();
                    temp = number_[4] - number_[5];
                    label8.Text = temp.ToString();
                    temp = number_[6] - number_[7];
                    label10.Text = temp.ToString();
                    panel3.Visible = true;

                    int id = int.Parse(dr[0].ToString());
                    mark[5] = id;
                    string sqlstring1 = "select 图书名称,类别,借阅时间,是否违约 from Library_Borrow where ID ='" + id + "'";
                    recode[2] = "Library_Borrow";
                    DataSet DS = new DataSet();
                    DS = date.GetDataSet(sqlstring1, recode[2]);
                    if (DS.Tables[0] == null || DS.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("用户名为 " + textBox2.Text.ToString() + " 的账户没有借阅信息", "提示", MessageBoxButtons.OK);
                    }
                    else
                    {
                        dataGridView1.DataSource = DS;
                        dataGridView1.DataMember = recode[2];
                        panel2.Visible = true;
                    }
                }
            }
            
        }
        /// <summary>
        /// 确认还书按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            recode[6] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["图书名称"].Value.ToString();
            
        }
        /// <summary>
        /// 防止窗口多次打开
        /// </summary>
        private static BooksBroow bookborrow1 = new BooksBroow();
        public static BooksBroow getBookBorrow()
        {
            if(bookborrow1.IsDisposed)
            {
                bookborrow1 = new BooksBroow();
                return bookborrow1;
            }
            else
            {
                
                return bookborrow1;
            }
        }
    }
}
