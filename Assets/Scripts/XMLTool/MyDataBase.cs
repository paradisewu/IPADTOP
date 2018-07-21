using System.Collections.Generic;
using System;
using UnityEngine;
public class MyDataBase
{
    private static DataBase database;
    public static string RESDATAPATH = Application.streamingAssetsPath;
    //源数据目录
    private static string DATAPATH = Application.dataPath;
    //数据目录
    static MyDataBase()
    {
#if UNITY_ANDROID
        DATAPATH = Application.persistentDataPath;
 
#endif
        database = new XmlDataBase(DATAPATH);
    }

    public static void Insert<T>(T t)
    {
        database.Insert(t);
    }
    public static T Select<T>(T t)
    {
        return database.Select(t);
    }
    public static List<T> SelectAll<T>(T t)
    {
        return database.SelectAll<T>(t);
    }
    public static void Update<T>(T t, string key)
    {
        database.Update(t, key);
    }
    public static void Update<T>(T t)
    {
        database.Update(t);
    }
    public static void Delete<T>(T t)
    {
        database.Delete(t);
    }

}