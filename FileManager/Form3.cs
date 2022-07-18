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
using System.Collections;
using System.IO;

namespace FileManager
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    String username, password, Id;
        //    username = textBox1.Text;
        //    password = textBox2.Text;
        //    Random rd = new Random();
        //    string mdfPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"database\712.mdf");
        //    string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + mdfPath + ";Integrated Security=True;Connect Timeout=30;ApplicationIntent=ReadWrite";
        //    SqlConnection connect = new SqlConnection(conn);  //创建一个数据库连接实例
        //    connect.Open(); //打开数据库 

        //    String sql = "INSERT INTO [login]([user],[password]) VALUES('" + username + "','" + password + "')";//SQL语句向表中写入数据
        //    SqlCommand sqlCommand = new SqlCommand(sql, connect);
        //    sqlCommand.ExecuteNonQuery();
        //    MessageBox.Show("注册成功");
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            String username, password;
            username = textBox1.Text;
            password = textBox2.Text;
            string mdfPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"database\712.mdf");
            string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + mdfPath + ";Integrated Security=True;Connect Timeout=30;ApplicationIntent=ReadWrite";
            SqlConnection connect = new SqlConnection(conn);  //创建一个数据库连接实例
            connect.Open(); //打开数据库 
            String sql = "select [user],[password] from [login] where [user]='" + username + "'and [password]='" + password + "'";//SQL语句实现表数据的读取
            SqlCommand sqlCommand = new SqlCommand(sql, connect);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)//满足用户名与密码一致，进入下一个界面
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
                this.Dispose();
            }
            else//如果登录失败，询问是否注册新用户
            {
                DialogResult dr = MessageBox.Show("登录失败，请再次尝试");
            }
        }

        //使背景加载时不闪烁
        protected override CreateParams CreateParams 
        {
            get
            {
                CreateParams paras = base.CreateParams;
                paras.ExStyle |= 0x02000000;
                return paras;
            }
        }



        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
