using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManager;

namespace FileManager
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }

        

        internal  void ShowF2(Form1 form1)//参数为Form1类 类型参数
        {
            //this.Show();//先把自己show出来
            ///form1;//在这里你就可以使用Form1的东东啦
            ///
            //Form1 f1 = new Form1();
            
            //string g = Program.global_str5;
            //webBrowser1.Navigate(g);
            
        }


        private void Form2_Load(object sender, EventArgs e)
        {
           
            string g = Program.global_str5;
            webBrowser1.Navigate(g);

        }
    }
}
