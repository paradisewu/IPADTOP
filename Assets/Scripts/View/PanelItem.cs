using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelItem : PanelBase
{

    #region 初始化相关
    protected override void OnInitSkin()
    {
        base.SetMainSkinPath("UI/PanelItem");
        base.OnInitSkin();

        _type = PanelType.PanelItem;
        _showStyle = UIManager.PanelShowStyle.Nomal;
    }

    private Toggle toggle1;
    private Toggle toggle2;
    private Toggle toggle3;

    protected override void OnInitSkinDone()
    {
        toggle1 = this.skinTransform.Find("LeftControl/ToggleOne").GetComponent<Toggle>();
        toggle2 = this.skinTransform.Find("LeftControl/ToggleTwo").GetComponent<Toggle>();
        toggle3 = this.skinTransform.Find("LeftControl/ToggleThree").GetComponent<Toggle>();
    }
    protected override void OnInitDone()
    {
        base.OnInitDone();
        toggle1.isOn = true;

    }


    protected override void OnClick(GameObject click)
    {
        base.OnClick(click);
        ClickButton(click);
    }
    #endregion

    private ProjectType ProjectName;
    private ControlType ControlName;
    private OperationType OperationName;

    private bool M_click;
    public void ClickButton(GameObject click)
    {
        M_click = false;
        string name = click.name;
        if (name.Equals("ButtonClose"))
        {
            M_click = true;
            Close();
        }
        else if (name.Equals("ToggleOne"))
        {
            ProjectName = ProjectType.ItemOne;
            M_click = true;
        }
        else if (name.Equals("ToggleTwo"))
        {
            ProjectName = ProjectType.ItemTwo;
            M_click = true;
        }
        else if (name.Equals("ToggleThree"))
        {
            ProjectName = ProjectType.ItemThree;
            M_click = true;
        }
        else if (name.Equals("P_Open"))
        {
            ControlName = ControlType.Projector;
            OperationName = OperationType.Open;
        }
        else if (name.Equals("P_Close"))
        {
            ControlName = ControlType.Projector;
            OperationName = OperationType.Close;
        }
        else if (name.Equals("C_Open"))
        {
            ControlName = ControlType.Computer;
            OperationName = OperationType.Open;
        }
        else if (name.Equals("C_Close"))
        {
            ControlName = ControlType.Computer;
            OperationName = OperationType.Close;
        }
        else if (name.Equals("play"))
        {
            ControlName = ControlType.Software;
            OperationName = OperationType.Play;
        }
        else if (name.Equals("pasue"))
        {
            ControlName = ControlType.Software;
            OperationName = OperationType.Pause;
        }
        else if (name.Equals("stop"))
        {
            ControlName = ControlType.Software;
            OperationName = OperationType.Stop;
        }
        else if (name.Equals("quite"))
        {
            ControlName = ControlType.Sound;
            OperationName = OperationType.Quite;
        }
        else if (name.Equals("Sub"))
        {
            ControlName = ControlType.Sound;
            OperationName = OperationType.Sub;
        }
        else if (name.Equals("Add"))
        {
            ControlName = ControlType.Sound;
            OperationName = OperationType.Add;
        }
        if (!M_click)
        {
            Debug.Log("send");
            GameManager.MessageAnalysis(ProjectName, ControlName, OperationName);
        }
    }
}

