using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IeventInfo
{

}
public class EventInfo<T>: IeventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}
public class EventInfo : IeventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}
public class EventCenter : BaseManager<EventCenter>
{
    private Dictionary<string,IeventInfo> eveDic = new Dictionary<string, IeventInfo>();
    
    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">委托函数</param>
    public void AddEventListener<T>(string name,UnityAction<T> action)
    {
        if (eveDic.ContainsKey(name))
        {
            (eveDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            eveDic.Add(name, new EventInfo<T>(action));
        }
    }
    /// <summary>
    /// 添加监听没有参数的函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener(string name, UnityAction action)
    {
        if (eveDic.ContainsKey(name))
        {
            (eveDic[name] as EventInfo).actions += action;
        }
        else
        {
            eveDic.Add(name, new EventInfo(action));
        }
    }

    /// <summary>
    /// 删除事件监听
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name=""></param>
    /// <param name="action">委托名</param>
    public void RemoveEventLinstener<T>(string name,UnityAction<T> action)
    {
        if (eveDic.ContainsKey(name))
        {
            (eveDic[name] as EventInfo<T>).actions -= action;
        }
    }
    /// <summary>
    /// 删除没有参数的函数监听
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void RemoveEventLinstener(string name, UnityAction action)
    {
        if (eveDic.ContainsKey(name))
        {
            (eveDic[name] as EventInfo).actions -= action;
        }
    }

    /// <summary>
    /// 触发事件监听
    /// </summary>
    /// <param name="name"></param>
    /// <param name="info"></param>
    public void EventTrigger<T>(string name,T info)
    {
        if (eveDic.ContainsKey(name))
        {
            //eveDic[name].Invoke(info);
            if ((eveDic[name] as EventInfo<T>).actions != null) 
                (eveDic[name] as EventInfo<T>).actions.Invoke(info);
        }
    }
    /// <summary>
    /// 触发没有参数的函数监听
    /// </summary>
    /// <param name="name"></param>
    public void EventTrigger(string name)
    {
        if (eveDic.ContainsKey(name))
        {
            //eveDic[name].Invoke(info);
            if ((eveDic[name] as EventInfo).actions != null)
                (eveDic[name] as EventInfo).actions.Invoke();
        }
    }
    /// <summary>
    /// 清除事件中心
    /// </summary>
    public void Clear()
    {
        eveDic.Clear(); 
    }
}
