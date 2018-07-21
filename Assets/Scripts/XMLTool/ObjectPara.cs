using System;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
public class ObjectPara<T>
{
    public string ObjName;//待解析的类的名称
    public string[] cols;//类的成员变量
    public string[] values;//成员变量对应的值
    public PropertyInfo[] info;//反射获取类的属性
    public Type objtype;//待解析类型
    public T obj;//泛型对象
    public T GetObject(string[] values)
    {
        T _obj = obj;
        for (int i = 0; i < info.Length; i++)
        {
            if (objtype.GetProperty(cols[i]).PropertyType.IsArray)
            {
                Type type = objtype.GetProperty(cols[i]).PropertyType;
                string[] vale = values[i].Split(new char[] { ',' });
                if (type == typeof(string[]))
                {
                    info[i].SetValue(_obj, vale, null);
                }

                else if (type == typeof(int[]))
                {

                    int[] va = new int[vale.Length];
                    for (int j = 0; j < va.Length; j++)
                    {
                        va[j] = int.Parse(vale[j]);
                    }
                    info[i].SetValue(_obj, va, null);
                }
                else if (type == typeof(float[]))
                {
                    float[] va = new float[vale.Length];
                    for (int j = 0; j < va.Length; j++)
                    {
                        va[j] = float.Parse(vale[j]);
                    }
                    info[i].SetValue(_obj, va, null);
                }
                //MyTool.P(values[i]);
            }
            else
            {
                info[i].SetValue(_obj, Convert.ChangeType(values[i], info[i].PropertyType), null);
            }
        }
        return _obj;
    }
    public ObjectPara()
    {
    }
    public ObjectPara(T obj)
    {
        this.obj = obj;
        AnalyzeObject();
    }
    public void AnalyzeObject()//解析对象
    {
        //获取对象的名称，字段，以及值，以字符串数组的形式存储
        objtype = obj.GetType();
        ObjName = objtype.Name;
        info = objtype.GetProperties();
        cols = new string[info.Length];
        values = new string[info.Length];
        for (int i = 0; i < info.Length; i++)
        {
            cols[i] = info[i].Name;
            string value = "";
            if (objtype.GetProperty(cols[i]).PropertyType.IsArray)
            {
                if (objtype.GetProperty(cols[i]).GetValue(obj, null) != null)
                {
                    Type type = objtype.GetProperty(cols[i]).PropertyType;
                    if (type == typeof(string[]))
                    {
                        //MyTool.P(value);
                        string[] ob = objtype.GetProperty(cols[i]).GetValue(obj, null) as string[];
                        if (ob != null)
                        {
                            foreach (var o in ob)
                            {
                                value += o + ",";
                            }
                            //value = value.Substring(0, value.Length-2);
                            value = value.TrimEnd(new char[] { ',' });
                        }
                    }
                    else if (type == typeof(int[]))
                    {
                        int[] ob = objtype.GetProperty(cols[i]).GetValue(obj, null) as int[];
                        if (ob != null)
                        {
                            foreach (var o in ob)
                            {
                                value += o + ",";

                            }
                            //value = value.Substring(0, value.Length-2);
                            value = value.TrimEnd(new char[] { ',' });
                        }
                    }
                    else if (type == typeof(float[]))
                    {
                        float[] ob = objtype.GetProperty(cols[i]).GetValue(obj, null) as float[];
                        if (ob != null)
                        {
                            foreach (var o in ob)
                            {
                                value += o + ",";

                            }
                            //value = value.Substring(0, value.Length-2);
                            value = value.TrimEnd(new char[] { ',' });
                        }
                    }
                }
            }
            else
            {
                value = Convert.ToString(objtype.GetProperty(cols[i]).GetValue(obj, null));
            }
            //MyTool.P(value);
            values[i] = value;
        }
    }
    public System.Object GetKeyValue(string col)
    {
        return (objtype.GetProperty(col).GetValue(obj, null));
    }
}