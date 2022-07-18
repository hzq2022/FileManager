using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diguiTree.TreeHelp
{
    public class TreeClass<T>
    {
        //构造函数-3种
        public TreeClass()
        {
            sons = new List<TreeClass<T>>();///目录层级1包含的文件夹
        }
        public TreeClass(int node_level)
        {
            this.node_level = node_level;
            sons = new List<TreeClass<T>>();///目录层级2包含的文件夹及文件
        }
        public TreeClass(int node_level, T node_data)
        {
            this.node_level = node_level;
            this.node_data = node_data;
            sons = new List<TreeClass<T>>();///目录层级2包含的文件夹及文件
        }

        public TreeClass(int node_level, T node_data, T node_name)
        {
            this.node_level = node_level;///目录层级3包含的文件
            this.node_data = node_data;
            this.node_name = node_name;
            sons = new List<TreeClass<T>>();
        }
        //树形结构：1、关联性，包含父与子。2、nodes数据信息，层级(node_level)+路径(node_data)+文件名(node_name)



        private TreeClass<T> parent;   //父节点
        private List<TreeClass<T>> sons;    //字节点


        public TreeClass<T> Parent
        {
            get { return parent; }
        }
        public List<TreeClass<T>> Sons
        {
            get { return sons; }
        }

        //节点数据
        public int node_level { get; set; }
        public T node_data { get; set; }
        public T node_name { get; set; }


        //添加子节点
        public void AddNodes(List<TreeClass<T>> sons)//节点集合
        {
            foreach (var son in sons)
            {
                if (!this.sons.Contains(son))
                {
                    son.parent = this;
                    this.sons.Add(son);
                }
            }
        }
        public void AddNode(TreeClass<T> son)//单个节点
        {
            if (!this.sons.Contains(son))
            {
                son.parent = this;
                this.sons.Add(son);
            }
        }

        //清除son
        public void Remove(TreeClass<T> son)//删除单个节点
        {
            if (this.sons.Contains(son))
            {
                this.sons.Remove(son);
            }
        }
        public void Remove(List<TreeClass<T>> sons)//删除节点集合
        {
            foreach (var son in sons)
            {
                if (this.sons.Contains(son))
                {
                    this.sons.Remove(son);
                }
            }
        }

    }
}