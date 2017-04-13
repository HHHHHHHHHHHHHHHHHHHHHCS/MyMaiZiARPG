using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

public class MyJsonManager
{
    private static string _savePath;
    private static bool isFirst = true;
    private const string sample = @"{}";

    #region Init__GetSet
    /// <summary>
    ///初始化路径用
    /// </summary>
    static void Init()
    {
        if (isFirst)
        {
            isFirst = false;
            if (Application.platform == RuntimePlatform.Android)
            {
                _savePath = Application.persistentDataPath + "/GameData/";//Android环境下的文件路径
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                _savePath = Application.dataPath + "/Raw/GameData/";//IPhonePlayer     
            }
            else
            {
                _savePath = Application.dataPath + "/GameData/";//PC
            }
        }
    }

    private static string SavePath
    {
        get
        {
            Init();
            return _savePath;
        }
    }
    #endregion
    #region UseMethod
    /// <summary>
    /// 得到数据字典
    /// </summary>
    /// <returns>数据字典</returns>
    public static Dictionary<string, string> GetJsonDic(string saveName)
    {
        CheckExists(saveName);
        string strJson = GetJsonString(SavePath + saveName);
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(strJson);
    }

    /// <summary>
    /// 更新数据信息
    /// </summary>
    /// <param name="dataKey">数据的key值</param>
    /// <param name="dataInfo">数据的值</param>
    public static void UpdateData(string saveName, string dataKey, string dataInfo)
    {
        Dictionary<string, string> _dictionary = GetJsonDic(saveName);
        _dictionary[dataKey]= dataInfo;
        string output = JsonConvert.SerializeObject(_dictionary);
        UpdateFile(saveName, output);
    }

    /// <summary>
    /// 得到数据
    /// </summary>
    /// <param name="dataKey">数据的key值</param>
    /// <returns>数据</returns>
    public static string GetData(string saveName, string dataKey)
    {
        Dictionary<string, string> _dictionary = GetJsonDic(saveName);
        if (_dictionary.ContainsKey(dataKey))
        {
            return _dictionary[dataKey];
        }

        _dictionary.Add(dataKey, "");
        string output = JsonConvert.SerializeObject(_dictionary);
        UpdateFile(saveName, output);
        return "";
    }

    /// <summary>
    /// 检测文件是否存在
    /// </summary>
    private static void CheckExists(string saveName)
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
            UpdateFile(saveName, sample);
            return;
        }
        if (File.Exists(SavePath + saveName))
        {
            return;
        }

    }




    /// <summary>
    /// 更新文本信息
    /// </summary>
    /// <param name="info">要更新的内容</param>
    private static void UpdateFile(string saveName, string info)
    {

        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(SavePath + saveName);
        //如果此文件不存在则创建
        sw = t.CreateText();
        //写入信息
        sw.Write(info);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();

    }


    /// <summary>
    /// 获取Json 从本地
    /// </summary>
    /// <param name="_path">Json本地的位置</param>
    /// <returns>json的数据</returns>
    private static string GetJsonString(string _path)
    {
        StreamReader sr = File.OpenText(_path);
        string strJson = sr.ReadToEnd();
        sr.Close();
        sr.Dispose();
        return strJson;
    }

    #endregion

}
