using UnityEngine;
using System.Collections;
using System.IO;
public class CheckData : MonoBehaviour
{
    public string resData;
    public string gameData;
    public string path = "/GameData.xml";
    // Use this for initialization
    void Awake()
    {
        resData = Application.streamingAssetsPath + path;
#if UNITY_ANDROID
        gameData = Application.persistentDataPath+path;

#else
        gameData = Application.dataPath + path;
#endif
        StartCoroutine(ReadData(gameData));
    }
    IEnumerator ReadData(string path)
    {

        if (!File.Exists(path))
        {
#if UNITY_ANDROID
            WWW www = new WWW(resData);
            yield return www;
            if(www.isDone) 
            {
                File.WriteAllBytes(path,www.bytes);
            }
#else
            File.Copy(resData, path);
#endif
            Application.LoadLevel(1);
        }
        else
        {
            Application.LoadLevel(1);
        }
        yield return 0;

    }


}