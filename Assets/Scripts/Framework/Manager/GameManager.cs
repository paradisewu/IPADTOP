using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using UnityEngine;
namespace LuaFramework
{
    public class GameManager : Manager
    {
        private Dictionary<ControlType, Dictionary<ProjectType, Dictionary<OperationType, string>>> messageDictory = new Dictionary<ControlType, Dictionary<ProjectType, Dictionary<OperationType, string>>>();
        private Dictionary<ProjectType, Dictionary<OperationType, string>> ProjectDictory = new Dictionary<ProjectType, Dictionary<OperationType, string>>();
        private Dictionary<OperationType, string> OperationDicoty = new Dictionary<OperationType, string>();



        CHChatSocket _CHChatSocket;

        void Start()
        {
            _CHChatSocket = CHChatSocket.instance;
            UIManager.ShowPanel(PanelType.PanelMain);
            XmlPath = Application.dataPath + "/data.xml";
            ReadXMl();
        }


        private string XmlPath;
        /// <summary>
        /// 读取XML数据
        /// </summary>
        public void ReadXMl()
        {
            if (!File.Exists(XmlPath))
            {
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlPath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
            string temp1 = string.Empty;
            string temp2 = string.Empty;
            string temp3 = string.Empty;
            foreach (XmlElement item in nodeList)
            {
                bool changeControl = false;
                bool changeProject = false;

                string controlType = item.GetAttribute("controlType");
                ControlType tp1 = (ControlType)Enum.Parse(typeof(ControlType), controlType, false);
                if (temp1 == string.Empty || temp1 != controlType)
                {
                    ProjectDictory = new Dictionary<ProjectType, Dictionary<OperationType, string>>();
                    changeControl = true;
                    temp1 = controlType;
                }

                string projectType = item.GetAttribute("projectType");
                ProjectType tp2 = (ProjectType)Enum.Parse(typeof(ProjectType), projectType, false);
                if (temp2 == string.Empty || temp2 != projectType)
                {
                    OperationDicoty = new Dictionary<OperationType, string>();
                    changeProject = true;
                    temp2 = projectType;
                }

                string operationType = item.GetAttribute("operationType");
                OperationType tp3 = (OperationType)Enum.Parse(typeof(OperationType), operationType, false);
                string msg = item.GetAttribute("commandMsg");
                OperationDicoty.Add(tp3, msg);

                if (changeControl)
                {
                    messageDictory.Add(tp1, ProjectDictory);
                }
                if (changeProject)
                {
                    ProjectDictory.Add(tp2, OperationDicoty);
                }
            }
        }


        public void MessageAnalysis(ProjectType a, ControlType b, OperationType c)
        {
            if (messageDictory.ContainsKey(b))
            {
                if (messageDictory[b].ContainsKey(a))
                {
                    if (messageDictory[b][a].ContainsKey(c))
                    {
                        string msg = messageDictory[b][a][c];
                        byte[] data = HexToByte(msg);
                        SendMessageToServer(data);
                    }
                }
            }
        }

        public void MessageAnalysis(ControlType b, OperationType c)
        {
            foreach (ProjectType suit in Enum.GetValues(typeof(ProjectType)))
            {
                MessageAnalysis(suit, b, c);
            }
        }


        public void SendMessageToServer(string message)
        {
            Debug.Log(message);
            _CHChatSocket.SendMsg(message);
        }

        public void SendMessageToServer(byte[] data)
        {
            _CHChatSocket.SendMsg(data);
        }

        public static byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");//移除空格
            msg = msg.Replace("-", "");
            msg = msg.Replace(":", "");

            byte[] comBuffer = new byte[msg.Length / 2];
            try
            {
                for (int i = 0; i < msg.Length; i += 2)
                {
                    comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return comBuffer;
        }

    }
}
public enum ControlType
{
    Null,
    Computer,
    Projector,
    Lighting,
    Sound,
    Software
}
public enum ProjectType
{
    ItemOne, ItemTwo, ItemThree
}

public enum OperationType
{
    Open, Close, Play, Pause, Stop, Quite, Sub, Add
}