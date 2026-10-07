using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioData audioData = JsonMgr.Instance().LoadData<AudioData>("AudioData");
        AudioMgr.Instance().ChangeBKValue(audioData.BkMusicVoluem);
        AudioMgr.Instance().ChangeSoundValue(audioData.SoundVoluem);
        UIManager.Instance().ShowPanel<StartPanel>("StartPanel");
        AudioMgr.Instance().PlayBKMusic("BKMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
