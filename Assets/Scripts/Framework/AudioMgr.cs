using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioMgr : BaseManager<AudioMgr>
{
    //背景音乐组件
    private AudioSource bkMusic = null;
    //背景音乐音量
    private float bkValue = 1;
    //音效挂载的对象
    private GameObject soundObj = null;
    //音效
    private List<AudioSource> soundList = new List<AudioSource>();
    //音效音量
    private float soundValue = 1;

    public float BkMusicVolume => bkValue;

    public float SoundVolume => soundValue;

    public AudioMgr()
    {
        MonoManager.Instance().AddUpadteLinstener(Update);
    }
    private void Update()
    {
        for (int i = soundList.Count - 1; i >= 0; --i)
        {
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayBKMusic(string name)
    {
        if(bkMusic == null)
        {
            GameObject obj = new GameObject(name);
            bkMusic = obj.AddComponent<AudioSource>();
        }
        ResMgr.Instance().LoadAsyn<AudioClip>("Music/BK/" + name, (clip) =>
        {
            bkMusic.clip = clip;
            bkMusic.loop = true;
            bkMusic.volume = bkValue;
            bkMusic.Play();
        });
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBKMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Pause();
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void StopBKMusic(string name)
    {
        if (bkMusic == null)
            return;
        bkMusic.Stop();
    }

    /// <summary>
    /// 改变音量大小
    /// </summary>
    public void ChangeBKValue(float v)
    {
        bkValue = v;
        if (bkMusic == null)
            return;
        bkMusic.volume = bkValue;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name,bool isLoop,UnityAction<AudioSource> callBack = null)
    {
        if(soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }
        ResMgr.Instance().LoadAsyn<AudioClip>("Music/Sound/" + name, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundValue;
            source.Play();
            soundList.Add(source);
            if (callBack != null)
                callBack(source);
        });
    }

    /// <summary>
    /// 停止音效
    /// </summary>
    /// <param name="name"></param>
    public void StopSound(AudioSource soure)
    {
        if (soundList.Contains(soure))
        {
            soundList.Remove(soure);
            soure.Stop();
            GameObject.Destroy(soure);
        }
    }

    /// <summary>
    /// 改变音效音量
    /// </summary>
    /// <param name="v"></param>
    public void ChangeSoundValue(float value)
    {
        soundValue = value;
        for (int i = 0; i < soundList.Count; i++)
            soundList[i].volume = value;
    }
}
