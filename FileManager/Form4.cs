using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace FileManager
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username, password;
            username = textBox1.Text;
            password = textBox2.Text;

            if (textBox1.Text != "" && textBox2.Text != "")
            {
                Random rd = new Random();
                string mdfPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"database\712.mdf");
                string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + mdfPath + ";Integrated Security=True;Connect Timeout=30;ApplicationIntent=ReadWrite";
                SqlConnection connect = new SqlConnection(conn);  //创建一个数据库连接实例
                connect.Open(); //打开数据库 
                String sql = "INSERT INTO [login]([user],[password]) VALUES('" + username + "','" + password + "')";//SQL语句向表中写入数据
                SqlCommand sqlCommand = new SqlCommand(sql, connect);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("注册成功");
            }
            else
            {
                MessageBox.Show("请输入有效的注册信息");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text="";
            textBox2.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
