using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using diguiTree.TreeHelp;

namespace diguiTree.TreeHelp
{
    public class diguiClass
    {
        //设置固定名称的、存放文件的文件夹
        public static string file_1 = "712_项目文件";
        public static string file_2 = "712_项目结论";
       

        //获取当前文件夹根目录路径
        public static string getRoot()
        {
            return Directory.GetCurrentDirectory();
        }
        //获取项目文件夹
        public static string getXiangmuRoot()
        {
            return getRoot() + "\\" + file_1;
        }
        //获取结论文件夹路径
        //获取文件简介文件夹路径
        
        //递归读取文件名
        //public static List<TreeClass<string>> GetTrees()
        //{
        //    List<TreeClass<string>> files = new List<TreeClass<string>>();
        //    //获取文件夹下所有文件
        //    return files;
        //}

        //public static TreeClass<string> Getsons(string path, int level)
        //{
        //    var files = new List<FileInfo>();
        //    TreeClass<string> treeClass = new TreeClass<string>(0);
        //    var allFiles = new DirectoryInfo(path).GetFiles();//获取文件夹下所有文件
        //    while (allFiles != null)
        //    {
        //        foreach (var item in allFiles)
        //        {
        //            TreeClass<string> treeClassesItem = new TreeClass<string>(level + 1, item.FullName, item.Name);
        //            treeClass.AddNode(treeClassesItem);
        //        }
        //        treeClass.AddNode(treeClass);
        //    }
        //    return treeClass;
        //}
        public static TreeClass<string> GetAllFiles(string path, int level)
        {

            string path1 = Directory.GetParent(path).FullName;
            string nameof = path.Replace(path1, "");
            string name = nameof.Replace("/", "");
            string name1 = name.Replace("\\", "");
            TreeClass<string> treeClasses = new TreeClass<string>(level, path, name1);
            var allFiles = new DirectoryInfo(path).GetFiles();//获取文件夹下所有文件

            foreach (var item in allFiles)
            {
                TreeClass<string> treeClassesItem = new TreeClass<string>(level + 1, item.FullName, item.Name);
                treeClasses.AddNode(treeClassesItem);
            }
            var tmpdics = new DirectoryInfo(path).GetDirectories();//获取文件夹下所有子文件夹
            foreach (var dic in tmpdics)
            {
                List<TreeClass<string>> treeClassesI = new List<TreeClass<string>>();
                ///treeClasses.node_name = dic.FullName;

                treeClassesI.Add(diguiClass.GetAllFiles(dic.FullName, level + 1));
                treeClasses.AddNodes(treeClassesI);
            }
            return treeClasses;
        }
    }
}
