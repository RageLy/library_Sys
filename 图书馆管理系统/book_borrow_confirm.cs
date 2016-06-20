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
    public partial class book_borrow_confirm : Form
    {
        public book_borrow_confirm()
        {
            InitializeComponent();
        }
        BooksBroow book = new BooksBroow();
        string[] value=new string[15];

        private void book_borrow_confirm_Load(object sender, EventArgs e)
        {
            value = book.trans_Value();

            DateBase date = new DateBase();
            string sqlstring = "select 图书名称,分类号,类别,出版社,单价 from Library_Book where 编号 ='" + value[1] + "'";
            DataSet DS = new DataSet();
            DS = date.GetDataSet(sqlstring, value[2]);
            dataGridView1.DataSource = DS;
            dataGridView1.DataMember = value[2];           

            label2.Text = value[0];


        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确认借书按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("请输入密码！", "提示", MessageBoxButtons.OK);
            }
            else
            {
                DateBase Date=new DateBase();
                string SqlString = "select * from Library_Login where 用户名='"+value[0]+"' and 密码='"+textBox1.Text+"'";
                SqlDataReader dr;
                dr = Date.GetDataReader(SqlString);
                if (dr.Read())
                {
                    value[10]=dr[0].ToString();
                    Creat_ID_Re cr = new Creat_ID_Re();
                    value[9] = cr.GetFormCode(2, 5);            //2是指示借书，5代表字符串的长度
                    string time=DateTime.Now.ToString();
                    string sqlstring1 = "insert into Library_Borrow(单号,图书名称,ID,编号,分类号,类别,借阅时间,借阅时长,是否违约,操作员) values('" + value[9] + "','" + value[8] + "','" + value[10] + "','" + value[1] + "','" + int.Parse(value[4]) + "','" + value[6] + "','" + time + "','30','否','" + value[0] + "')";
                    int j=int.Parse(value[3])-1;
                    string sqlstring2 = "update Library_Book set 剩余可借数量= '" + j + "' where 编号 ='" + value[1] + "'";
                    value[11] = Judeg_class(value[4]);
                    j = int.Parse(value[7]) + 1;
                    string sqlstring3 = "update Library_User_Inf set "+value[11]+"= '" + j + "' where ID ='" + value[10] + "'";
                    int i = Date.ExecuteSQL(sqlstring1);
                    if (i == 1)
                    {
                        i = Date.ExecuteSQL(sqlstring2);
                        if (i == 1)
                        {
                            i = Date.ExecuteSQL(sqlstring3);
                            if (i == 1)
                            {
                                MessageBox.Show("借书成功", "提示", MessageBoxButtons.OK);
                                this.Close();
                            }
                            else
                            {                       
                                MessageBox.Show("借书失败,3", "警告", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show("借书失败,2", "警告", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("借书失败,1", "警告", MessageBoxButtons.OK);
                    }
                    
                }
                else
                {
                    MessageBox.Show("密码错误！", "提示", MessageBoxButtons.OK);
                }
            }

        }
        public string Judeg_class(string m)
        {
            string a = "";
            switch(m)
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
    }
}
