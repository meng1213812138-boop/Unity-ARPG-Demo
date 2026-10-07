using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    private Image currentHealthImg;
    private Text playerName;
    private Text maxHealthText;
    private Text currentHealthText;
    private PlayerController player;

    public void Init(PlayerController targetPlayer)
    {
        player = targetPlayer;
        playerName = GetControl<Text>("PlayerNameText");
        maxHealthText = GetControl<Text>("MaxHeathText");
        currentHealthText = GetControl<Text>("CurrentHealthText");
        currentHealthImg = GetControl<Image>("CurrentHealthImg");
        maxHealthText.text = player.maxHealth.ToString();
        playerName.text = player.playerName;
        resHealth();
    }

    private void Update()
    {
        resHealth();
    }

    private void resHealth()
    {
        if (player == null)
            return;
        currentHealthText.text = player.currentHealth.ToString();
        currentHealthImg.fillAmount = player.maxHealth > 0 ? (float)player.currentHealth / player.maxHealth : 0;
    }
}
