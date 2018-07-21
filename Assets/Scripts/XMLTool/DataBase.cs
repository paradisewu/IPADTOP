using System.Collections.Generic;
using System;
public interface DataBase
{
    void Insert<T>(T t);
    T Select<T>(T t);
    List<T> SelectAll<T>(T t);
    void Update<T>(T t, string key);
    void Update<T>(T t);
    void Delete<T>(T t);
    void CreateData(string path);
}