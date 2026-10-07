using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : BasePanel
{
    protected override void OnClick(string btnName)
    {
        if(btnName == "StartBtn")
        {
            SceneMgr.Instance().LoadSceneAsyn("SampleScene", () =>
            {
                UIManager.Instance().HidePanel("StartPanel");
                AudioMgr.Instance().StopBKMusic("BKMusic");
            });
        }
        else if (btnName == "SettingBtn")
        {
            UIManager.Instance().ShowPanel<SettingPanel>("SettingPanel");
        }
        else if (btnName == "QuitBtn")
        {
            Application.Quit();
        }
    }
}
