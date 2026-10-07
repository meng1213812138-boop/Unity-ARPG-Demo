using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : BasePanel
{
    private Text TipsText;

    protected override void OnClick(string btnName)
    {
        if (btnName == "OkeyBtn")
        {
            SceneMgr.Instance().LoadSceneAsyn("StartSence", () =>
            {
                UIManager.Instance().HidePanel("TipsPanel");
            });
        }
        else if (btnName == "QuitBtn")
        {
            SceneMgr.Instance().LoadSceneAsyn("StartSence", () =>
            {
                UIManager.Instance().HidePanel("TipsPanel");
            });
        }
    }

    public void ReTipText(string tips)
    {
        TipsText = GetControl<Text>("TipsText");
        TipsText.text = tips;
    }
}
