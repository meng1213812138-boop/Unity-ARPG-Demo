using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : BaseManager<InputMgr>
{
    private bool isStart = false;
    public InputMgr()
    {
        MonoManager.Instance().AddUpadteLinstener(MyUpdate);
    }
    /// <summary>
    /// 用来检测是否开启检查输入
    /// </summary>
    /// <param name="isOpen"></param>
    public void StartOrEndcheck(bool isOpen)
    {
        isStart = isOpen;
    }
    public void CheckKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(key))
            EventCenter.Instance().EventTrigger("某键按下", key);
        if (Input.GetKeyUp(key))
            EventCenter.Instance().EventTrigger("某键抬起", key);
    }
    private void MyUpdate()
    {
        if(!isStart)
            return;
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);
    }
}
