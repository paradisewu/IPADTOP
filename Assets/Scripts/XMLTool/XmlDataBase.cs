using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System;
public class XmlDataBase : DataBase
{
    private string path = "/DataBase/GameData.xml";
    private string Myroot = "MyData";
    private XmlDocument xmlDoc = new XmlDocument();
    public static string ObjectID = "ID";
    public static string PATH;
    private void ReadFile(string path)
    {
        PATH = path + this.path;
        if (!File.Exists(PATH))//如果指定的路径不存在
        {
            if (!File.Exists(MyDataBase.RESDATAPATH + this.path))//如果不存在源文件
            {
                Directory.CreateDirectory(MyDataBase.RESDATAPATH + "/DataBase");

                File.CreateText(PATH);
                CreateData((MyDataBase.RESDATAPATH + this.path));
                Application.Quit();
            }
            else
            {

                Directory.CreateDirectory(path + "/DataBase");

                File.CreateText(PATH);
                File.Copy(MyDataBase.RESDATAPATH + this.path, PATH);

                File.Delete(MyDataBase.RESDATAPATH + this.path);

            }
        }
        else
        {
            xmlDoc.Load(PATH);

        }
    }
    public XmlDataBase()
    {
        //这是一个XML数据库，基于XML的对象的存取，改查操作。
        //注意，数据对象的ID需要以字母开头，且不能有特殊字符。

        //TextAsset textAsset = (TextAsset)Resources.Load(file, typeof(TextAsset));

        ReadFile(path);//读取默认路径
    }
    public XmlDataBase(string path)
    {
        ReadFile(path);//指定读取默认路径
    }
    public void CreateData(string path)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
        XmlNode root = xmlDoc.CreateElement(Myroot);
        xmlDoc.AppendChild(xmlDeclaration);
        xmlDoc.AppendChild(root);
        xmlDoc.Save(path);
    }
    public void Insert<T>(T obj)
    {
        //插入一个对象
        //对象有字段，属性值
        //利用数据的特点，即名称不同，优化查询速度，具体的做法为，将属性值作为子根操作。

        ObjectPara<T> OP = new ObjectPara<T>(obj);

        XmlNode root = xmlDoc.SelectSingleNode(Myroot);//查找<game data>

        Debug.Log(OP.ObjName);
        XmlNode Myroots = root.SelectSingleNode(OP.ObjName);//查询对象表
        if (Myroots == null)
        {
            Myroots = xmlDoc.CreateElement(OP.ObjName);
            root.AppendChild(Myroots);
        }
        //以对象表为根，创建一个以ID为根的子根
        Debug.Log(OP.GetKeyValue(ObjectID).ToString());
        XmlNode node = Myroots.SelectSingleNode(OP.GetKeyValue(ObjectID).ToString());
        if (node != null)
        {
            //如果待插入的对象已经存在，则将此数据删除后，再重新插入
            Delete<T>(obj);
            Insert<T>(obj);
            return;
        }

        XmlElement xe1 = xmlDoc.CreateElement(OP.GetKeyValue(ObjectID).ToString());//创建一个<data>节点

        for (int i = 0; i < OP.cols.Length; i++)
        {

            XmlElement xe = xmlDoc.CreateElement(OP.cols[i]);
            xe.InnerText = OP.values[i];
            xe1.AppendChild(xe);
        }

        if (Myroots == null)
        {
            Myroots = xmlDoc.CreateElement(OP.ObjName);
            root.AppendChild(Myroots);
        }
        else
        {
            Myroots.AppendChild(xe1);
        }
        //添加到<bookstore>节点中
        xmlDoc.Save(PATH);
        OP = null;
    }
    public List<T> SelectAll<T>(T obj)
    {

        List<T> list = new List<T>();

        ObjectPara<T> OP = new ObjectPara<T>(obj);

        XmlNode root = xmlDoc.SelectSingleNode(Myroot);//查找<game data>
        XmlNode Myroots = root.SelectSingleNode(OP.ObjName);//查询对象表

        XmlNodeList nodes = Myroots.ChildNodes;//获取对象的所有子对象
        foreach (XmlNode node in nodes)//
        {
            XmlNodeList lis = node.ChildNodes;//获取每个对象的字段
            T o = obj;
            int count = lis.Count;
            string[] values = new string[count];
            for (int i = 0; i < lis.Count; i++)
            {
                values[i] = lis.Item(i).InnerText.Trim();
            }
            o = OP.GetObject(values);
            list.Add(o);
        }
        return list;
    }
    public T Select<T>(T obj)
    {
        ObjectPara<T> OP = new ObjectPara<T>(obj);

        XmlNode root = xmlDoc.SelectSingleNode(Myroot);//查找<game data>

        XmlNode Myroots = root.SelectSingleNode(OP.ObjName);//查询对象表

        XmlNode node = Myroots.SelectSingleNode(OP.GetKeyValue(ObjectID).ToString());
        //获取指定列为根的根元素
        if (node != null)
        {
            XmlNodeList list = node.ChildNodes;//获取根的所有字段
            int count = list.Count;
            string[] values = new string[count];
            for (int i = 0; i < list.Count; i++)
            {
                //成员变量
                //将遍历出的值赋予数组
                values[i] = list.Item(i).InnerText.Trim();

            }
            obj = OP.GetObject(values);
        }
        else
        {
            obj = default(T);
            //MyTool.P(213);
        }
        return obj;
    }
    public void Update<T>(T obj)
    {
        ObjectPara<T> OP = new ObjectPara<T>(obj);
        //

        XmlNode root = xmlDoc.SelectSingleNode(Myroot);//查找<game data>

        XmlNode Myroots = root.SelectSingleNode(OP.ObjName);//查询对象表

        XmlNode node = Myroots.SelectSingleNode(OP.GetKeyValue(ObjectID).ToString());

        if (node != null)
        {

            XmlNodeList xnl = node.ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {//成员变量
                //将遍历出的值赋予数组

                xnl.Item(i).InnerText = OP.values[i];
            }
            xmlDoc.Save(PATH);
        }
    }
    public void Update<T>(T obj, string key)
    {
        //插入一个对象
        //对象有字段，属性值
        //利用数据的特点，即名称不同，优化查询速度，具体的做法为，将属性值作为子根操作。

        ObjectPara<T> OP = new ObjectPara<T>(obj);
        //

        XmlNode root = xmlDoc.SelectSingleNode(Myroot);//查找<game data>

        XmlNode Myroots = root.SelectSingleNode(OP.ObjName);//查询对象表

        XmlNode node = Myroots.SelectSingleNode(OP.GetKeyValue(ObjectID).ToString());

        if (node != null)
        {
            //找到名为name的对象
            XmlNode xl = node.SelectSingleNode(key);//找到字段
            xl.InnerText = OP.GetKeyValue(key).ToString();
            xmlDoc.Save(PATH);
        }
    }
    public void Delete<T>(T obj)
    {
        ObjectPara<T> OP = new ObjectPara<T>(obj);
        XmlNode root = xmlDoc.SelectSingleNode(Myroot);//查找<game data>

        XmlNode Myroots = root.SelectSingleNode(OP.ObjName);//查询对象表

        XmlNode node = Myroots.SelectSingleNode(OP.GetKeyValue(ObjectID).ToString());

        if (node != null)
        {
            node.RemoveAll();
            Myroots.RemoveChild(node);
            xmlDoc.Save(PATH);
        }
    }


}