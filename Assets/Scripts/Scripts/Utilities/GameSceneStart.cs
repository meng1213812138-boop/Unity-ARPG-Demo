using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneStart : MonoBehaviour
{
    private PlayerController playerController;
    private EnemyObject enemyObject;
    private bool isHaveTipsPanel = false;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        enemyObject = FindObjectOfType<EnemyObject>();
        UIManager.Instance().ShowPanel<GamePanel>("GamePanel", UIManager.E_UI_Layer.Mid, panel =>
        {
            panel.Init(playerController);
        });
    }

    // Update is called once per frame
    void Update()
    {
        Lose();
        Win();
    }


    public void Win()
    {
        if (enemyObject == null && !isHaveTipsPanel)
        {
            UIManager.Instance().ShowPanel<TipsPanel>("TipsPanel", UIManager.E_UI_Layer.Top, panel =>
            {
                panel.ReTipText("您已胜利");
            });
            isHaveTipsPanel = true;
        }
    }

    public void Lose()
    {
        if(playerController == null && !isHaveTipsPanel)
        {
            UIManager.Instance().ShowPanel<TipsPanel>("TipsPanel", UIManager.E_UI_Layer.Top, panel =>
            {
                panel.ReTipText("您已死亡");
            });
            isHaveTipsPanel = true;
        }
    }
}
