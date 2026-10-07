using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 用来分类池子里面的内容
/// </summary>
public class PoolData
{
    //缓存池内的文件夹
    public GameObject fatherObj;
    //文件夹里的内容
    public List<GameObject> poolList;
    public PoolData(GameObject obj,GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>() {};
        PushObj(obj);
    }
    /// <summary>
    /// 往缓存池里放东西
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        obj.SetActive(false);
        //存起来
        poolList.Add(obj);
        //设置父对象
        obj.transform.parent = fatherObj.transform;
    }
    public GameObject GetObj()
    {
        GameObject obj = null;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.parent = null;

        return obj;
    }
}
public class PoolManager : BaseManager<PoolManager>
{
    //缓存池容器
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    private GameObject poolObj;
    
    /// <summary>
    /// 从缓存池往外拿东西的方法
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public void GetObj(string name,UnityAction<GameObject> callBack)
    {
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            //obj = poolDic[name].GetObj();
            callBack(poolDic[name].GetObj());
        }
        else
        {
            ResMgr.Instance().LoadAsyn<GameObject>(name, (o) =>
            {
                o.name = name;
                callBack(o);
            });
        }
    }

    /// <summary>
    /// 存储对象进缓存池的方法
    /// </summary>
    public void PushObj(string name, GameObject obj)
    {
        if (poolObj == null)
            poolObj = new GameObject("Pool");
        //里面有抽屉
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        //里面没有抽屉
        else
        {
            poolDic.Add(name, new PoolData(obj, poolObj));
        }
    }

    /// <summary>
    /// 清除缓存池的方法
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
