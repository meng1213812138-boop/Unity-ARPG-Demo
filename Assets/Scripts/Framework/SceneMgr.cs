using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : BaseManager<SceneMgr>
{
    /// <summary>
    /// 同步加载场景的方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    public void LoadScene(string name,UnityAction fun)
    {
        SceneManager.LoadScene(name);
        fun();
    }
    /// <summary>
    /// 异步加载场景的方法壳
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    public void LoadSceneAsyn(string name,UnityAction fun)
    {
        MonoManager.Instance().StratCoroutine(ReallLoadSceneAsyn(name, fun));
    }

    /// <summary>
    /// 异步加载场景的方法核心
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    /// <returns></returns>
    private IEnumerator ReallLoadSceneAsyn(string name,UnityAction fun)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        while (!ao.isDone)
        {
            EventCenter.Instance().EventTrigger("进度条更新",ao.progress);
            yield return ao.progress;
        }
        fun();
    }
}
