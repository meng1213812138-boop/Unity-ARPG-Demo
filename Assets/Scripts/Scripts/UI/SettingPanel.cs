using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public override void ShowMe()
    {
        base.ShowMe();

        Slider musicSlider = GetControl<Slider>("MusicSlider");
        Slider soundSlider = GetControl<Slider>("SoundSlider");

        if (musicSlider != null)
        {
            musicSlider.SetValueWithoutNotify(AudioMgr.Instance().BkMusicVolume);
        }

        if (soundSlider != null)
        {
            soundSlider.SetValueWithoutNotify(AudioMgr.Instance().SoundVolume);
        }
    }

    protected override void OnClick(string btnName)
    {
        if(btnName == "OkeyBtn")
        {
            UIManager.Instance().HidePanel("SettingPanel");
        }
        else if(btnName == "QuitBtn")
        {
            UIManager.Instance().HidePanel("SettingPanel");
        }
    }

    protected override void OnSliderValueChange(string sliderName, float value)
    {
        if(sliderName == "MusicSlider")
        {
            AudioMgr.Instance().ChangeBKValue(value);
        }
        else if(sliderName == "SoundSlider")
        {
            AudioMgr.Instance().ChangeSoundValue(value);

        }

        AudioData audioData = new AudioData();
        audioData.BkMusicVoluem = AudioMgr.Instance().BkMusicVolume;
        audioData.SoundVoluem = AudioMgr.Instance().SoundVolume;

        JsonMgr.Instance().SaveData(audioData, "AudioData");
    }
    
}
