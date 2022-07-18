using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using diguiTree.TreeHelp;
using static FileManager.Form2;
using System.Data.SqlClient;




namespace FileManager
{
    public partial class Form1 : Form
    {
        private static string global_str;
        private static string global_str2;///上一个点击的节点，路径
        private static string global_str3;///当前点击的节点，路径
        private static string global_str4;///搜索栏的路径
        
        public Form1()
        {
            InitializeComponent();
            textBox2.Visible = true;
            panel1.Visible = true;
            splitContainer4.Visible = true;
            splitContainer5.Visible = true;
            webBrowser1.Visible = true;
            splitContainer6.Visible = false;
            splitContainer7.Visible = false;
            splitContainer8.Visible = false;
            splitContainer9.Visible = false;
            webBrowser2.Visible = false;
            webBrowser3.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //直接调用 button1_Click，模拟鼠标点击button1
            button1_Click(button1, null);
            //treeView1.SelectedNode.Remove();
            ///button1.PerformClick();
            // 注意！
            // 调用button1.PerformClick()也可以模拟鼠标点击button1

        }

        string abc = diguiClass.getRoot(); //项目根目录      
        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }
        /// <summary>
        /// 文件对比显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool sign1 = true;
        private void button2_Click(object sender, EventArgs e)
        {
            ///显示调用对比显示窗口
            if (sign1)
            {
                label1.Text = "";
                label3.Text = "";
                splitContainer6.Visible = true;
                splitContainer7.Visible = true;
                splitContainer8.Visible = true; 
                splitContainer9.Visible = true;
                webBrowser1.Visible = false;              
                webBrowser2.Visible = true;
                webBrowser3.Visible = true;
                sign1 = false;
                webBrowser1.DocumentText = "";
                textBox2.Text = "";
                ///显示单栏显示打开的文件，即用于对比的文件
                textBox3.Text = "";
                webBrowser2.DocumentText = "";
                webBrowser2.Navigate(global_str2);
                label1.Visible = true;
                label1.Text = global_str2.Substring(global_str2.LastIndexOf("\\") + 1);
                New_txt(global_str2, textBox2);//创建访问txt
            }
            ///显示调用单栏显示窗口，隐藏对比显示窗口
            else
            {
                splitContainer6.Visible = false;
                splitContainer7.Visible = false;
                splitContainer8.Visible = false;
                splitContainer9.Visible = false;
                webBrowser1.Visible = true;
                webBrowser2.Visible = false;                
                webBrowser3.Visible = false;           
                sign1 = true;
                webBrowser2.DocumentText = "";
                webBrowser3.DocumentText = "";
                textBox3.Text = "";
                textBox4.Text = "";
                //关闭上一次对比显示打开的web，即上次对比用到的文件
                global_str2 = "";
            }
        }

        /// <summary>
        /// 结论txt保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            ///判空
            if ((global_str == null ) &&( (global_str2 == null )||(global_str3 == null)))
            {
                MessageBox.Show("请选中节点文件，再进行结论编辑与保存");
            } 
            //点击树状节点
            if ((webBrowser1.Visible == true)&& (global_str!=null))
            {
                string fsPath0 = global_str.Split('.')[0] + ".txt";
                saveText(fsPath0);    
            }
            if ((webBrowser2.Visible == true) && (global_str2 != null))
            {
                string fsPath = global_str2.Split('.')[0] + ".txt";
                saveTextbox3(fsPath, label1.Text);
            }
            if ((webBrowser3.Visible == true) && (global_str3 != null))
            {
                string fsPath2 = global_str3.Split('.')[0] + ".txt";
                saveTextbox4(fsPath2, label3.Text);
            }
            ////点击搜索下拉栏,不支持对比显示
        }
        ///保存textbox中内容,可编辑区编辑内容的保存
        private void saveText(String SavePath)
        {
            string s1 = SavePath;
            string s2 = string.Empty;
            string s3 = string.Empty;
            //先求出最后出现这个字符的下标
            int index = s1.LastIndexOf('\\');
            //从下一个索引开始截取
            s2 = s1.Substring(index + 1);
            s3 = s1.Replace(s2, "");
            if (webBrowser1.Visible == true)
                {
                    string name = this.textBox2.Text.Trim();///去掉字符串两边空白
                    if (name != string.Empty)
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "(*.txt)|*.txt|(*.*)|*.*";
                        saveFileDialog.FileName = s2;
                        saveFileDialog.InitialDirectory = s3;
                        //将日期时间作为文件名
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false);
                            streamWriter.Write(this.textBox2.Text);
                            saveFileDialog.RestoreDirectory = true;
                            streamWriter.Close();
                            MessageBox.Show("文件保存成功！");
                        }
                    }
                }
        }
        private void saveTextbox3(String SavePath,String filename)
        {
            string s1 = SavePath;
            string s2 = filename.Split('.')[0];
            string s3 = string.Empty;
            //先求出最后出现这个字符的下标
            int index = s1.LastIndexOf('\\');
            //从下一个索引开始截取，获取保存文件名
            s2 = s1.Substring(index + 1);
            //获取保存路径
            s3 = s1.Replace(s2, "");

            if (webBrowser2.Visible == true)
            {
                string name = this.textBox3.Text.Trim();
                if (name != string.Empty)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "(*.txt)|*.txt|(*.*)|*.*";
                    saveFileDialog.FileName = s2;
                    saveFileDialog.InitialDirectory = s3;
                    //将日期时间作为文件名
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false);
                        streamWriter.Write(this.textBox3.Text);
                        saveFileDialog.RestoreDirectory = true;
                        streamWriter.Close();
                        MessageBox.Show("文件保存成功！");
                    }
                }
            }
        }
        private void saveTextbox4(String SavePath, String filename)
        {
            string s1 = SavePath;
            string s2 = filename.Split('.')[0];
            string s3 = string.Empty;
            //先求出最后出现这个字符的下标
            int index = s1.LastIndexOf('\\');
            //从下一个索引开始截取，获取保存文件名
            s2 = s1.Substring(index + 1);
            //获取保存路径
            s3 = s1.Replace(s2, "");

            if (webBrowser2.Visible == true)
            {
                string name = this.textBox4.Text.Trim();
                if (name != string.Empty)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "(*.txt)|*.txt|(*.*)|*.*";
                    saveFileDialog.FileName = s2;
                    saveFileDialog.InitialDirectory = s3;
                    //将日期时间作为文件名
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false);
                        streamWriter.Write(this.textBox4.Text);
                        saveFileDialog.RestoreDirectory = true;
                        streamWriter.Close();
                        MessageBox.Show("文件保存成功！");
                    }
                }
            }
        }

        bool isMouseClick = true;
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
                string p1 = e.Node.Text;
                string p2 = string.Empty;
                int index = p1.LastIndexOf('.');
                p2 = p1.Substring(index + 1);
                int p3 = e.Node.Level;
                if (isMouseClick)
                {
                    treeView1.SelectedNode = e.Node;
                }
                ////单form显示结论报告
                //if (p2 == "doc" || p2 == "pdf" || p2 == "docx" || p2 == "xlsx") 
                //{
                //    Program.global_str5 = e.Node.Name;
                //    Form2 form2 = new Form2();
                //    form2.Show();
                //}
                ///单栏显示
                if (splitContainer6.Visible == false ) 
                {
                    One_Web(e);
                    Show_Edit(e, webBrowser1);
                }
                ///双栏显示     
                if (splitContainer6.Visible == true && splitContainer9.Visible== true && p2 != "doc" && p2 != "pdf" && p2 != "docx" && p2 != "xlsx")  
                {
                    Two_Web(e);
                    Show_Edit(e, webBrowser3);
                }
                global_str = e.Node.Name;
        }

        /// <summary>
        ///单栏显示
        /// </summary>
        /// <param name="p2"></param>
        /// <param name="e"></param>
        private void One_Web( TreeViewEventArgs e)
        {
            string p1 = e.Node.Text;
            string p2 = string.Empty;
            int index = p1.LastIndexOf('.');
            p2 = p1.Substring(index + 1);
            if (p2 == "txt")
            {
                FileStream fs = new FileStream(e.Node.Name, FileMode.Open);//创建文件流
                StreamReader reader = new StreamReader(fs);//创建指定的读取器
                string s = reader.ReadToEnd();//读取文件文件中的内容(文件不大可以采用这种方式)
                textBox2.Text = s;//将读到的内容赋给textBox2的文本属性
                fs.Close();
                reader.Close();
            }
            else
            {
                textBox2.Text = "";
                string Spath = abc + "\\" + e.Node.FullPath;
                webBrowser1.Navigate(Spath);
                global_str2 = Spath;
                string fsPath = Spath.Split('.')[0] + ".txt";

                // 如果文件不存在
                if (p2 != "doc" && p2 != "docx" && p2 != "pdf" && p2 != "xlsx" && p1.Contains('.'))
                {
                    if (!System.IO.File.Exists(fsPath))
                    {
                        //创建txt
                        StreamWriter sw = new StreamWriter(fsPath);
                        sw.WriteLine("请输入试验、仿真条件，结论！");
                        sw.Close();
                    }
                    else
                    {
                        FileStream fs = new FileStream(fsPath, FileMode.Open);//创建文件流
                        StreamReader reader = new StreamReader(fs);//创建指定的读取器
                        string s = reader.ReadToEnd();//读取文件文件中的内容(文件不大可以采用这种方式)
                        textBox2.Text = s;//将读到的内容赋给textBox2的文本属性
                        fs.Close();
                        reader.Close();
                    }
                }
            }

        }
        /// <summary>..............................................................................
        ///双栏显示
        /// </summary>
        /// <param name="p2"></param>
        /// <param name="e"></param> 
        private void Two_Web(TreeViewEventArgs e)
        {
                string p1 = e.Node.Text;
                string p2 = string.Empty;
                int index = p1.LastIndexOf('.');
                p2 = p1.Substring(index + 1);
                if (p2 == "txt")
                {
                    FileStream fs = new FileStream(e.Node.Name, FileMode.Open);//创建文件流
                    StreamReader reader = new StreamReader(fs);//创建指定的读取器
                    string s = reader.ReadToEnd();//读取文件中的内容(文件不大可以采用这种方式)
                    textBox4.Text = s;//将读到的内容赋给textBox2的文本属性
                    fs.Close();
                    reader.Close();
                }
                else
                {
                    textBox4.Text = "";
                    webBrowser3.DocumentText = "";
                    string Spath = abc + "\\" + e.Node.FullPath;
                    webBrowser3.Navigate(Spath);
                    ///webBrowser3.Navigate(e.Node.Name);
                    string fsPath = Spath.Split('.')[0] + ".txt";
                    label3.Visible = true;
                    label3.Text = Spath.Substring(Spath.LastIndexOf("\\") + 1);
                    global_str3 = Spath;                   
                    if (p2 != "doc" && p2 != "docx" && p2 != "pdf" && p2 != "xlsx" && p1.Contains('.'))
                    {
                        if (!System.IO.File.Exists(fsPath))
                        {
                            //创建txt
                            StreamWriter sw = new StreamWriter(fsPath);
                            sw.WriteLine("请输入试验、仿真条件，结论！");
                            sw.Close();
                        }
                        else
                        {
                            FileStream fs = new FileStream(fsPath, FileMode.Open);//创建文件流
                            StreamReader reader = new StreamReader(fs);//创建指定的读取器
                            string s = reader.ReadToEnd();//读取文件文件中的内容(文件不大可以采用这种方式)
                            textBox4.Text = s;//将读到的内容赋给textBox2的文本属性
                            fs.Close();
                            reader.Close();
                        }
                    }
                }

            //}

        }
        /// <summary>
        ///创建/访问txt
        /// </summary>
        private void New_txt(string path, TextBox TextBox)
        {
            string p1 = path;
            string p2 = string.Empty;
            int index = p1.LastIndexOf('.');
            p2 = p1.Substring(index + 1);
            if (p2 == "txt")
            {
                FileStream fs = new FileStream(path, FileMode.Open);//创建文件流
                StreamReader reader = new StreamReader(fs);//创建指定的读取器
                string s = reader.ReadToEnd();//读取文件文件中的内容(文件不大可以采用这种方式)
                textBox3.Text = s;//将读到的内容赋给textBox2的文本属性
                fs.Close();
                reader.Close();
            }
            else
            {
                textBox3.Text = "";
                webBrowser2.DocumentText = "";
                webBrowser2.Navigate(path);
                string fsPath = path.Split('.')[0] + ".txt";
                label1.Visible = true;
                label1.Text = path.Substring(path.LastIndexOf("\\") + 1);
                global_str2 = path;
                if (p2 != "doc" && p2 != "docx" && p2 != "pdf" && p2 != "xlsx" && p1.Contains('.'))
                {
                    if (!System.IO.File.Exists(fsPath))
                    {
                        //创建txt
                        StreamWriter sw = new StreamWriter(fsPath);
                        sw.WriteLine("请输入试验、仿真条件，结论！");
                        sw.Close();
                    }
                    else
                    {
                        FileStream fs = new FileStream(fsPath, FileMode.Open);//创建文件流
                        StreamReader reader = new StreamReader(fs);//创建指定的读取器
                        string s = reader.ReadToEnd();//读取文件文件中的内容(文件不大可以采用这种方式)
                        textBox3.Text = s;//将读到的内容赋给textBox2的文本属性
                        fs.Close();
                        reader.Close();
                    }
                }
            }

        }

        /// <summary>
        /// 可编辑区显示项目信息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="web"></param>
        private void Show_Edit(TreeViewEventArgs e,WebBrowser web)
        {           
            if (e.Node.Level == 1||e.Node.Level==2)
            {
                ///创建并显示txt
                textBox5.Text = "";
                string Spath = abc + "\\" + e.Node.FullPath;
                TxtClass.GetTxt(Spath);
                FileStream fs = new FileStream("fileList.txt", FileMode.Open);//创建文件流

                StreamReader reader = new StreamReader(fs);//创建指定的读取器
                string s = reader.ReadToEnd();//读取文件文件中的内容(文件不大可以采用这种方式)
                textBox5.Text = s;//将读到的内容赋给textBox2的文本属性
                fs.Seek(0, SeekOrigin.Begin);///txt清空
                fs.SetLength(0);
                fs.Close();
                reader.Close();
                web.Navigate(Spath);
                return;
            }

        }


        /// <summary>
        ///加载文件到trewview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //splitContainer6.Visible = true;
            //splitContainer7.Visible = true;
            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    webBrowser1.Navigate(ofd.FileName);
            //}
            //ReadTXT(textBox2);
            treeView1.Nodes.Clear();///清除树节点
            //设置树形组件的基础属性
            treeView1.CheckBoxes = false;
            treeView1.FullRowSelect = true;
            treeView1.Indent = 10;
            treeView1.ItemHeight = 30;
            treeView1.LabelEdit = false;
            treeView1.Scrollable = true;
            treeView1.ShowPlusMinus = true;
            treeView1.ShowRootLines = true;
            treeView1.HideSelection = false;
            var Data = diguiClass.GetAllFiles(diguiClass.getXiangmuRoot(), 0);
            ///if (Data.Parent == null) ///创建根节点
            ///{
            TreeNode tn = new TreeNode();
            tn.Name = Data.node_data;
            tn.Text = Data.node_name;
            tn.Tag = Data.node_level;
            foreach (var item in Data.Sons)
            {
                TreeNode node = new TreeNode();
                node.Text = item.node_name;
                node.Tag = item.node_level;
                tn.Nodes.Add(node);
                if (item.Sons.Count > 0)
                {

                    foreach (var i in item.Sons)
                    {
                        TreeNode childnode = new TreeNode();
                        childnode.Name = i.node_data;
                        childnode.Text = i.node_name;
                        childnode.Tag = i.node_level;
                        node.Nodes.Add(childnode);
                        if (i.Sons.Count > 0)

                        {
                            foreach (var dr in i.Sons)
                            {
                                TreeNode dnode = new TreeNode();
                                dnode.Name = dr.node_data;
                                dnode.Text = dr.node_name;
                                dnode.Tag = dr.node_level;
                                childnode.Nodes.Add(dnode);
                            }
                        }
                    }
                }
            }
            treeView1.Nodes.Add(tn);
            treeView1.ExpandAll();
            treeView1.SelectedNode = tn;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ///选中或勾选树节点时触发子树节点或父树节点的逻辑操作
            isMouseClick = false;
            SetCheckedChildNodes(e.Node, e.Node.Checked);
            SetCheckedParentNodes(e.Node, e.Node.Checked);
            isMouseClick = true;

        }
        private static void SetCheckedParentNodes(TreeNode tn, bool CheckState)
        {
            if (tn.Parent != null)
            {
                ///当选中树节点勾选后同级所有树节点都勾选时，父树节点为勾选状态；
                ///当选中树节点中的同级树节点其中有一个树节点未勾选则父树节点为未勾选状态；
                bool b = false;
                for (int i = 0; i < tn.Parent.Nodes.Count; i++)
                {
                    bool state = tn.Parent.Nodes[i].Checked;
                    if (!state.Equals(CheckState))
                    {
                        b = !b;
                        break;
                    }
                }
                tn.Parent.Checked = b ? (Boolean)false : CheckState;
                SetCheckedParentNodes(tn.Parent, CheckState);
            }
        }
        //树节点的子树节点逻辑操作
        private static void SetCheckedChildNodes(TreeNode tn, bool CheckState)
        {
            if (tn.Nodes.Count > 0)
            {
                //当前树节点状态变更，子树节点同步状态
                foreach (TreeNode tn1 in tn.Nodes)
                {
                    tn1.Checked = CheckState;

                    SetCheckedChildNodes(tn1, CheckState);
                }
            }
        }
        //定义TreeView控件失去焦点后，仍然高亮显示被选中的节点
        //失去焦点时
        private void treeView1_Leave(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                //让选中项背景色呈现黄色
                treeView1.SelectedNode.BackColor = Color.CornflowerBlue;
                //前景色为白色
                treeView1.SelectedNode.ForeColor = Color.White;
            }
        }

        //将要选中新节点之前发生
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                //将上一个选中的节点背景色还原（原先没有颜色）
                treeView1.SelectedNode.BackColor = Color.Empty;
                //还原前景色
                treeView1.SelectedNode.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// 搜索功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool sign3 = true;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fsPath1 = abc + "\\" + diguiClass.file_1 + comboBox1.Text.Split('.')[0] + ".txt";
            string fsPath = abc + "\\" + diguiClass.file_1 + comboBox1.Text;
            global_str4 = fsPath;
            int index = global_str4.LastIndexOf('.');
            string p2 = global_str4.Substring(index + 1);
            ///单栏模式
            if (splitContainer6.Visible == false)
            {

                if (p2 != "doc" && p2 != "docx" && p2 != "pdf" && p2 != "xlsx" && global_str4.Contains('.'))
                {

                    webBrowser1.Navigate(fsPath);///选中某项搜索结果并显示
                    if (!System.IO.File.Exists(fsPath1))
                    {
                        //创建txt
                        StreamWriter sw = new StreamWriter(fsPath1);
                        sw.WriteLine("请输入试验、仿真条件，结论！");
                        sw.Close();
                    }
                    else
                    {
                        FileStream fs = new FileStream(fsPath1, FileMode.Open);//创建文件流
                        StreamReader reader = new StreamReader(fs);//创建指定的读取器
                        string s = reader.ReadToEnd();//读取文件文件中的内容(文件不大可以采用这种方式)
                        textBox2.Text = s;//将读到的内容赋给textBox2的文本属性
                        fs.Close();
                        reader.Close();
                    }
                }

                if (p2 == "doc" || p2 == "pdf" || p2 == "xlsx" || p2 == "docx") //单form显示结论报告
                {
                    Program.global_str5 = global_str4;
                    Form2 form2 = new Form2();
                    form2.Show();
                }
            }
            //双栏模式
            else
            {   
                //如果为双栏，且双栏都为空
                if (splitContainer6.Visible == true) 
                {
                    if (sign3)
                    {
                        Show_Search(global_str4, webBrowser2, fsPath1,label1,textBox3);
                        sign3 = false;
                    }
                    else
                    {
                        Show_Search(global_str4, webBrowser3, fsPath1,label3,textBox4);
                        sign3 = true;
                    }
                }
                //如果为双栏，首栏已有内容,即首栏有文档打开
                if (splitContainer6.Visible == true && webBrowser2.DocumentText != ""&& webBrowser3.DocumentText == "")
                {
                    Show_Search(global_str4, webBrowser3, fsPath1,label3,textBox4);
                }
            }
        }

        private void Show_Search(string path,WebBrowser web,string fsPath1,Label lab,TextBox text)
        {
            int index = global_str4.LastIndexOf('.');
            string p2 = global_str4.Substring(index + 1);
            if (p2 != "doc" && p2 != "docx" && p2 != "pdf" && p2 != "xlsx" && global_str4.Contains('.'))
            {
                web.DocumentText = "";
                web.Navigate(global_str4);
                lab.Visible = true;
                lab.Text = comboBox1.Text.Substring(comboBox1.Text.LastIndexOf("\\") + 1);
                ///sign3 = true;
                if (!System.IO.File.Exists(fsPath1))
                {
                    //创建txt
                    StreamWriter sw = new StreamWriter(fsPath1);
                    sw.WriteLine("请输入试验、仿真条件，结论！");
                    sw.Close();
                }
                else
                {
                    FileStream fs = new FileStream(fsPath1, FileMode.Open);//创建文件流
                    StreamReader reader = new StreamReader(fs);//创建指定的读取器
                    string s = reader.ReadToEnd();//读取文件文件中的内容(文件不大可以采用这种方式)
                    text.Text = s;//将读到的内容赋给textBox2的文本属性
                    fs.Close();
                    reader.Close();
                }

            }
            if (p2 == "doc" || p2 == "pdf" || p2 == "xlsx" || p2 == "docx") //单form显示结论报告
            {
                Program.global_str5 = global_str4;
                Form2 form2 = new Form2();
                form2.Show();
            }
        }

        /// <summary>
        /// 搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string strtxt = comboBox1.Text;
            ///判断搜索栏是否有输入
            if (strtxt == "")
            {
                MessageBox.Show("请输入关键词");
            }
            ///加载搜索结果到下拉栏
            else
            {
                GetFileName(diguiClass.getXiangmuRoot(), strtxt);
                this.comboBox1.DroppedDown = true;
            }
        }

        private void GetFileName(String DirName, String FileName)
        {

            //文件夹信息
            DirectoryInfo dir = new DirectoryInfo(DirName);
            // 如果非根路径且是系统文件则跳过
            if (null != dir.Parent && dir.Attributes.ToString().IndexOf("System") > -1)
            {
                return;
            }
            // 获得所有文件
            try
            {
                FileInfo[] finfo = dir.GetFiles();
                string fname = string.Empty;

                DirectoryInfo[] dinfo = dir.GetDirectories();
                foreach (DirectoryInfo IDir in dinfo)
                {
                    //查找子文件夹中是否有符合要求的文件
                    GetFileName(IDir.FullName, FileName);
                    ///i; //总扫描数量
                }

                foreach (FileInfo Ifinfo in finfo)
                {
                    fname = Ifinfo.Name;
                    
                    //判断文件是否包含查询名
                    if (fname.IndexOf(FileName) > -1)
                    {
                        string aaa = diguiClass.getXiangmuRoot();
                        string fsPath = Ifinfo.FullName.Replace(aaa, "");
                        comboBox1.Items.Add(fsPath);///将文件名加入到下拉栏

                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser2_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Program.global_str5 = e.Node.Name;
            Form2 form2 = new Form2();
            form2.Show();

        }

        private void webBrowser3_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
