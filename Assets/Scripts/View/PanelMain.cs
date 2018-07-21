using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMain : PanelBase
{
    public ControlType m_comtrolTpye;

    #region 初始化相关
    protected override void OnInitSkin()
    {
        base.SetMainSkinPath("UI/PanelMain");
        base.OnInitSkin();

        _type = PanelType.PanelMain;
        _showStyle = UIManager.PanelShowStyle.Nomal;
    }

    protected override void OnInitDone()
    {
        base.OnInitDone();

    }


    protected override void OnClick(GameObject click)
    {
        base.OnClick(click);
        ClickButton(click);
    }

    #endregion

    public void ClickButton(GameObject click)
    {
        if (click.name.Equals("Button_Close"))
        {
            Close();
        }
        else if (click.name.Equals("ToggleOne"))
        {
            m_comtrolTpye = ControlType.Projector;
        }
        else if (click.name.Equals("ToggleTwo"))
        {
            m_comtrolTpye = ControlType.Computer;
        }
        else if (click.name.Equals("ToggleThree"))
        {
            m_comtrolTpye = ControlType.Lighting;
        }
        else if (click.name.Equals("ToggleFive"))
        {
            m_comtrolTpye = ControlType.Sound;
        }
        else if (click.name.Equals("ToggleFour"))
        {
            m_comtrolTpye = ControlType.Null;
            UIManager.ShowPanel(PanelType.PanelItem);
        }
        else if (click.name.Equals("Toggle_Open"))
        {
            if (m_comtrolTpye!=ControlType.Null)
            {
                GameManager.MessageAnalysis(m_comtrolTpye, OperationType.Open);
            }
        }
        else if (click.name.Equals("Toggle_Close"))
        {
            if (m_comtrolTpye != ControlType.Null)
            {
                GameManager.MessageAnalysis(m_comtrolTpye, OperationType.Close);
            }
        }
    }
}
