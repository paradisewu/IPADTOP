using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test11222 : MonoBehaviour
{

    void Start()
    {

        ControlData controlData = new ControlData();
        controlData.ID = "lihua";
        controlData.Control_Type = ControlType.Computer;
        controlData.ProjectDatas = new List<ProjectData>()
        {
            new ProjectData() { Id = 1, Name = "项目1" },
            new ProjectData() { Id = 2, Name = "项目2" },
            new ProjectData() { Id = 3, Name = "项目3" }
        };



        MyDataBase.Insert(controlData);

        //MyDataBase.Insert()

        //List<MyPerson> perList = MyDataBase.SelectAll<MyPerson>(p);

        //foreach (var item in perList)
        //{
        //    Debug.Log(item.ID);
        //}
    }


}
