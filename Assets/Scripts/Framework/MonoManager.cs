using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MonoManager : BaseManager<MonoManager>
{
    public MonoController controller;

    public MonoManager()
    {
        GameObject obj = new GameObject("Monocontroller");
        controller = obj.AddComponent<MonoController>();
    }

    /// <summary>
    /// 用于添加帧更新
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpadteLinstener(UnityAction fun)
    {
        controller.AddUpadteLinstener(fun);
    }

    /// <summary>
    /// 用于移除帧更新
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateLinstener(UnityAction fun)
    {
        controller.RemoveUpdateLinstener(fun);
    }

    public Coroutine StratCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }

    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }

    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }

    public Coroutine StartCoroutine_Auto(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }
}
