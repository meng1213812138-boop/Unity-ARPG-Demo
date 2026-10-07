using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(updateEvent != null)
        {
            updateEvent();
        }
    }

    /// <summary>
    /// 用于添加帧更新
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpadteLinstener(UnityAction fun)
    {
        updateEvent += fun;
    }

    /// <summary>
    /// 用于移除帧更新
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateLinstener(UnityAction fun)
    {
        updateEvent -= fun;
    }

    
}
