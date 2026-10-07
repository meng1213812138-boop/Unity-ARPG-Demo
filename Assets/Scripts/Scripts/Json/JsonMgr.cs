using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum JsonType
{
    JsonUtility,
    LitJson,
}
public class JsonMgr : BaseManager<JsonMgr>
{
    /// <summary>
    /// 存储json数据
    /// </summary>
    /// <param name="data"></param>
    /// <param name="fileName"></param>
    /// <param name="type"></param>
    public void SaveData(object data,string fileName,JsonType type = JsonType.LitJson)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        string jsonStr = "";
        switch (type)
        {
            case JsonType.JsonUtility:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }
        File.WriteAllText(path, jsonStr);
    }

    /// <summary>
    /// 读取指定文件中的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="flieName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public T LoadData<T>(string flieName, JsonType type = JsonType.LitJson) where T : new()
    {
        string path = Application.streamingAssetsPath + "/" + flieName + ".json";
        if(!File.Exists(path))
            path = Application.persistentDataPath + "/" + flieName + ".json";
        if(!File.Exists(path))
            return new T();
        string jsonStr = File.ReadAllText(path);
        T data = default(T);
        switch (type)
        {
            case JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case JsonType.LitJson:
                Debug.Log("JSON文件路径：" + path);
                Debug.Log("JSON文件内容：" + jsonStr);
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }
        return data;
    }
}
